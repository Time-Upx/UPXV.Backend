using SkiaSharp;
using UPXV.Backend.API.DTOs.QRCode;
using UPXV.Backend.Common.Configuration;
using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp.Rendering;

namespace UPXV.Backend.Common;

public static class Registry
{
   public static void RegisterAll (this IServiceCollection services)
   {
      services.AddTransient(FileConfiguration.Create);
      services.AddTransient(QRCodeConfiguration.Create);
   }

   public static Exception ResolutionException<TService> () => new InvalidOperationException($"Unable to resolve {typeof(TService).Name}");
}
