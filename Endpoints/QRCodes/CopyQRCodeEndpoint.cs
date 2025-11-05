
using FluentValidation.Results;

using UPXV.Backend.Common;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.QRCodes;

public class CopyQRCodeEndpoint : IEndpoint
{
   private const string _copySuffix = " - Cópia";
   private const int _copySuffixLength = 8;
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("/{id}/copy", (string id, UPXV_Context context, ApplicationConfiguration appConfig) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         context.LoadRequirements(qrcode);
         int copyCount = context.QRCodes
            .Where(qr => qr.Name.Substring(0, qr.Name.Length - _copySuffixLength).Equals(qrcode.Name))
            .Count();

         string suffix = copyCount > 0 ? $"{_copySuffix} ({copyCount + 1})" : _copySuffix;
         QRCode copy = qrcode.CopyToNew(suffix);

         if (!QRCodeDetailDTO.TryCreate(copy, appConfig, out var details, out var problem))
            return Results.UnprocessableEntity(problem.Errors);

         context.Add(copy);
         context.SaveChanges();

         details.Id = copy.Id;

         return Results.Ok(details);
      })
      .WithDescription("Cria uma cópia do código QR e o salva no banco como uma nova entidade")
      .Produces<QRCodeDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound)
      .Produces<List<ValidationFailure>>(StatusCodes.Status422UnprocessableEntity);
}
