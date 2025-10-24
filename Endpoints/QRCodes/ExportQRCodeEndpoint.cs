using FluentValidation;
using FluentValidation.Results;
using SkiaSharp;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;
using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp.Rendering;

namespace UPXV.Backend.Endpoints.QRCodes;

public class ExportQRCodeEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("/{id}/export", (
         int id,
         QRCodeExportDTO dto,
         QRCodeConfiguration qrcodeConfig,
         ApplicationConfiguration appConfig,
         UPXV_Context context,
         IValidator<QRCodeExportDTO> validator) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         string url = (appConfig.ClientBaseURL + Routes.QRCodes.DETAIL_REQUEST)
            .Replace("{id}", qrcode.Id.ToString());

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

         SKBitmap bitmap = writer.Write(url);

         try
         {
            MemoryStream stream = new();
            bitmap.Encode(stream, SKEncodedImageFormat.Png, dto.Quality ?? qrcodeConfig.DefaultQuality);
            return Results.File(stream, "image/png", $"{qrcode.Id}.png");
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
}
