using SkiaSharp;
using UPXV.Backend.API.DTOs.QRCode;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Configuration;
using ZXing;
using ZXing.Common;
using ZXing.SkiaSharp.Rendering;

namespace UPXV.Backend.API.Actions.QRCode;

public static class GenerateQRCodeAction
{
   public static Attempt<Exception> Execute (QRCodeConfiguration config, CreateQRCodeDTO dto)
   {
      var writer = CreateWriter(config, dto);

      SKBitmap bitmap = writer.Write(dto.Data);

      using (FileStream stream = File.OpenWrite(filePath))
      {
         bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
      }
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
