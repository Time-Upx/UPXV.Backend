using UPXV.Backend.Endpoints;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Data.Seeds;

public static class IntentSeedsIceCream
{
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

   public static int c = 0;
   public static int ci => ++c;

   public static readonly Intent TagList = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Tags",
      Description = "",
      UrlTemplate = Routes.Tags.LIST_PAGE,
      Parameters = [],
   };

   public static readonly Intent TagDetail = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Tag",
      Description = "",
      UrlTemplate = Routes.Tags.DETAIL_PAGE,
      Parameters = [new(c, "id")],
   };
   
   public static readonly Intent ItemList = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Items",
      Description = "",
      UrlTemplate = Routes.Items.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent QRCodeList = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Códigos QR",
      Description = "",
      UrlTemplate = Routes.QRCodes.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent QRCodeDetail = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Código QR",
      Description = "",
      UrlTemplate = Routes.QRCodes.DETAIL_PAGE,
      Parameters = [new(c, "id")],
   };

   public static readonly Intent StatusList = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Status",
      Description = "",
      UrlTemplate = Routes.Status.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent StatusDetail = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Status",
      Description = "",
      UrlTemplate = Routes.Status.DETAIL_PAGE,
      Parameters = [new(c, "id")],
   };
   
   public static readonly Intent UnitList = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Unidades",
      Description = "",
      UrlTemplate = Routes.Units.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent UnitDetail = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Unidade",
      Description = "",
      UrlTemplate = Routes.Units.DETAIL_PAGE,
      Parameters = [new(c, "id")],
   };
   
   public static readonly Intent ConsumableList = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Consumíveis",
      Description = "",
      UrlTemplate = Routes.Consumables.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent ConsumableDetail = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Consumível",
      Description = "",
      UrlTemplate = Routes.Consumables.DETAIL_PAGE,
      Parameters = [new(c, "id")],
   };

   public static readonly Intent ConsumableAdd = new()
   {
      Id = ci,
      Type = IntentType.Post,
      Name = "Dar Entrada de Consumível",
      Description = "",
      UrlTemplate = Routes.Consumables.ADD_ACTION,
      Parameters = [new(c, "id"), new(c, "amount")],
   };
   
   public static readonly Intent ConsumableTake = new()
   {
      Id = ci,
      Type = IntentType.Post,
      Name = "Fazer Retirada de Consumível",
      Description = "",
      UrlTemplate = Routes.Consumables.TAKE_ACTION,
      Parameters = [new(c, "id"), new(c, "amount")],
   };
   
   public static readonly Intent PatrimonyList = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Patrimônios",
      Description = "",
      UrlTemplate = Routes.Patrimonies.LIST_PAGE,
      Parameters = [],
   };
   
   public static readonly Intent PatrimonyDetail = new()
   {
      Id = ci,
      Type = IntentType.Redirect,
      Name = "Página de Detalhamento de Patrimônio",
      Description = "",
      UrlTemplate = Routes.Patrimonies.DETAIL_PAGE,
      Parameters = [new(c, "id")],
   };
   
   public static readonly Intent PatrimonySwitchStatus = new()
   {
      Id = ci,
      Type = IntentType.Post,
      Name = "Alterar Status de Patrimônio",
      Description = "",
      UrlTemplate = Routes.Patrimonies.SWITCH_STATUS_ACTION,
      Parameters = [new(c, "id"), new(c, "statusId")],
   };
}
