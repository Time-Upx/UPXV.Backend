
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.QRCodes;

public class IncreaseQRCodeUseEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPatch("{id}/increase-use", (string id, UPXV_Context context) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         qrcode.TimesUsed++;
         context.SaveChanges();

         return Results.Ok(qrcode.TimesUsed);
      })
      .WithDescription("Increases the use count of a QR code by 1.")
      .Produces<int>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
