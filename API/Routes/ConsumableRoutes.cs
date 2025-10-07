using UPXV.Backend.API.Actions.Consumables;

namespace UPXV.Backend.API.Routes;

public class ConsumableRoutes : IRouter
{
   public const string ROUTE = "consumables";
   public const string _ = "";

   public string DETAIL => "/{id}";
   public string ADD => "/add/{id}";
   public string TAKE => "/take/{id}";

   public void ConfigureEndpoints (IEndpointRouteBuilder endpoints)
   {
      var route = endpoints.MapGroup(ROUTE);

      route.MapPost(_, CreateConsumableAction.MapEndpoint);

      route.MapPut(_, UpdateConsumableAction.MapEndpoint);

      route.MapDelete(_, DeleteConsumableAction.MapEndpoint);

      route.MapGet(_, ListConsumablesAction.MapEndpoint);

      route.MapGet(DETAIL, GetConsumableAction.MapEndpoint);

      route.MapPatch(ADD, AddConsumableAction.MapEndpoint);

      route.MapPatch(TAKE, TakeConsumableAction.MapEndpoint);
   }
}
