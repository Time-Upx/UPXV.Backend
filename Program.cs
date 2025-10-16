using UPXV.Backend.Common;

namespace UPXV.Backend;

public class Program
{
   public static void Main(string[] args)
   {
      WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

      Registry.ArrangeDependencies(builder);

      WebApplication app = builder.Build();

      Registry.PrepareApplication(app);

      app.Run();
   }
}
