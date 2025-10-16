using Microsoft.EntityFrameworkCore;
using UPXV.Backend.Data;

namespace UPXV.Backend.Entities;

public class Consumable : IHasRequirements, INamedEntity
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public string? Description { get; set; }
   public ICollection<Tag> Tags { get; set; } = [];
   public double Quantity { get; set; }
   public int UnitId { get; set; }
   public Unit? Unit { get; set; }

   public void LoadRequirements (DbContext context)
   {
      context.Load(this, c => c.Unit);
      context.LoadCollection(this, c => c.Tags);
   }
}
