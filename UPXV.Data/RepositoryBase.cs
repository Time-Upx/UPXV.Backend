using UPXV.Models;

namespace UPXV.Data;

public class RepositoryBase<TEntity> where TEntity : class, IEntityBase
{
   protected UPXV_Context _context;

   public RepositoryBase (UPXV_Context context) => _context = context;

   public TEntity? FindByNid (int nid) => _context.Set<TEntity>().Find(nid);
   public TEntity? FindByTid (string tid) => _context.Set<TEntity>().FirstOrDefault(model => model.Tid == tid);
   public TEntity? FindBy (Func<TEntity, bool> predicate) => _context.Set<TEntity>().FirstOrDefault(predicate);
   public void Create (ref TEntity model) => _context.Set<TEntity>().Add(model);
   public void CreateMultiple (IEnumerable<TEntity> models) => _context.Set<TEntity>().AddRange(models); 
   public void Update (ref TEntity model) => _context.Set<TEntity>().Update(model);
   public void UpdateMultiple (IEnumerable<TEntity> models) => _context.Set<TEntity>().UpdateRange(models);
   public void Delete (ref TEntity model) => _context.Set<TEntity>().Remove(model);
   public void DeleteMultiple (IEnumerable<TEntity> models) => _context.Set<TEntity>().RemoveRange(models);
   public int Save() => _context.SaveChanges();
   public int Count(Query<TEntity> baseQuery) => baseQuery.ApplyTo(_context.Set<TEntity>()).Count();
   public ICollection<TResult> ReadQuery<TResult>(Query<TResult> query) where TResult : class, IEntityBase => 
      query.ApplyTo(_context.Set<TResult>()).ToList();
}