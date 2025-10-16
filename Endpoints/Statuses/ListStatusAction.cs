using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Page;
using UPXV.Backend.DTOs.Statuses;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Statuses;

public class ListStatusEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("", (int pageIndex, int pageSize, UPXV_Context context) =>
      {
         if (Validate.TryFails(out ValidationResult result,
            (pageIndex < 0, nameof(pageIndex), "Número da página não pode ser negativo", pageIndex ),
            (pageSize < 0, nameof(pageSize), "Tamanho da página não pode ser negativo", pageSize ) ))
            return Problems.Validation(result.Errors);

         IEnumerable<StatusListDTO> page = context.Status
            .Paging(pageIndex, pageSize)
            .ToList()
            .Peek(context.LoadRequirements)
            .Select(StatusListDTO.Of);

         return Results.Ok(new PageDTO<StatusListDTO>(page, pageIndex, pageSize));
      })
      .WithDescription("Lista todos os Status com paginação")
      .Produces<PageDTO<StatusListDTO>>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
}
