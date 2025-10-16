using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Intents;
using UPXV.Backend.DTOs.Page;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Intents;

public class ListIntentsEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("", (int pageIndex, int pageSize, UPXV_Context context) =>
      {
         if (Validate.TryFails(out ValidationResult problem,
            (pageIndex < 0, nameof(pageIndex), "Número da página não pode ser negativo", pageIndex),
            (pageSize < 0, nameof(pageSize), "Tamanho da página não pode ser negativo", pageSize)))
            return Problems.Validation(problem.Errors);


         IEnumerable<IntentListDTO> page = context.Intents
            .Paging(pageIndex, pageSize)
            .ToList()
            .Peek(context.LoadRequirements)
            .Select(IntentListDTO.Of);

         return Results.Ok(new PageDTO<IntentListDTO>(page, pageIndex, pageSize));
      })
      .WithDescription("Lista todos as Intenções com paginação")
      .Produces<PageDTO<IntentListDTO>>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
}
