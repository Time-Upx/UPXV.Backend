using UPXV.Models;

namespace UPXV.Data.Seeds;

public static class ConsumableSeeds
{
   public static readonly Consumable[] Data = [Consumable1!, Consumable2!, Consumable3!, Consumable4!];

   public static readonly Consumable Consumable1 = new()
   {
      Tid = "Consumível 1",
      Quantity = 5,
      Unit = UnitSeeds.Meters,
   };
   public static readonly Consumable Consumable2 = new()
   {
      Tid = "Consumível 2",
      Unit = UnitSeeds.SingleUnit,
   };
   public static readonly Consumable Consumable3 = new()
   {
      Tid = "Consumível 3",
      Quantity = 1,
      Unit = UnitSeeds.Kilograms,

   };
   public static readonly Consumable Consumable4 = new()
   {
      Tid = "Consumível 4",
      Unit = UnitSeeds.Liters,
   };
}
