using UPXV.Backend.DTOs.Tags;
using UPXV.Backend.DTOs.Units;
using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Consumables;

public record ConsumableListDTO
{
   public required int Id { get; set; }
   public required string Name { get; set; }
   public IEnumerable<TagListDTO> Tags { get; set; } = [];
   public double Quantity { get; set; }
   public required UnitListDTO Unit { get; set; }

   public static ConsumableListDTO Of (Consumable consumable) => new ConsumableListDTO
   {
      Id = consumable.Id,
      Name = consumable.Name,
      Quantity = consumable.Quantity,
      Tags = consumable.Tags.Select(TagListDTO.Of),
      Unit = UnitListDTO.Of(consumable.Unit!),
   };
}