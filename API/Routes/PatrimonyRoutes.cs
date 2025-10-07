using UPXV.Backend.API.Actions.Patrimonies;

namespace UPXV.Backend.API.Routes;

public class PatrimonyRoutes : IRouter
{
   public const string ROUTE = "patrimonies";
   public string LIST => ROUTE;
   public string DETAIL => ROUTE + "/{id}";
   public string CREATE => ROUTE;
   public string UPDATE => ROUTE;
   public string DELETE => ROUTE;
   public string SWITCH_STATUS => ROUTE + "/switch-status";

   public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
   {
      endpoints.MapPost(CREATE, CreatePatrimonyAction.MapEndpoint);

      endpoints.MapPut(UPDATE, UpdatePatrimonyAction.MapEndpoint);

      endpoints.MapDelete(DELETE, DeletePatrimonyAction.MapEndpoint);

      endpoints.MapGet(LIST, ListPatrimoniesAction.MapEndpoint);

      endpoints.MapGet(DETAIL, GetPatrimonyAction.MapEndpoint);

      endpoints.MapPatch(SWITCH_STATUS, SwitchPatrimonyStatusAction.MapEndpoint);
   }
}
