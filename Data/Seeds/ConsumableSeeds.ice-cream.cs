using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class ConsumableSeedsIceCream
{
   public static Consumable[] Data => [BaseChocolate, GranuladoColorido, CasquinhasWaffle];

   public static readonly Consumable BaseChocolate = new()
   {
      Id = 1,
      Name = "Base de Sorvete de Chocolate",
      Description = "Base pré-misturada e rica de chocolate para fazer sorvete.",
      Quantity = 50,
      UnitId = UnitSeeds.Litro.Id,
      Tags = [TagSeedsIceCream.Laticinio, TagSeedsIceCream.Refrigerado]
   };

   public static readonly Consumable GranuladoColorido = new()
   {
      Id = 2,
      Name = "Granulado Colorido",
      Description = "Granulado de açúcar colorido para coberturas.",
      Quantity = 5,
      UnitId = UnitSeeds.Quilograma.Id,
      Tags = [TagSeedsIceCream.Cobertura]
   };

   public static readonly Consumable CasquinhasWaffle = new()
   {
      Id = 3,
      Name = "Casquinhas de Waffle",
      Description = "Caixa com grande quantidade de casquinhas de waffle.",
      Quantity = 10,
      UnitId = UnitSeedsIceCream.Caixa.Id,
   };
}