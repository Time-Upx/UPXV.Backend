using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.QRCodes;

public class DeleteQRCodeEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapDelete("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         context.LoadRequirements(qrcode);
         context.Remove(qrcode);
         context.SaveChanges();

         return Results.Ok(QRCodeDetailDTO.Of(qrcode));
      })
      .WithDescription("Remove o QR-Code do banco de dados")
      .Produces<QRCodeDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}

