using UPXV.Backend.API.Actions.Items;

namespace UPXV.Backend.API.Routes;

public class ItemRoutes : IRouter
{
   public const string ROUTE = "items";

   public const string LIST = ROUTE;

   public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
   {
      endpoints.MapGet(LIST, ItemListAction.MapEndpoint);
   }
}
