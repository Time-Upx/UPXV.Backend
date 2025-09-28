using UPXV.Models;

namespace UPXV.Data.Seeds;

public static class UnitSeeds
{
   public static Unit[] Data => [Litro, Quilograma, Caixa, Unidade];

   public static readonly Unit Litro = new()
   {
      Nid = 1,
      Tid = "Litro",
      Abbreviation = "L"
   };
   public static readonly Unit Quilograma = new()
   {
      Nid = 2,
      Tid = "Quilograma",
      Abbreviation = "kg"
   };
   public static readonly Unit Caixa = new()
   {
      Nid = 3,
      Tid = "Caixa",
      Abbreviation = "cx"
   };
   public static readonly Unit Unidade = new()
   {
      Nid = 4,
      Tid = "Unidade",
      Abbreviation = "un"
   };
}