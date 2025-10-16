using FluentValidation;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;
using UPXV.Backend.Endpoints;

namespace UPXV.Backend.Common;

public static class Registry
{
   public static void ArrangeDependencies (WebApplicationBuilder builder)
   {
      new DataSetup().AddMySQL(builder);

      builder.Services.AddCors();
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      builder.Services.AddTransient(ApplicationConfiguration.Create);
      builder.Services.AddTransient(QRCodeConfiguration.Create);
      builder.Services.AddTransient(FileConfiguration.Create);

      builder.Services.AddValidatorsFromAssemblyContaining<Program>();
   }

   public static void PrepareApplication(WebApplication app)
   {
      new DataSetup().InitializeDatabase(app);
      new Routes().MapEndpoints(app);

      app.UseHttpsRedirection();
      app.UseDeveloperExceptionPage();
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "UPXV"));
   }

   public static Exception ResolutionException<TService> () => new InvalidOperationException($"Unable to resolve {typeof(TService).Name}");
}
