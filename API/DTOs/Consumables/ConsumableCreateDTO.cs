using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Consumables;

public record ConsumableCreateDTO
{
   public required string Name { get; set; }
   public required int UnitId { get; set; }
   public double Quantity { get; set; } = 0;
   public string? Description { get; set; }
   public IEnumerable<int> TagIds { get; set; } = [];
   public Consumable BuildEntity (ICollection<Tag> tags) => new Consumable
   {
      Name = Name,
      Quantity = Quantity,
      UnitId = UnitId,
      Description = Description,
      Tags = tags
   };
}
