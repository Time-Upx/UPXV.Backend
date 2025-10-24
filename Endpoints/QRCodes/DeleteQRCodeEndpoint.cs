using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.QRCodes;

public class DeleteQRCodeEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapDelete("/{id}", (int id, UPXV_Context context, ApplicationConfiguration appConfig) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         context.LoadRequirements(qrcode);
         context.Remove(qrcode);
         context.SaveChanges();

         if (!QRCodeDetailDTO.TryCreate(qrcode, appConfig, out var details, out ValidationResult result))
            return Results.UnprocessableEntity(result.Errors);

         return Results.Ok(details);
      })
      .WithDescription("Remove o QR-Code do banco de dados. " +
         "Pode retornar erro (422) se os arguments passados para a Intenção não forem adequados. " +
         "Caso impossível pois também é verificado na criação.")
      .Produces<QRCodeDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound)
      .Produces<List<ValidationFailure>>(StatusCodes.Status422UnprocessableEntity);
}

