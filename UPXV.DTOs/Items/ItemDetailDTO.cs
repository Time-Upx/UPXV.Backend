using UPXV.DTOs.Consumables;
using UPXV.DTOs.Patrimonies;
using UPXV.DTOs.Tags;
using UPXV.Models;

namespace UPXV.DTOs.Items;

public record ItemDetailDTO
{
   public ConsumableDetailDTO? Consumable { get; set; }
   public PatrimonyDetailDTO? Patrimony { get; set; }
   public static ItemDetailDTO Of (Consumable consumable) => new ItemDetailDTO
   {
      Consumable = ConsumableDetailDTO.Of(consumable)
   };
   public static ItemDetailDTO Of (Patrimony patrimony) => new ItemDetailDTO
   {
      Patrimony = PatrimonyDetailDTO.Of(patrimony)
   };
}
