using UPXV.Backend.DTOs.Consumables;
using UPXV.Backend.DTOs.Patrimonies;
using UPXV.Backend.Entities;

namespace UPXV.Backend.DTOs.Items;

public record ItemListDTO
{
   public ConsumableListDTO? Consumable {  get; set; }
   public PatrimonyListDTO? Patrimony { get; set; }

   public static ItemListDTO Of (Consumable consumable) => new ItemListDTO
   {
      Consumable = ConsumableListDTO.Of(consumable)
   };
   public static ItemListDTO Of (Patrimony patrimony) => new ItemListDTO
   {
      Patrimony = PatrimonyListDTO.Of(patrimony)
   };
}
