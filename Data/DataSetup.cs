using Microsoft.EntityFrameworkCore;

using UPXV.Backend.Data.Seeds;

namespace UPXV.Backend.Data;

public static class DataSetup
{
   public static IServiceCollection AddMySQL (WebApplicationBuilder builder)
   {
      string enviroment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIROMENT") ?? "Development";
      var connectionString = builder.Configuration.GetConnectionString(enviroment);
      var serverVersion = ServerVersion.AutoDetect(connectionString);

      return builder.Services.AddDbContext<UPXV_Context>(dbContextOptions => dbContextOptions
         .UseMySql(connectionString, serverVersion)
         .LogTo(Console.WriteLine, LogLevel.Information)
         .EnableSensitiveDataLogging()
         .EnableDetailedErrors());
   }

   public static void InitializeDatabase(WebApplication app)
   {
      using IServiceScope scope = app.Services.CreateScope();
      UPXV_Context context = scope.ServiceProvider.GetRequiredService<UPXV_Context>();

      if (context.Database.IsRelational())
      {
         context.Database.Migrate();
      }

      SeedDatabase(context);
   }

   private static void SeedDatabase (UPXV_Context context)
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
