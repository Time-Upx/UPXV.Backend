using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Page;
using UPXV.Backend.DTOs.QRCodes;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.QRCodes;

public class ListQRCodesEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("", (int pageIndex, int pageSize, UPXV_Context context) =>
      {
         if (Validate.TryFails(out ValidationResult result,
            (pageIndex < 0, nameof(pageIndex), "Número da página não pode ser negativo", pageIndex ),
            (pageSize < 0, nameof(pageSize), "Tamanho da página não pode ser negativo", pageSize ) ))
            return Problems.Validation(result.Errors);

         IEnumerable<QRCodeListDTO> page = context.QRCodes
            .Paging(pageIndex, pageSize)
            .ToList()
            .Peek(context.LoadRequirements)
            .Select(QRCodeListDTO.Of);

         return Results.Ok(new PageDTO<QRCodeListDTO>(page, pageIndex, pageSize));
      })
      .WithDescription("Lista todos os QR-Codes com paginação")
      .Produces<PageDTO<QRCodeListDTO>>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
}
