using UPXV.Backend.Data;
using UPXV.Backend.API.Routes;
using UPXV.Backend.Common.Configuration;

namespace UPXV.Backend;

public class Program
{
   public static void Main(string[] args)
   {
      var builder = WebApplication.CreateBuilder(args);

      IConfiguration config = builder.Configuration;

      builder.Services.AddCors();

      builder.Services.AddMySQL(config);

      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      builder.Services.SetupConfigurations();

      var app = builder.Build();

      app.Services.InitializeDatabase();

      app.UseHttpsRedirection();

      app.UseSwagger();

      app.UseSwaggerUI(c => {
         c.SwaggerEndpoint("v1/swagger.json", "UPXV");
      });

      Routes.MapRoutes(app);

      app.Run();
   }
}
