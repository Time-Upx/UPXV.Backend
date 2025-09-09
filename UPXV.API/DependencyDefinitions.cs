using UPXV.Data.Repositories;
using UPXV.Services;

namespace UPXV_API;

public static class DependencyDefinitions
{
   public static IServiceCollection AddDependencies (this IServiceCollection services)
   {
      services.AddScoped<ConsumableService>();
      services.AddScoped<ConsumableRepository>();

      services.AddScoped<ItemService>();
      services.AddScoped<ItemRepository>();

      services.AddScoped<PatrimonyService>();
      services.AddScoped<PatrimonyRepository>();

      services.AddScoped<StatusService>();
      services.AddScoped<StatusRepository>();

      services.AddScoped<TagService>();
      services.AddScoped<TagRepository>();
         
      services.AddScoped<UnitService>();
      services.AddScoped<UnitRepository>();

      return services;
   }
}
