using UPXV.Backend.Endpoints;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class IntentSeeds
{
   public static IntentParameter Id (int intentId) => new (intentId, "id");
   public static Intent[] Data => 
   [
      ConsumableList, ConsumableDetail, ConsumableAdd, ConsumableTake,
      PatrimonyList, PatrimonyDetail, PatrimonySwitchStatus,
      QRCodeList, QRCodeDetail,
      StatusList, StatusDetail,
      UnitList, UnitDetail,
      TagList, TagDetail,
      ItemList,
   ];

   public static readonly Intent TagList = new()
   {
      Id = 1,
      Type = IntentType.Redirect,
      Name = "Página de Tags",
      Description = "",
      UrlTemplate = Routes.Tags.LIST_PAGE,
      Parameters = [],
   };

   public static readonly Intent TagDetail = new()
   {
      Id = 2,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Tag",
      Description = "",
      UrlTemplate = Routes.Tags.DETAIL_PAGE,
      Parameters = [Id(2)],
   };
   
   public static readonly Intent ItemList = new()
   {
      Id = 3,
      Type = IntentType.Redirect,
      Name = "Página de Items",
      Description = "",
      UrlTemplate = Routes.Items.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent QRCodeList = new()
   {
      Id = 4,
      Type = IntentType.Redirect,
      Name = "Página de Códigos QR",
      Description = "",
      UrlTemplate = Routes.QRCodes.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent QRCodeDetail = new()
   {
      Id = 5,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Código QR",
      Description = "",
      UrlTemplate = Routes.QRCodes.DETAIL_PAGE,
      Parameters = [Id(5)],
   };

   public static readonly Intent StatusList = new()
   {
      Id = 6,
      Type = IntentType.Redirect,
      Name = "Página de Status",
      Description = "",
      UrlTemplate = Routes.Status.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent StatusDetail = new()
   {
      Id = 7,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Status",
      Description = "",
      UrlTemplate = Routes.Status.DETAIL_PAGE,
      Parameters = [Id(7)],
   };
   
   public static readonly Intent UnitList = new()
   {
      Id = 8,
      Type = IntentType.Redirect,
      Name = "Página de Unidades",
      Description = "",
      UrlTemplate = Routes.Units.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent UnitDetail = new()
   {
      Id = 9,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Unidade",
      Description = "",
      UrlTemplate = Routes.Units.DETAIL_PAGE,
      Parameters = [Id(9)],
   };
   
   public static readonly Intent ConsumableList = new()
   {
      Id = 10,
      Type = IntentType.Redirect,
      Name = "Página de Consumíveis",
      Description = "",
      UrlTemplate = Routes.Consumables.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent ConsumableDetail = new()
   {
      Id = 11,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Consumível",
      Description = "",
      UrlTemplate = Routes.Consumables.DETAIL_PAGE,
      Parameters = [Id(11)],
   };

   public static readonly Intent ConsumableAdd = new()
   {
      Id = 12,
      Type = IntentType.Post,
      Name = "Dar Entrada de Consumível",
      Description = "",
      UrlTemplate = Routes.Consumables.ADD_ACTION,
      Parameters = [Id(12), new(12, "amount")],
   };
   
   public static readonly Intent ConsumableTake = new()
   {
      Id = 13,
      Type = IntentType.Post,
      Name = "Fazer Retirada de Consumível",
      Description = "",
      UrlTemplate = Routes.Consumables.TAKE_ACTION,
      Parameters = [Id(13), new(13, "amount")],
   };
   
   public static readonly Intent PatrimonyList = new()
   {
      Id = 14,
      Type = IntentType.Redirect,
      Name = "Página de Patrimônios",
      Description = "",
      UrlTemplate = Routes.Patrimonies.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent PatrimonyDetail = new()
   {
      Id = 15,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Patrimônio",
      Description = "",
      UrlTemplate = Routes.Patrimonies.DETAIL_PAGE,
      Parameters = [Id(15)],
   };
   
   public static readonly Intent PatrimonySwitchStatus = new()
   {
      Id = 16,
      Type = IntentType.Post,
      Name = "Alterar Status de Patrimônio",
      Description = "",
      UrlTemplate = Routes.Patrimonies.SWITCH_STATUS_ACTION,
      Parameters = [Id(16), new(16, "statusId")],
   };
}
