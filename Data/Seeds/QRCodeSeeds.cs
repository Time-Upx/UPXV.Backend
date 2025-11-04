using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class QRCodeSeeds
{
   public static QRCode[] Data =>
   [
      QRCode_CaixaRegistradora,
      QRCode_SwitchStatusCaixaRegistradora,
      QRCode_AddGranuladoColorido,
      QRCode_TakeGranuladoColorido,
      QRCode_Tags
   ];

   public static int cq = 0;
   public static int ca = 0;
   public static int cqi => ++cq;
   public static int cai => ++ca;

   public static readonly QRCode QRCode_CaixaRegistradora = new()
   {
      Id = cqi,
      IntentId = IntentSeeds.PatrimonyDetail.Id,
      Name = "Ver Caixa Registradora",
      Description = "Detalhes da caixa registradora do balcão",
      Arguments =
      [
         new (cai, cq, "id", PatrimonySeeds.CaixaRegistradora.Id)
      ]
   };

   public static readonly QRCode QRCode_SwitchStatusCaixaRegistradora = new()
   {
      Id = cqi,
      IntentId = IntentSeeds.PatrimonySwitchStatus.Id,
      Name = "Status Caixa Registradora",
      Description = "Altera o Status da Caixa Registradora para Em Manutenção",
      Arguments =
      [
         new(cai, cq, "id", PatrimonySeeds.CaixaRegistradora.Id),
         new(cai, cq, "statusId", StatusSeeds.EmManutencao.Id),
      ]
   };

   public static readonly QRCode QRCode_AddGranuladoColorido = new()
   {
      Id = cqi,
      IntentId = IntentSeeds.ConsumableAdd.Id,
      Name = "Adicionar Granulado Colorido",
      Description = "Entrada de 5kg de Granulado Colorido",
      Arguments =
      [
        new(cai, cq, "id", ConsumableSeeds.GranuladoColorido.Id),
        new(cai, cq, "amount", 50),
      ]
   };

   public static readonly QRCode QRCode_TakeGranuladoColorido = new()
   {
      Id = cqi,
      IntentId = IntentSeeds.ConsumableTake.Id,
      Name = "Retirar Granulado Colorido",
      Description = "Retirada de 3kg para Produção",
      Arguments =
      [
        new(cai, cq, "id", ConsumableSeeds.GranuladoColorido.Id),
        new(cai, cq, "amount", 3),
      ]
   };

   public static readonly QRCode QRCode_Tags = new()
   {
      Id = cqi,
      IntentId = IntentSeeds.TagList.Id,
      Name = "Detalhe Tag Vegano",
      Description = "Página de Detalhamento da Tag 'Sabor Vegano' (Tag ID 50)",
      Arguments = []
   };
}
