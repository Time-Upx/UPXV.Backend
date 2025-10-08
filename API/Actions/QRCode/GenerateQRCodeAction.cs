using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Http.Extensions;

using SkiaSharp;

using UPXV.Backend.API.DTOs.QRCode;
using UPXV.Backend.Common.Configuration;

using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp.Rendering;

namespace UPXV.Backend.API.Actions.QRCode;

public static class GenerateQRCodeAction
{

   // TODO: Change it to a Middleware
   public static RouteHandlerBuilder MapQRCode (
        this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string pattern,
        QRCodeConfiguration config)
   {
      return endpoints.MapPost(config.AppendExtension(pattern), MapEndpoint);
   }
   public static IResult MapEndpoint (CreateQRCodeDTO dto, QRCodeConfiguration qrcodeConfig, HttpRequest request)
   {
      return Execute(dto, qrcodeConfig, request).Either(
         file => Results.File(file.Content, "image/png", file.Name),
         failure => failure switch
         {
            DirectoryNotFoundException e => Results.Problem(e.Message, statusCode: 500),
            Exception e => Results.Problem(e.Message, statusCode: 500),
         });
   }
   public static Attempt<(byte[] Content, string Name), Exception> Execute (CreateQRCodeDTO dto, QRCodeConfiguration qrcodeConfig, HttpRequest request)
   {
      if (!Directory.Exists(qrcodeConfig.DestinationPath))
         return new DirectoryNotFoundException("Pasta de códigos QR não encontrada");

      string url = request.GetEncodedUrl();

      string fileName = url.Replace("/", "_").Replace("\\", "_") + ".png";

      string endPath = Path.Combine(qrcodeConfig.DestinationPath, fileName);

      if (!File.Exists(endPath))
      {
         var writer = CreateWriter(qrcodeConfig, dto);

         SKBitmap bitmap = writer.Write(url);

         using (FileStream stream = File.OpenWrite(endPath))
         {
            bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
         }
      }

      byte[] content;
      try
      {
         content = File.ReadAllBytes(endPath);
      }
      catch (Exception ex)
      {
         return ex;
      }
      return (content, fileName);
   }

   private static IBarcodeWriter<SKBitmap> CreateWriter(QRCodeConfiguration configuration, CreateQRCodeDTO? dto = null)
   {
      return new BarcodeWriter<SKBitmap>
      {
         Format = BarcodeFormat.QR_CODE,
         Options = new EncodingOptions
         {
            Width = dto?.Width ?? configuration.DefaultWidth,
            Height = dto?.Height ?? configuration.DefaultHeight,
            Margin = dto?.Margin ?? configuration.DefaultMargin,
         },
         Renderer = new SKBitmapRenderer()
      };
   }
}
