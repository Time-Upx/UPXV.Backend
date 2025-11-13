using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class ConsumableSeeds
{
   public static Consumable[] Data => [LuvasNitrilicas, MascarasDescartaveis, ResinaComposta];

   public static readonly Consumable LuvasNitrilicas = new()
   {
      Id = 1,
      Name = "Luvas de Nitrilo (Caixa)",
      Description = "Caixa de luvas descartáveis de nitrilo, tamanho M.",
      Quantity = 20,
      UnitId = UnitSeeds.Caixa.Id,
      Tags = [TagSeeds.Descartavel, TagSeeds.Esterilizacao]
   };

   public static readonly Consumable MascarasDescartaveis = new()
   {
      Id = 2,
      Name = "Máscaras Descartáveis",
      Description = "Máscaras faciais triplas descartáveis, com elástico.",
      Quantity = 50,
      UnitId = UnitSeeds.Unidade.Id,
      Tags = [TagSeeds.Descartavel]
   };

   public static readonly Consumable ResinaComposta = new()
   {
      Id = 3,
      Name = "Resina Composta Universal (Seringa)",
      Description = "Seringas de resina para restaurações dentárias.",
      Quantity = 15,
      UnitId = UnitSeeds.Unidade.Id, // Mantendo "Unidade" para cada seringa
   };
}