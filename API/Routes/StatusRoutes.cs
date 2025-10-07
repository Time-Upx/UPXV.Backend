using UPXV.Backend.API.Actions.Statuses;

namespace UPXV.Backend.API.Routes;

public class StatusRoutes : IRouter
{
   public const string ROUTE = "status";

   public const string LIST = ROUTE;
   public const string DETAIL = ROUTE + "/{id}";
   public const string CREATE = ROUTE;
   public const string UPDATE = ROUTE;
   public const string DELETE = ROUTE;

   public void ConfigureEndpoints (IEndpointRouteBuilder endpoints)
   {
      endpoints.MapPost(CREATE, CreateStatusAction.MapEndpoint);

      endpoints.MapPut(UPDATE, UpdateStatusAction.MapEndpoint);

      endpoints.MapDelete(DELETE, DeleteStatusAction.MapEndpoint);

      endpoints.MapGet(LIST, ListStatusAction.MapEndpoint);

      endpoints.MapGet(DETAIL, GetStatusAction.MapEndpoint);
   }
}
