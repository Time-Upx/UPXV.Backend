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
         if (!validator.TryValidate(dto, out ValidationResult validationResult))
            return Problems.Validation(validationResult.Errors);

         Intent intent = context.Intents.Find(dto.IntentId)!;
         intent.Parameters = context.IntentParameters
            .Where(ip => ip.IntentId == intent.Id)
            .ToList();

         if (!dto.TryBuildEntity(intent, dto.IntentArguments, appConfig, out QRCode qrcode, out string url, out ValidationResult entityResult))
            return Results.UnprocessableEntity(entityResult.Errors);

         qrcode.Arguments = dto.IntentArguments
            .Select(kv => new QRCodeArgument()
            {
               QRCode = qrcode,
               Parameter = kv.Key,
               Value = kv.Value
            })
            .ToList();

         context.Add(qrcode);
         context.SaveChanges();
         context.LoadRequirements(qrcode);
         
         return Results.Ok(QRCodeDetailDTO.Of(qrcode, url));
      })
      .WithDescription("Salva um novo QRCode no banco de dados. " +
         "Pode retornar erro (422) se os argumentos passados para a Intenção não forem adequados")
      .Produces<QRCodeDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest)
      .Produces<List<ValidationFailure>>(StatusCodes.Status422UnprocessableEntity);

}
