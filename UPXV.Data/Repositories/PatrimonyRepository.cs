using UPXV.Models;

namespace UPXV.Data.Repositories;

public class PatrimonyRepository(UPXV_Context context) : RepositoryBase<Patrimony> (context)
{
   public override void LoadRequirements (Patrimony patrimony)
   {
      Load(patrimony, p => p.Status);
   }
}
