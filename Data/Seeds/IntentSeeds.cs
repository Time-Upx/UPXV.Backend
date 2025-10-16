using UPXV.Backend.Endpoints;
using UPXV.Backend.Endpoints.QRCodes;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class IntentSeeds
{
   public static Intent[] Data => [
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
      Name = "Listagem de Tags",
      Description = "",
      UrlTemplate = Routes.Tags.LIST_PAGE,
      Parameters = [],
   };

   public static readonly Intent TagDetail = new()
   {
      Id = 2,
      Type = IntentType.Redirect,
      Name = "Detalhamento de Tag",
      Description = "",
      UrlTemplate = Routes.Tags.DETAIL_PAGE,
      Parameters = ["id"],
   };
   
   public static readonly Intent ItemList = new()
   {
      Id = 3,
      Type = IntentType.Redirect,
      Name = "Listagem de Items",
      Description = "",
      UrlTemplate = Routes.Items.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent QRCodeList = new()
   {
      Id = 4,
      Type = IntentType.Redirect,
      Name = "Listagem de Códigos QR",
      Description = "",
      UrlTemplate = Routes.QRCodes.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent QRCodeDetail = new()
   {
      Id = 5,
      Type = IntentType.Redirect,
      Name = "Detalhamento de Código QR",
      Description = "",
      UrlTemplate = Routes.QRCodes.DETAIL_PAGE,
      Parameters = ["id"],
   };

   public static readonly Intent StatusList = new()
   {
      Id = 6,
      Type = IntentType.Redirect,
      Name = "Listagem de Status",
      Description = "",
      UrlTemplate = Routes.Status.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent StatusDetail = new()
   {
      Id = 7,
      Type = IntentType.Redirect,
      Name = "Detalhamento de Status",
      Description = "",
      UrlTemplate = Routes.Status.DETAIL_PAGE,
      Parameters = ["id"],
   };
   
   public static readonly Intent UnitList = new()
   {
      Id = 8,
      Type = IntentType.Redirect,
      Name = "Listagem de Unidades",
      Description = "",
      UrlTemplate = Routes.Units.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent UnitDetail = new()
   {
      Id = 9,
      Type = IntentType.Redirect,
      Name = "Detalhamento de Unidade",
      Description = "",
      UrlTemplate = Routes.Units.DETAIL_PAGE,
      Parameters = ["id"],
   };
   
   public static readonly Intent ConsumableList = new()
   {
      Id = 10,
      Type = IntentType.Redirect,
      Name = "Listagem de Consumíveis",
      Description = "",
      UrlTemplate = Routes.Consumables.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent ConsumableDetail = new()
   {
      Id = 11,
      Type = IntentType.Redirect,
      Name = "Detalhamento de Consumível",
      Description = "",
      UrlTemplate = Routes.Consumables.DETAIL_PAGE,
      Parameters = ["id"],
   };

   public static readonly Intent ConsumableAdd = new()
   {
      Id = 12,
      Type = IntentType.Action,
      Name = "Entrada de Consumível",
      Description = "",
      UrlTemplate = Routes.Consumables.ADD_ACTION,
      Parameters = ["id", "amount"],
   };
   
   public static readonly Intent ConsumableTake = new()
   {
      Id = 13,
      Type = IntentType.Action,
      Name = "Retirada de Consumível",
      Description = "",
      UrlTemplate = Routes.Consumables.TAKE_ACTION,
      Parameters = ["id", "amount"],
   };
   
   public static readonly Intent PatrimonyList = new()
   {
      Id = 14,
      Type = IntentType.Redirect,
      Name = "Listagem de Patrimônios",
      Description = "",
      UrlTemplate = Routes.Patrimonies.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent PatrimonyDetail = new()
   {
      Id = 15,
      Type = IntentType.Redirect,
      Name = "Detalhamento de Patrimônio",
      Description = "",
      UrlTemplate = Routes.Patrimonies.DETAIL_PAGE,
      Parameters = ["id"],
   };
   
   public static readonly Intent PatrimonySwitchStatus = new()
   {
      Id = 16,
      Type = IntentType.Action,
      Name = "Alterar Status de Patrimônio",
      Description = "",
      UrlTemplate = Routes.Patrimonies.SWITCH_STATUS_ACTION,
      Parameters = ["id", "statusId"],
   };
}
