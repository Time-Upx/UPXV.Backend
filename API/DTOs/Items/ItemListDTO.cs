using UPXV.Backend.API.DTOs.Consumables;
using UPXV.Backend.API.DTOs.Patrimonies;
using UPXV.Backend.API.Entities;

namespace UPXV.Backend.API.DTOs.Items;

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
