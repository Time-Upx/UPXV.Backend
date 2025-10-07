using UPXV.Backend.API.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class UnitSeeds
{
   public static Unit[] Data => [Litro, Quilograma, Caixa, Unidade];

   public static readonly Unit Litro = new()
   {
      Id = 1,
      Name = "Litro",
      Abbreviation = "L"
   };
   public static readonly Unit Quilograma = new()
   {
      Id = 2,
      Name = "Quilograma",
      Abbreviation = "kg"
   };
   public static readonly Unit Caixa = new()
   {
      Id = 3,
      Name = "Caixa",
      Abbreviation = "cx"
   };
   public static readonly Unit Unidade = new()
   {
      Id = 4,
      Name = "Unidade",
      Abbreviation = "un"
   };
}