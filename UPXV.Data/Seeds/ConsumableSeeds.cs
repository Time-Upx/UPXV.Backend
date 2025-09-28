using UPXV.Models;

namespace UPXV.Data.Seeds;

public static class ConsumableSeeds
{
   public static Consumable[] Data => [BaseChocolate, GranuladoColorido, CasquinhasWaffle];

   public static readonly Consumable BaseChocolate = new()
   {
      Nid = 1,
      Tid = "Base de Sorvete de Chocolate",
      Description = "Base pré-misturada e rica de chocolate para fazer sorvete.",
      Quantity = 50,
      UnitNid = UnitSeeds.Litro.Nid,
      Tags = [TagSeeds.Laticinio, TagSeeds.Refrigerado]
   };

   public static readonly Consumable GranuladoColorido = new()
   {
      Nid = 2,
      Tid = "Granulado Colorido",
      Description = "Granulado de açúcar colorido para coberturas.",
      Quantity = 5,
      UnitNid = UnitSeeds.Quilograma.Nid,
      Tags = [TagSeeds.Cobertura]
   };

   public static readonly Consumable CasquinhasWaffle = new()
   {
      Nid = 3,
      Tid = "Casquinhas de Waffle",
      Description = "Caixa com grande quantidade de casquinhas de waffle.",
      Quantity = 10,
      UnitNid = UnitSeeds.Caixa.Nid,
   };
}