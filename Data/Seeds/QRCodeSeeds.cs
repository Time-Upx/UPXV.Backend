using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class QRCodeSeeds
{
   public static QRCode[] Data =>
   [
      QRCode_CadeiraOdontologica,
      QRCode_SwitchStatusCadeiraOdontologica,
      QRCode_AddLuvas,
      QRCode_TakeLuvas,
      QRCode_Tags
   ];

   public static int id = 0;
   public static string didi => $"Exemplo{++id}";
   public static string did => $"Exemplo{id}";

   public static int ca = 0;
   public static int cai => ++ca;

   public static readonly QRCode QRCode_CadeiraOdontologica = new()
   {
      Id = didi,
      IntentId = IntentSeeds.PatrimonyDetail.Id,
      Name = "Ver Cadeira Odontológica",
      Description = "Detalhes da cadeira principal do consultório",
      Arguments =
      [
         new (cai, did, "id", PatrimonySeeds.CadeiraOdontologica.Id)
      ]
   };

   public static readonly QRCode QRCode_SwitchStatusCadeiraOdontologica = new()
   {
      Id = didi,
      IntentId = IntentSeeds.PatrimonySwitchStatus.Id,
      Name = "Status Cadeira Odontológica",
      Description = "Altera o Status da Cadeira Odontológica para Em Manutenção",
      Arguments =
      [
         new(cai, did, "id", PatrimonySeeds.CadeiraOdontologica.Id),
         new(cai, did, "statusId", StatusSeeds.EmManutencao.Id),
      ]
   };

   public static readonly QRCode QRCode_AddLuvas = new()
   {
      Id = didi,
      IntentId = IntentSeeds.ConsumableAdd.Id,
      Name = "Adicionar Luvas de Nitrilo",
      Description = "Entrada de 10 caixas de Luvas de Nitrilo",
      Arguments =
      [
        new(cai, did, "id", ConsumableSeeds.LuvasNitrilicas.Id),
        new(cai, did, "amount", 10),
      ]
   };

   public static readonly QRCode QRCode_TakeLuvas = new()
   {
      Id = didi,
      IntentId = IntentSeeds.ConsumableTake.Id,
      Name = "Retirar Luvas de Nitrilo",
      Description = "Retirada de 1 caixa para uso no Consultório 1",
      Arguments =
      [
        new(cai, did, "id", ConsumableSeeds.LuvasNitrilicas.Id),
        new(cai, did, "amount", 1),
      ]
   };

   public static readonly QRCode QRCode_Tags = new()
   {
      Id = didi,
      IntentId = IntentSeeds.TagList.Id,
      Name = "Detalhe Tag Clínico",
      Description = "Página de Detalhamento da Tag 'Clínico' (Tag ID 3)",
      Arguments = []
   };
}