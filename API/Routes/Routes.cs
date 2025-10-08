namespace UPXV.Backend.API.Routes;

public class Routes : IRouter
{
   public static void MapRoutes(WebApplication app) => new Routes(app).ConfigureEndpoints(app);
   public void ConfigureEndpoints (IEndpointRouteBuilder endpoints)
   {
      endpoints.Configure(_consumables);
      endpoints.Configure(_patrimonies);
      endpoints.Configure(_status);
      endpoints.Configure(_units);
      endpoints.Configure(_items);
      endpoints.Configure(_tags);
   }

   public Routes (WebApplication app) : this(
      app.Services.GetRequiredService<ConsumableRoutes>(),
      app.Services.GetRequiredService<PatrimonyRoutes>(),
      app.Services.GetRequiredService<StatusRoutes>(),
      app.Services.GetRequiredService<UnitRoutes>(),
      app.Services.GetRequiredService<ItemRoutes>(),
      app.Services.GetRequiredService<TagRoutes>() ) {}

   public Routes (
      ConsumableRoutes consumables,
      PatrimonyRoutes patrimonies,
      StatusRoutes status,
      UnitRoutes units,
      ItemRoutes items,
      TagRoutes tags
   ) {
      _consumables = consumables;
      _patrimonies = patrimonies;
      _status = status;
      _units = units;
      _items = items;
      _tags = tags;
   }

   private ConsumableRoutes _consumables;
   private PatrimonyRoutes _patrimonies;
   private StatusRoutes _status;
   private UnitRoutes _units;
   private ItemRoutes _items;
   private TagRoutes _tags;
}

public static class RouteExtensions
{
   public static void Configure (
      this IEndpointRouteBuilder endpoints,
      IRouter router)
      => router.ConfigureEndpoints(endpoints);
}
