using System;

using Microsoft.AspNetCore.Http.Extensions;

using UPXV.Backend.API.Actions.QRCode;
using UPXV.Backend.Common.Configuration;

namespace UPXV.Backend.API;

public class QRCodeGeneratorMiddleware
{
   private readonly RequestDelegate _next;
   private readonly QRCodeConfiguration _qrCodeConfiguration;

   public RequestTimingMiddleware (RequestDelegate next, QRCodeConfiguration qrCodeConfiguration)
   {
      _next = next;
      _qrCodeConfiguration = qrCodeConfiguration;
   }

   public async Task InvokeAsync (HttpContext context)
   {
      string url = context.Request.GetEncodedUrl();

      if (!url.EndsWith(_qrCodeConfiguration.UrlExtension))
      {
         await _next(context);
         return;
      }

      GenerateQRCodeAction.Execute(
         new DTOs.QRCode.CreateQRCodeDTO(),
         _qrCodeConfiguration,
         context.Request);

   }
}
