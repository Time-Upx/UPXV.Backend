using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UPXV.Models;

namespace UPXV.Data;

public record Query<TEntity> where TEntity : class, IEntityBase
{
   public int _skip;
   public int _take;
   public bool _asNoTracking;
   public readonly ICollection<Expression<Func<TEntity, bool>>> _filters = [];
   public readonly ICollection<Expression<Func<TEntity, object>>> _includes = [];
   public readonly ICollection<(Expression<Func<TEntity, object>> Expression, bool Descending)> _sortings = [];

   public Query<TEntity> Skip (int skip)
   {
      _skip = skip;
      return this;
   }
   public Query<TEntity> Take (int take)
   {
      _take = take;
      return this;
   }
   public Query<TEntity> AsNoTracking ()
   {
      _asNoTracking = true;
      return this;
   }
   public Query<TEntity> Filter (Expression<Func<TEntity, bool>> predicate)
   {
      _filters.Add(predicate);
      return this;
   }
   public Query<TEntity> Include (Expression<Func<TEntity, object>> property)
   {
      _includes.Add(property);
      return this;
   }
   public Query<TEntity> SortBy<TKey> (Expression<Func<TEntity, TKey>> expression)
   {
      _sortings.Add((expression as Expression<Func<TEntity, object>>, false)!);
      return this;
   }
   
   public Query<TEntity> SortByDescending<TKey> (Expression<Func<TEntity, TKey>> expression)
   {
      _sortings.Add((expression as Expression<Func<TEntity, object>>, true)!);
      return this;
   }

   public IQueryable<TEntity> ApplyTo (IQueryable<TEntity> queryable)
   {
      if(queryable is null)
      {
         throw new ArgumentNullException(nameof(queryable));
      }

      if (_asNoTracking)
      {
         queryable = queryable.AsNoTracking();
      }

      foreach (var include in _includes)
      {
         queryable = queryable.Include(include);
      }

      foreach (var filter in _filters)
      {
         queryable = queryable.Where(filter);
      }

      if (_sortings.Any())
      {
         var firstSorting = _sortings.First();
         var sortedQueryable = firstSorting.Descending
            ? queryable.OrderByDescending(firstSorting.Expression)
            : queryable.OrderBy(firstSorting.Expression);
            
         foreach (var sorting in _sortings.Skip(1))
         {
            sortedQueryable = sorting.Descending
               ? sortedQueryable.ThenByDescending(sorting.Expression)
               : sortedQueryable.ThenBy(sorting.Expression);
         }

         queryable = sortedQueryable;
      }

      if (_skip > 0)
      {
         queryable = queryable.Skip(_skip);
      }

      if (_take > 0)
      {
         queryable = queryable.Take(_take);
      }

      return queryable;
   }
}