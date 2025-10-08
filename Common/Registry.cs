using UPXV.Backend.API.Routes;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;

namespace UPXV.Backend.Common;

public static class Registry
{
   public static void RegisterServices (WebApplicationBuilder builder)
   {
      builder.Services.AddCors();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();
   }

   public static void ConfigureApplication(WebApplication app)
   {
      app.UseHttpsRedirection();
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "UPXV"));
   }

   public static void PrepareEnviroment(WebApplication app)
   {
      FileConfiguration fileConfig = app.Services.GetRequiredService<FileConfiguration>();
      QRCodeConfiguration qrcodeConfig = app.Services.GetRequiredService<QRCodeConfiguration>();

      if (!Directory.Exists(fileConfig.TemporaryFolderPath))
         Directory.CreateDirectory(fileConfig.TemporaryFolderPath);

      if (!Directory.Exists(fileConfig.DestinationFolderBasePath))
         Directory.CreateDirectory(fileConfig.DestinationFolderBasePath);

      if (!Directory.Exists(qrcodeConfig.DestinationPath))
         Directory.CreateDirectory(qrcodeConfig.DestinationPath);
   }

   public static void RegisterConfigurations(WebApplicationBuilder builder)
   {
      builder.Services.AddTransient(ApplicationConfiguration.Create);
      builder.Services.AddTransient(QRCodeConfiguration.Create);
      builder.Services.AddTransient(FileConfiguration.Create);
   }
   public static void RegisterRouters (WebApplicationBuilder builder)
   {
      builder.Services.AddTransient<ConsumableRoutes>();
      builder.Services.AddTransient<PatrimonyRoutes>();
      builder.Services.AddTransient<StatusRoutes>();
      builder.Services.AddTransient<ItemRoutes>();
      builder.Services.AddTransient<UnitRoutes>();
      builder.Services.AddTransient<TagRoutes>();
      builder.Services.AddTransient<Routes>();
   }

   public static Exception ResolutionException<TService> () => new InvalidOperationException($"Unable to resolve {typeof(TService).Name}");
}
