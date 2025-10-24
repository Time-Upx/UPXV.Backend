using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.QRCodes;

public class UpdateQRCodeEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPut("/{id}", (int id, QRCodeUpdateDTO dto, UPXV_Context context, ApplicationConfiguration appConfig, IValidator<QRCodeUpdateDTO> validator) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         dto.UpdateEntity(qrcode);
         context.LoadRequirements(qrcode);

         if (!qrcode.TryGetUrl(appConfig, out string url, out ValidationResult dtoResult))
            return Results.UnprocessableEntity(dtoResult.Errors);

         context.Update(qrcode);
         context.SaveChanges();

         return Results.Ok(QRCodeDetailDTO.Of(qrcode, url));
      })
      .WithDescription("Atualiza o QR-Code com os valores novos. "+
         "Pode retornar erro (422) se os argumentos passados para a Intenção não forem adequados. ")
      .Produces<QRCodeDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound)
      .Produces<List<ValidationFailure>>(StatusCodes.Status422UnprocessableEntity);
}
