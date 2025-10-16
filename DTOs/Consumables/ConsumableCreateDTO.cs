using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Consumables;

public record ConsumableCreateDTO
{
   public required string Name { get; set; }
   public required int UnitId { get; set; }
   public double Quantity { get; set; } = 0;
   public string? Description { get; set; }
   public int[] TagIds { get; set; } = [];
   public Consumable BuildEntity (ICollection<Tag> tags) => new Consumable
   {
      Name = Name,
      Quantity = Quantity,
      UnitId = UnitId,
      Description = Description,
      Tags = tags
   };
}
