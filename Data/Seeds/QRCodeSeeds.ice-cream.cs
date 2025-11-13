using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class QRCodeSeedsIceCream
{
   public static QRCode[] Data =>
   [
      QRCode_CaixaRegistradora,
      QRCode_SwitchStatusCaixaRegistradora,
      QRCode_AddGranuladoColorido,
      QRCode_TakeGranuladoColorido,
      QRCode_Tags
   ];

   public static int id = 0;
   public static string didi => $"Exemplo{++id}";
   public static string did => $"Exemplo{id}";

   public static int ca = 0;
   public static int cai => ++ca;

   public static readonly QRCode QRCode_CaixaRegistradora = new()
   {
      Id = didi,
      IntentId = IntentSeeds.PatrimonyDetail.Id,
      Name = "Ver Caixa Registradora",
      Description = "Detalhes da caixa registradora do balcão",
      Arguments =
      [
         new (cai, did, "id", PatrimonySeedsIceCream.CaixaRegistradora.Id)
      ]
   };

   public static readonly QRCode QRCode_SwitchStatusCaixaRegistradora = new()
   {
      Id = didi,
      IntentId = IntentSeeds.PatrimonySwitchStatus.Id,
      Name = "Status Caixa Registradora",
      Description = "Altera o Status da Caixa Registradora para Em Manutenção",
      Arguments =
      [
         new(cai, did, "id", PatrimonySeedsIceCream.CaixaRegistradora.Id),
         new(cai, did, "statusId", StatusSeedsIceCream.EmManutencao.Id),
      ]
   };

   public static readonly QRCode QRCode_AddGranuladoColorido = new()
   {
      Id = didi,
      IntentId = IntentSeeds.ConsumableAdd.Id,
      Name = "Adicionar Granulado Colorido",
      Description = "Entrada de 5kg de Granulado Colorido",
      Arguments =
      [
        new(cai, did, "id", ConsumableSeedsIceCream.GranuladoColorido.Id),
        new(cai, did, "amount", 50),
      ]
   };

   public static readonly QRCode QRCode_TakeGranuladoColorido = new()
   {
      Id = didi,
      IntentId = IntentSeeds.ConsumableTake.Id,
      Name = "Retirar Granulado Colorido",
      Description = "Retirada de 3kg para Produção",
      Arguments =
      [
        new(cai, did, "id", ConsumableSeedsIceCream.GranuladoColorido.Id),
        new(cai, did, "amount", 3),
      ]
   };

   public static readonly QRCode QRCode_Tags = new()
   {
      Id = didi,
      IntentId = IntentSeeds.TagList.Id,
      Name = "Detalhe Tag Vegano",
      Description = "Página de Detalhamento da Tag 'Sabor Vegano' (Tag ID 50)",
      Arguments = []
   };
}
