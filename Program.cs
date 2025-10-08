using UPXV.Backend.API.Routes;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Common;
using System.Data;
using UPXV.Backend.Data;

namespace UPXV.Backend;

public class Program
{
   public static void Main(string[] args)
   {
      WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

      Registry.RegisterServices(builder);

      Registry.RegisterConfigurations(builder);

      Registry.RegisterRouters(builder);

      DataSetup.AddMySQL(builder);

      WebApplication app = builder.Build();

      Registry.ConfigureApplication(app);

      Registry.PrepareEnviroment(app);

      Routes.MapRoutes(app);

      app.Run();
   }
}
