namespace UPXV.Backend.API.Routes;

public class Routes : IRouter
{
   public static void MapRoutes(IEndpointRouteBuilder endpoints)
   {
      new Routes().ConfigureEndpoints(endpoints);
   }
   public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
   {
      endpoints.Configure<ConsumableRoutes>();
      endpoints.Configure<PatrimonyRoutes>();
      endpoints.Configure<StatusRoutes>();
      endpoints.Configure<UnitRoutes>();
      endpoints.Configure<ItemRoutes>();
      endpoints.Configure<TagRoutes>();
   }
}

public static class RouteExtensions
{
   public static void Configure (
      this IEndpointRouteBuilder endpoints,
      Action<IEndpointRouteBuilder> configuration)
      => configuration(endpoints);
   public static void Configure (
      this IEndpointRouteBuilder endpoints,
      IRouter router)
      => router.ConfigureEndpoints(endpoints);
   public static void Configure<TRouter> (
      this IEndpointRouteBuilder endpoints)
      where TRouter : IRouter, new()
      => new TRouter().ConfigureEndpoints(endpoints);
}
