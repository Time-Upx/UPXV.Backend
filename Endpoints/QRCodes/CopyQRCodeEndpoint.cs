
using FluentValidation.Results;

using UPXV.Backend.Common;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.QRCodes;

public class CopyQRCodeEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("/{id}/copy", (int id, UPXV_Context context, ApplicationConfiguration appConfig) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         QRCode copy = qrcode.CopyToNew();
         context.LoadRequirements(copy);

         if (!QRCodeDetailDTO.TryCreate(qrcode, appConfig, out var details, out var problem))
            return Results.UnprocessableEntity(problem.Errors);

         context.Add(copy);
         context.SaveChanges();

         return Results.Ok(details);
      })
      .WithDescription("Cria uma cópia do código QR e o salva no banco como uma nova entidade")
      .Produces<QRCodeDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound)
      .Produces<List<ValidationFailure>>(StatusCodes.Status422UnprocessableEntity);
}
