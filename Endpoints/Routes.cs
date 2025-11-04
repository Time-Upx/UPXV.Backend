using UPXV.Backend.Endpoints.Consumables;
using UPXV.Backend.Endpoints.Intents;
using UPXV.Backend.Endpoints.Items;
using UPXV.Backend.Endpoints.Patrimonies;
using UPXV.Backend.Endpoints.QRCodes;
using UPXV.Backend.Endpoints.Statuses;
using UPXV.Backend.Endpoints.Tags;
using UPXV.Backend.Endpoints.Units;

namespace UPXV.Backend.Endpoints;

public static class RouteExtensions
{
   public static void ConfigureEndpoint (this IEndpointRouteBuilder app, IEndpoint endpoint) => endpoint.MapEndpoint(app);
}
public class Routes
{
   public static void MapEndpoints (IEndpointRouteBuilder app)
   {
      app.MapGet("/", () => "UPXV Backend is running!");

      foreach (var group in _routes)
      {
         var route = app
            .MapGroup("api/" + group.Key.ToLower())
            .WithTags(group.Key);

         foreach (var endpoint in group.Value)
            route.ConfigureEndpoint(endpoint);
      }
   }

   private static Dictionary<string, List<IEndpoint>> _routes = new()
   {
      { Consumables.GROUP, new() { 
         { new CreateConsumableEndpoint() },
         { new DeleteConsumableEndpoint() },
         { new UpdateConsumableEndpoint() },
         { new ListConsumablesEndpoint() },
         { new TakeConsumableEndpoint() },
         { new AddConsumableEndpoint() },
         { new GetConsumableEndpoint() },
      } },
      { Patrimonies.GROUP, new() {
         { new SwitchPatrimonyStatusEndpoint() },
         { new ListPatrimoniesEndpoint() },
         { new CreatePatrimonyEndpoint() },
         { new DeletePatrimonyEndpoint() },
         { new UpdatePatrimonyEndpoint() },
         { new GetPatrimonyEndpoint() },
      } },
      { QRCodes.GROUP, new() {
         { new GenerateQRCodeActivationCodeEndpoint() },
         { new IncreaseQRCodeUseEndpoint() },
         { new CreateQRCodeEndpoint() },
         { new DeleteQRCodeEndpoint() },
         { new UpdateQRCodeEndpoint() },
         { new ExportQRCodeEndpoint() },
         { new ListQRCodesEndpoint() },
         { new CopyQRCodeEndpoint() },
         { new GetQRCodeEndpoint() },
      } },
      { Status.GROUP, new() {
         { new CreateStatusEndpoint() },
         { new DeleteStatusEndpoint() },
         { new UpdateStatusEndpoint() },
         { new ListStatusEndpoint() },
         { new GetStatusEndpoint() },
      } },
      { Units.GROUP, new() {
         { new CreateUnitEndpoint() },
         { new DeleteUnitEndpoint() },
         { new UpdateUnitEndpoint() },
         { new ListUnitsEndpoint() },
         { new GetUnitEndpoint() },
      } },
      { Tags.GROUP, new() {
         { new CreateTagEndpoint() },
         { new DeleteTagEndpoint() },
         { new UpdateTagEndpoint() },
         { new ListTagsEndpoint() },
         { new GetTagEndpoint() },
      } },
      { Intents.GROUP, new() {
         { new ListIntentsEndpoint() },
         { new GetIntentEndpoint() },
      } },
      { Items.GROUP, new() {
         { new ListItemsEndpoint() },
      } },
   };

   public static class Consumables
   {
      public const string GROUP = "Consumables";
      public const string LIST_PAGE = "consumables";
      public const string DETAIL_PAGE = "consumables/{id}";
      public const string TAKE_ACTION = "api/consumables/{id}/take?amount={amount}";
      public const string ADD_ACTION = "api/consumables/{id}/add?amount={amount}";
   }
   public static class Patrimonies
   {
      public const string GROUP = "Patrimonies";
      public const string LIST_PAGE = "patrimonies";
      public const string DETAIL_PAGE = "patrimonies/{id}";
      public const string SWITCH_STATUS_ACTION = "api/patrimonies/{id}/switch-status?statusId={statusId}";
   }
   public static class Units
   {
      public const string GROUP = "Units";
      public const string LIST_PAGE = "units";
      public const string DETAIL_PAGE = "units/{id}";
   }
   public static class Tags
   {
      public const string GROUP = "Tags";
      public const string LIST_PAGE = "tags";
      public const string DETAIL_PAGE = "tags/{id}";
   }
   public static class Status
   {
      public const string GROUP = "Status";
      public const string LIST_PAGE = "statuses";
      public const string DETAIL_PAGE = "statuses/{id}";
   }
   public static class QRCodes
   {
      public const string GROUP = "QRCodes";
      public const string LIST_PAGE = "qrcodes";
      public const string DETAIL_PAGE = "qrcodes/{id}";
      public const string DETAIL_REQUEST = "qrcodes/{id}/read";
   }
   public static class Intents
   {
      public const string GROUP = "Intents";
   }
   public static class Items
   {
      public const string GROUP = "Items";
      public const string LIST_PAGE = "items";
   }
}