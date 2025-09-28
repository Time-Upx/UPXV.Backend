using UPXV.DTOs.Tags;
using UPXV.DTOs.Units;
using UPXV.Models;

namespace UPXV.DTOs.Consumables;

public record ConsumableDetailDTO
{
   public required int Nid { get; set; }
   public required string Tid { get; set; }
   public required double Quantity { get; set; }
   public required IEnumerable<TagListDTO> Tags { get; set; }
   public required UnitDetailDTO Unit { get; set; }
   public static ConsumableDetailDTO Of (Consumable consumable) => new ConsumableDetailDTO
   {
      Nid = consumable.Nid,
      Tid = consumable.Tid,
      Quantity = consumable.Quantity,
      Tags = consumable.Tags.Select(TagListDTO.Of),
      Unit = UnitDetailDTO.Of(consumable.Unit!),
   };
}
