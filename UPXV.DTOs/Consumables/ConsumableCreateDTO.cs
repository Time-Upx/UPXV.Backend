using UPXV.Models;

namespace UPXV.DTOs.Consumables;

public record ConsumableCreateDTO
{
   public required string Tid { get; set; }
   public required int UnitNid { get; set; }
   public double Quantity { get; set; } = 0;
   public string? Description { get; set; }
   public IEnumerable<int> TagNids { get; set; } = [];
   public Consumable BuildEntity (ICollection<Tag> tags) => new Consumable
   {
      Tid = Tid,
      Quantity = Quantity,
      UnitNid = UnitNid,
      Description = Description,
      Tags = tags
   };
}
