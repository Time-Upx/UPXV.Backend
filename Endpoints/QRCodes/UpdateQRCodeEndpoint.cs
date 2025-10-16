using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.QRCodes;

public class UpdateQRCodeEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPut("/{id}", (int id, QRCodeUpdateDTO dto, UPXV_Context context, IValidator<QRCodeUpdateDTO> validator) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         dto.UpdateEntity(qrcode);

         context.Update(qrcode);
         context.SaveChanges();

         context.LoadRequirements(qrcode);
         return Results.Ok(QRCodeDetailDTO.Of(qrcode));
      })
      .WithDescription("Atualiza o QR-Code com os valores novos")
      .Produces<QRCodeDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
