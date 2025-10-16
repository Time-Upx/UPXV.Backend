using UPXV.Backend.DTOs.Tags;
using UPXV.Backend.DTOs.Units;
using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Consumables;

public record ConsumableDetailDTO
{
   public required int Id { get; set; }
   public required string Name { get; set; }
   public required double Quantity { get; set; }
   public required IEnumerable<TagListDTO> Tags { get; set; }
   public required UnitDetailDTO Unit { get; set; }
   public static ConsumableDetailDTO Of (Consumable consumable) => new ConsumableDetailDTO
   {
      Id = consumable.Id,
      Name = consumable.Name,
      Quantity = consumable.Quantity,
      Tags = consumable.Tags.Select(TagListDTO.Of),
      Unit = UnitDetailDTO.Of(consumable.Unit!),
   };
}

