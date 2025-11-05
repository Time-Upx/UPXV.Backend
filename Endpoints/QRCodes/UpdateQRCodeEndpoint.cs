using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

using static UPXV.Backend.Endpoints.Routes;

namespace UPXV.Backend.Endpoints.QRCodes;

public class UpdateQRCodeEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPut("/{id}", (string id, QRCodeUpdateDTO dto, UPXV_Context context, ApplicationConfiguration appConfig, IValidator<QRCodeUpdateDTO> validator) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         dto.UpdateEntity(qrcode);

         Intent intent = context.Intents.Find(dto.IntentId)!;
         intent.Parameters = context.IntentParameters
            .Where(ip => ip.IntentId == intent.Id)
            .ToList();

         string url;
         if (dto.IntentArguments is not null)
         {
            if (!QRCodeExtensions.TryBuildUrl(intent, appConfig, dto.IntentArguments, out url, out ValidationResult dtoResult))
               return Results.UnprocessableEntity(dtoResult.Errors);

            context.QRCodeArguments.RemoveRange(context.QRCodeArguments
               .Where(qa => qa.QRCodeId == qrcode.Id));

            qrcode.Arguments = dto.IntentArguments.ToEntities(qrcode.Id, qrcode);
         } 
         else
         {
            qrcode.Arguments = context.QRCodeArguments
               .Where(arg => arg.QRCodeId == qrcode.Id)
               .ToList();

            if (!qrcode.TryGetUrl(appConfig, out url, out ValidationResult urlResult))
               return Results.UnprocessableEntity(urlResult.Errors);
         }

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
