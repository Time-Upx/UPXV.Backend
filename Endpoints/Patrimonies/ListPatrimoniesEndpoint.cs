using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Page;
using UPXV.Backend.DTOs.Patrimonies;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Patrimonies;

public class ListPatrimoniesEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("", (int pageIndex, int pageSize, UPXV_Context context) =>
      {
         if (Validate.TryFails(out ValidationResult result,
            (pageIndex < 0, nameof(pageIndex), "Número da página não pode ser negativo", pageIndex ),
            (pageSize < 0, nameof(pageSize), "Tamanho da página não pode ser negativo", pageSize ) ))
            return Problems.Validation(result.Errors);

         IEnumerable<PatrimonyListDTO> page = context.Patrimonies
            .Paging(pageIndex, pageSize)
            .ToList()
            .Peek(context.LoadRequirements)
            .Select(PatrimonyListDTO.Of);

         return Results.Ok(new PageDTO<PatrimonyListDTO>(page, pageIndex, pageSize));
      })
      .WithDescription("Lista todos os Patrimônios com paginação")
      .Produces<PageDTO<PatrimonyListDTO>>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
}
