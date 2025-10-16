using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.QRCodes;

public class CreateQRCodeEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("", (QRCodeCreateDTO dto, UPXV_Context context, ApplicationConfiguration appConfig, IValidator<QRCodeCreateDTO> validator) =>
      {
         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         QRCode qrcode = dto.BuildEntity();

         context.Add(qrcode);
         context.SaveChanges();
         context.LoadRequirements(qrcode);

         var urlAttempt = qrcode.GetUrl(appConfig.ClientBaseURL);

         if (urlAttempt.TryGetFailure(out Exception failure)) 
            return Problems.Error(failure);

         var qrCodeDetail = QRCodeDetailDTO.Of(qrcode);

         qrCodeDetail.Url = urlAttempt.GetSuccessValueOrThrow();

         return Results.Ok(qrCodeDetail);
      })
      .WithDescription("Salva um novo QRCode no banco de dados")
      .Produces<QRCodeDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest)
      .Produces(StatusCodes.Status500InternalServerError);
}
