using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UPXV.Data.Mappings;
using UPXV.Data.Seeds;

namespace UPXV.Data;

public static class DataConfiguration
{
   public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
   {
      string enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT") ?? "Development";
      var connectionString = configuration.GetConnectionString(enviroment);
      var serverVersion = ServerVersion.AutoDetect(connectionString);

      return services.AddDbContext<UPXV_Context>(
          dbContextOptions => dbContextOptions
              .UseMySql(connectionString, serverVersion)
              .LogTo(Console.WriteLine, LogLevel.Information)
              .EnableSensitiveDataLogging()
              .EnableDetailedErrors(),
          ServiceLifetime.Transient
      );
   }

   public static void InitializeDatabase(this UPXV_Context context)
   {
      context.SeedDatabase();
   }

   public static void SeedDatabase (this UPXV_Context context)
   {
      context.Consumables.AddRange(ConsumableSeeds.Data);
      context.Patrimonies.AddRange(PatrimonySeeds.Data);
      context.Status.AddRange(StatusSeeds.Data);
      context.Units.AddRange(UnitSeeds.Data);
      context.Tags.AddRange(TagSeeds.Data);
      context.SaveChanges();
   }
}
