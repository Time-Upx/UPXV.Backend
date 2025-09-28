using UPXV.Data;
using UPXV_API;

namespace UPXV.Api;

public class Program
{
   public static void Main(string[] args)
   {
      var builder = WebApplication.CreateBuilder(args);

      IConfiguration config = builder.Configuration;

      // Add services to the container.

      builder.Services.AddCors();

      builder.Services.AddLogging();

      builder.Services.AddServices();
      builder.Services.AddRepositories();
      builder.Services.AddValidators();
      builder.Services.AddControllers();

      builder.Services.AddMySQL(config);

      var app = builder.Build();

      app.Services.InitializeDatabase();

      app.UseHttpsRedirection();

      app.UseSwagger();

      app.UseSwaggerUI(c => {
         c.SwaggerEndpoint("swagger", "Swagger");
      });

      //app.UseAuthorization();

      app.MapControllers();

      app.Run();

   }
}
