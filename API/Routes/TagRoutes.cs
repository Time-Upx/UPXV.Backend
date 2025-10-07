using UPXV.Backend.API.Actions.Tags;

namespace UPXV.Backend.API.Routes;

public class TagRoutes : IRouter
{
   public const string ROUTE = "tags";

   public const string LIST = ROUTE;
   public const string DETAIL = ROUTE + "/{id}";
   public const string CREATE = ROUTE;
   public const string UPDATE = ROUTE;
   public const string DELETE = ROUTE;

   public void ConfigureEndpoints (IEndpointRouteBuilder endpoints)
   {
      endpoints.MapPost(CREATE, CreateTagAction.MapEndpoint);

      endpoints.MapPut(UPDATE, UpdateTagAction.MapEndpoint);

      endpoints.MapDelete(DELETE, DeleteTagAction.MapEndpoint);

      endpoints.MapGet(LIST, ListTagsAction.MapEndpoint);

      endpoints.MapGet(DETAIL, GetTagAction.MapEndpoint);
   }
}
