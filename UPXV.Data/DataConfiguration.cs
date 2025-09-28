using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UPXV.Data.Seeds;

namespace UPXV.Data;

public static class DataConfiguration
{
   public static IServiceCollection AddMySQL (this IServiceCollection services, IConfiguration configuration)
   {
      string enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT") ?? "Development";
      var connectionString = configuration.GetConnectionString(enviroment);
      var serverVersion = ServerVersion.AutoDetect(connectionString);

      return services.AddDbContext<UPXV_Context>(dbContextOptions => dbContextOptions
         .UseMySql(connectionString, serverVersion)
         .LogTo(Console.WriteLine, LogLevel.Information)
         .EnableSensitiveDataLogging()
         .EnableDetailedErrors());
   }

   public static void InitializeDatabase(this IServiceProvider provider)
   {
      using IServiceScope scope = provider.CreateScope();
      UPXV_Context context = scope.ServiceProvider.GetRequiredService<UPXV_Context>();

      if (context.Database.IsRelational())
      {
         context.Database.Migrate();
      }

      SeedDatabase(context);
   }
   public static void SeedDatabase (UPXV_Context context)
   {
      context.Database.EnsureCreated();

      if (!context.Consumables.Any())
         context.Consumables.AddRange(ConsumableSeeds.Data);

      if (!context.Patrimonies.Any())
         context.Patrimonies.AddRange(PatrimonySeeds.Data);

      if (!context.Status.Any())
         context.Status.AddRange(StatusSeeds.Data);

      if (!context.Units.Any())
         context.Units.AddRange(UnitSeeds.Data);

      if (!context.Tags.Any())
         context.Tags.AddRange(TagSeeds.Data);

      context.SaveChanges();
   }
}
