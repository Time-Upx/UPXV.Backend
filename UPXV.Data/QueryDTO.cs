using System.Linq.Dynamic.Core;
using UPXV.Models;

namespace UPXV.Data;

public record struct QueryDTO<TEntity> where TEntity : class, IEntityBase
{
   public QueryDTO () { }
   public int Skip { get; set; } = 0;
   public int Take { get; set; } = 0;
   public bool AsNoTracking { get; set; } = false;
   public ICollection<string> Filters { get; set; } = [];
   public ICollection<string> Includes { get; set; } = [];
   public ICollection<SortDetails> Sortings { get; set; } = [];

   public Query<TEntity> ToQuery ()
   {
      var query = new Query<TEntity>();

      if (AsNoTracking)
      {
         query.AsNoTracking();
      }

      query.Skip(Skip);
      query.Take(Take);

      // Convert string filters to Expression<Func<TEntity, bool>>
      foreach (var filterString in Filters)
      {
         var expression = DynamicExpressionParser.ParseLambda<TEntity, bool>(ParsingConfig.Default, false, filterString);
         query.Filter(expression);
      }

      // Convert string includes to Expression<Func<TEntity, object>>
      foreach (var includeString in Includes)
      {
         var expression = DynamicExpressionParser.ParseLambda<TEntity, object>(ParsingConfig.Default, false, includeString);
         query.Include(expression);
      }

      // Convert string sortings to Expression<Func<TEntity, object>>
      foreach (var sortDetails in Sortings)
      {
         var expression = DynamicExpressionParser.ParseLambda<TEntity, object>(ParsingConfig.Default, false, sortDetails.Expression);
         if (sortDetails.Descending)
         {
            query.SortByDescending(expression);
         }
         else
         {
            query.SortBy(expression);
         }
      }

      return query;
   }
}

public record struct SortDetails (string Expression, bool Descending);