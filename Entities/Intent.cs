using Microsoft.EntityFrameworkCore;
using UPXV.Backend.Data;

namespace UPXV.Backend.Entities;

public class Intent : INamedEntity, IHasRequirements
{
   public int Id { get; set; }
   public required IntentType Type { get; set; }
   public required string Name { get; set; }
   public required string Description { get; set; }
   public required string UrlTemplate { get; set; }
   public List<IntentParameter> Parameters { get; set; } = [];

   public void LoadRequirements (DbContext context)
   {
      context.LoadCollection(this, i => i.Parameters);
   }
}
