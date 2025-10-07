using Microsoft.EntityFrameworkCore;

namespace UPXV.Backend.Data
{
   public interface IHasRequirements
   {
      public void LoadRequirements (DbContext context);
   }
}