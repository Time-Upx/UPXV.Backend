using UPXV.Backend.API.Actions.Units;

namespace UPXV.Backend.API.Routes;

public class UnitRoutes : IRouter
{
   public const string ROUTE = "units";

   public const string LIST = ROUTE;
   public const string DETAIL = ROUTE + "/{id}";
   public const string CREATE = ROUTE;
   public const string UPDATE = ROUTE;
   public const string DELETE = ROUTE;

   public void ConfigureEndpoints (IEndpointRouteBuilder endpoints)
   {
      endpoints.MapPost(CREATE, CreateUnitAction.MapEndpoint);

      endpoints.MapPut(UPDATE, UpdateUnitAction.MapEndpoint);

      endpoints.MapDelete(DELETE, DeleteUnitAction.MapEndpoint);

      endpoints.MapGet(LIST, ListUnitsAction.MapEndpoint);

      endpoints.MapGet(DETAIL, GetUnitAction.MapEndpoint);
   }
}
