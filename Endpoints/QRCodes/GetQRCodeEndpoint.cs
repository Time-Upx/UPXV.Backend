using UPXV.Backend.Common;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.QRCodes;

public class GetQRCodeEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("/{id}", (string id, UPXV_Context context, ApplicationConfiguration appConfig) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         context.LoadRequirements(qrcode);

         var urlAttempt = qrcode.GetUrl(appConfig.ClientBaseURL);

         if (urlAttempt.TryGetFailure(out Exception failure)) 
            Problems.Error(failure);

         var qrCodeDetail = QRCodeDetailDTO.Of(qrcode);

         qrCodeDetail.Url = urlAttempt.GetSuccessValueOrThrow();

         return Results.Ok(qrCodeDetail);
      })
      .Produces<QRCodeDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound)
      .Produces(StatusCodes.Status500InternalServerError);
}
