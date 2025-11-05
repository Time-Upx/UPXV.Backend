using FluentValidation;
using FluentValidation.Results;
using SkiaSharp;
using System.Reflection;
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
         string id,
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

         string url = (appConfig.ClientBaseURL + "/" + Routes.QRCodes.DETAIL_REQUEST)
            .Replace("{id}", qrcode.Id.ToString());

         var writer = new BarcodeWriter<SKBitmap>
         {
            Format = BarcodeFormat.QR_CODE,
            Options = new EncodingOptions
            {
               Width = dto.Width ?? qrcodeConfig.DefaultWidth,
               Height = dto.Height ?? qrcodeConfig.DefaultHeight,
               Margin = dto.Margin ?? qrcodeConfig.DefaultMargin,
            },
            Renderer = new SKBitmapRenderer()
         };

         using SKBitmap bitmap = writer.Write(url);

         string assemblyPath = Assembly.GetExecutingAssembly().Location;
         string assemblyDirectory = Path.GetDirectoryName(assemblyPath)!;
         string fileName = 
         $"export_{DateTime.Now:yyyy-mm-dd_hh-mm-ss-ff)}{(qrcode.Name is null ? "" : "_" + qrcode.Name.Replace(" ", "_"))}";
         string filePath = Path.Combine(assemblyDirectory, "exports", fileName);
         Console.WriteLine(filePath);
         try
         {
            var fileMap = bitmap.Copy();
            Task.Run(() =>
            {
               try
               {
                  using FileStream fileStream = new(filePath, FileMode.OpenOrCreate);
                  fileMap.Encode(fileStream, SKEncodedImageFormat.Png, dto.Quality ?? qrcodeConfig.DefaultQuality);
               } 
               catch (Exception e) 
               { 
                  Console.WriteLine($"{e.GetType().Name}, {e.Message}");
               }
            });
            MemoryStream memoryStream = new();
            bitmap.Encode(memoryStream, SKEncodedImageFormat.Png, dto.Quality ?? qrcodeConfig.DefaultQuality);
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
}
