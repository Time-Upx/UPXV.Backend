using FluentValidation;
using FluentValidation.Results;

using SkiaSharp;

using UPXV.Backend.Common;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Validation;

using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp.Rendering;

using QRCode = UPXV.Backend.Entities.QRCode;

namespace UPXV.Backend.Endpoints.QRCodes;

public class ExportQRCodeEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("/{id}/export", (
         string id,
         QRCodeExportDTO? dto,
         QRCodeConfiguration qrcodeConfig,
         ApplicationConfiguration appConfig,
         UPXV_Context context,
         IValidator<QRCodeExportDTO> validator) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         if (dto is not null && !validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         string url = appConfig.ClientBaseURL + "/" + Routes.QRCodes.DETAIL_REQUEST.Replace("{id}", qrcode.Id.ToString());

         Console.WriteLine(url);

         var writer = new BarcodeWriter<SKBitmap>
         {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
               Width = dto?.Width ?? qrcodeConfig.DefaultWidth,
               Height = dto?.Height ?? qrcodeConfig.DefaultHeight,
               Margin = dto?.Margin ?? qrcodeConfig.DefaultMargin,
            },
            Renderer = new SKBitmapRenderer()
         };

         using SKBitmap bitmap = writer.Write(url);

         if (qrcodeConfig.ExportFolder is not null)
         {
            TrySaveExport(qrcodeConfig.ExportFolder, qrcode, bitmap.Copy(), qrcodeConfig, dto?.Quality);
         }
         try
         {
            MemoryStream memoryStream = new();
            bitmap.Encode(memoryStream, SKEncodedImageFormat.Png, dto?.Quality ?? qrcodeConfig.DefaultQuality);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return Results.File(memoryStream, "image/png", $"{qrcode.Id}.png");
         }
         catch (Exception ex)
         {
            return Problems.Error(ex);
         }
      })
      .WithDescription("Gera e exporta a imagem do QRCode com as especificações passadas, optando por configurações padrão caso não houver")
      .Produces(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status500InternalServerError);


   private async void TrySaveExport(string exportFolder, QRCode qrcode, SKBitmap bitmap, QRCodeConfiguration config, int? quality)
   {
      try
      {
         if (!Directory.Exists(exportFolder))
            Directory.CreateDirectory(exportFolder);

         string fileName = $"export_{DateTime.Now:yyyy-mm-dd_HH-mm-ss-ff}{qrcode.Name.ToLower().Replace(" ", "_")}";
         string filePath = Path.Combine(exportFolder, fileName);

         Console.WriteLine(filePath);

         using FileStream fileStream = new(filePath, FileMode.OpenOrCreate);

         bitmap.Encode(fileStream, SKEncodedImageFormat.Png, quality ?? config.DefaultQuality);

         Console.WriteLine($"Successfully exported qrcode at '{filePath}'");
      }
      catch (Exception e)
      {
         Console.WriteLine($"{e.GetType().Name}, {e.Message}");
      }
   }
}
