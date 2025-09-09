using UPXV.Models;

namespace UPXV.Data.Seeds;

public static class UnitSeeds
{
   public static readonly Unit[] Data = [Meters!, SingleUnit!, Kilograms!, Grams!, Liters!];

   public static readonly Unit Meters = new ()
   {
      Tid = "Metros",
      Abbreviation = "m",
   };

   public static readonly Unit SingleUnit = new ()
   {
      Tid = "Unidades",
      Abbreviation = "un",
   };

   public static readonly Unit Kilograms = new ()
   {
      Tid = "Kilogramas",
      Abbreviation = "kg",
   };

   public static readonly Unit Grams = new()
   {
      Tid = "Gramas",
      Abbreviation = "g",
   };

   public static readonly Unit Liters = new()
   {
      Tid = "Litros",
      Abbreviation = "L",
   };
}
