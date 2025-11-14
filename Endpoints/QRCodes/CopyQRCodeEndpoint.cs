
using FluentValidation.Results;

using Microsoft.EntityFrameworkCore;

using System.Text.RegularExpressions;

using UPXV.Backend.Common;
using UPXV.Backend.Common.Configuration;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.QRCodes;

public class CopyQRCodeEndpoint : IEndpoint
{
   private const string _copySuffix = " - Cópia";
   private string QueryPattern (string baseName) => $"^{Regex.Escape(baseName)}({Regex.Escape(_copySuffix)})?( \\(\\d+\\))?$";
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("/{id}/copy", (string id, UPXV_Context context, ApplicationConfiguration appConfig) =>
      {
         if (!context.TryFind(out QRCode qrcode, id))
            return Problems.NotFound<QRCode>(id);

         context.LoadRequirements(qrcode);

         int copyIndex = qrcode.Name.LastIndexOf(_copySuffix);
         string baseName = copyIndex == -1
            ? qrcode.Name
            : qrcode.Name.Substring(0, copyIndex);
         string pattern = QueryPattern(baseName);

         var memoryEvaluation = context.QRCodes
            .Where(qr => qr.Name.StartsWith(baseName)) // Database filter
            .ToList(); // Switch to client-side evaluation

         var copyCount = memoryEvaluation
            .Where(qr => Regex.IsMatch(qr.Name, pattern))
            .Count();

         string suffix = copyCount > 0 ? $"{_copySuffix} ({copyCount})" : _copySuffix;
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
