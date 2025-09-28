using UPXV.Models;

namespace UPXV.Data.Repositories;

public class ConsumableRepository(UPXV_Context context) : RepositoryBase<Consumable>(context)
{
   public override void LoadRequirements (Consumable consumable)
   {
      Load(consumable, c => c.Unit);
   }
}
