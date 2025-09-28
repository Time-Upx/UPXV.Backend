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
      .Find(nid);

   public TEntity? FindBy (Func<TEntity, bool> predicate) => _context
      .Set<TEntity>()
      .FirstOrDefault(predicate);

   public ICollection<TEntity> FindAll (Func<TEntity, bool> predicate) => _context
      .Set<TEntity>()
      .Where(predicate)
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

   public bool DoesValueExists<TValue> (TValue value, Expression<Func<TEntity, TValue>> propertySelector)
   {
      // 1. Get the parameter from the property selector (e.g., the 'e' in 'e => e.Name')
      var parameter = propertySelector.Parameters.Single();

      // 2. Create the right side of the comparison: a constant value
      var constant = Expression.Constant(value, typeof(TValue));

      // 3. Create the left side of the comparison: the property access expression
      //    (e.g., 'e.Name') which comes from the body of the lambda
      var memberAccess = propertySelector.Body;

      // 4. Create the final binary expression for the equality check (e.g., 'e.Name == value')
      var equality = Expression.Equal(memberAccess, constant);

      // 5. Build the complete lambda expression for the .Any() method
      var lambda = Expression.Lambda<Func<TEntity, bool>>(equality, parameter);

      // 6. Execute the query
      return _context.Set<TEntity>().Any(lambda);
   }

   public void Load<TValue> (TEntity entity, Expression<Func<TEntity, TValue?>> property)
      where TValue : class
      => _context.Entry(entity).Reference(property).Load();

   public void Load<TValue> (TEntity entity, Expression<Func<TEntity, IEnumerable<TValue>>> property)
      where TValue : class
      => _context.Entry(entity).Collection(property).Load();
}