using System.Linq.Expressions;
using UPXV.Common.Extensions;
using UPXV.Models;

namespace UPXV.Data;

public abstract class RepositoryBase<TEntity> where TEntity : class
{
   protected UPXV_Context _context;

   public RepositoryBase (UPXV_Context context) => _context = context;

   public virtual void LoadRequirements (TEntity entity) {}

   public TEntity? FindByNid (int nid) => _context
      .Set<TEntity>()
      .Find(nid)
      .LoadRequirements(this);

   public TEntity? FindBy (Func<TEntity, bool> predicate) => _context
      .Set<TEntity>()
      .FirstOrDefault(predicate)
      .LoadRequirements(this);

   public ICollection<TEntity> FindAll (Func<TEntity, bool> predicate) => _context
      .Set<TEntity>()
      .Where(predicate)
      .LoadRequirements(this)
      .ToList();

   public void Create (TEntity model) => _context.Set<TEntity>().Add(model);
   public void CreateMultiple (IEnumerable<TEntity> models) => _context.Set<TEntity>().AddRange(models);

   public void Update (TEntity model) => _context.Set<TEntity>().Update(model);
   public void UpdateMultiple (IEnumerable<TEntity> models) => _context.Set<TEntity>().UpdateRange(models);

   public void Delete (TEntity model) => _context.Set<TEntity>().Remove(model);
   public void DeleteMultiple (IEnumerable<TEntity> models) => _context.Set<TEntity>().RemoveRange(models);

   public int Save () => _context.SaveChanges();
   
   public int Count (Query<TEntity> baseQuery) => _context.Set<TEntity>().ApplyQuery(baseQuery).Count();
   
   public ICollection<TResult> ReadQuery<TResult> (Query<TResult> query) where TResult : class =>
      _context.Set<TResult>().ApplyQuery(query).ToList();
   
   public bool DoesValueExists<TValue> (TValue value, Func<TEntity, TValue> property) => _context
      .Set<TEntity>()
      .Any(e => value!.Equals(property(e)));

   public void Load<TValue> (TEntity entity, Expression<Func<TEntity, TValue?>> property)
      where TValue : class
      => _context.Entry(entity).Reference(property).Load();

   public void Load<TValue> (TEntity entity, Expression<Func<TEntity, IEnumerable<TValue>>> property)
      where TValue : class
      => _context.Entry(entity).Collection(property).Load();
}

public static class RepositoryExtensions
{
   public static TEntity? LoadRequirements<TEntity> (this TEntity? entity, RepositoryBase<TEntity> repository)
      where TEntity : class
   {
      if (entity is null) return null;
      repository.LoadRequirements(entity);
      return entity;
   }
   public static IEnumerable<TEntity> LoadRequirements<TEntity> (this IEnumerable<TEntity> entities, RepositoryBase<TEntity> repository)
      where TEntity : class
      => entities.Peek(repository.LoadRequirements);
}