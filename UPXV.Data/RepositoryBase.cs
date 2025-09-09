using UPXV.Models;

namespace UPXV.Data;

public class RepositoryBase<TModel> where TModel : class, IBaseModel
{
   protected UPXV_Context _context;

   public RepositoryBase (UPXV_Context context) => _context = context;

   public TModel? FindByNid (int nid) => _context.Set<TModel>().Find(nid);
   public TModel? FindByTid (string tid) => _context.Set<TModel>().FirstOrDefault(model => model.Tid == tid);
   public TModel? FindBy (Func<TModel, bool> predicate) => _context.Set<TModel>().FirstOrDefault(predicate);
   public void Create (TModel model) => _context.Set<TModel>().Add(model);
   public void CreateMultiple (IEnumerable<TModel> models) => _context.Set<TModel>().AddRange(models); 
   public void Update (TModel model) => _context.Set<TModel>().Update(model);
   public void UpdateMultiple (IEnumerable<TModel> models) => _context.Set<TModel>().UpdateRange(models);
   public void Delete (TModel model) => _context.Set<TModel>().Remove(model);
   public void DeleteMultiple (IEnumerable<TModel> models) => _context.Set<TModel>().RemoveRange(models);
   public void Save() => _context.SaveChanges();
}













