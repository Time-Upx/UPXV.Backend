using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Consumables;
using UPXV.Backend.DTOs.Page;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Consumables;

public class ListConsumablesEndpoint : IEndpoint
{
   public void MapEndpoint(IEndpointRouteBuilder app)
   {
      app.MapGet("", (int pageIndex, int pageSize, UPXV_Context context) =>
      {
         if (Validate.TryFails(out ValidationResult result,
            ( pageIndex < 0, nameof(pageIndex), "Número da página não pode ser negativo", pageIndex ),
            ( pageSize < 0, nameof(pageSize), "Tamanho da página não pode ser negativo", pageSize ) ))
            return Problems.Validation(result.Errors);

         IEnumerable<ConsumableListDTO> page = context.Consumables
            .Paging(pageIndex, pageSize)
            .ToList()
            .Peek(context.LoadRequirements)
            .Select(ConsumableListDTO.Of);

         var dto = new PageDTO<ConsumableListDTO>(page, pageIndex, pageSize);
         return Results.Ok(dto);
      })
      .WithDescription("Lista todos os Consumíveis com paginação")
      .Produces<PageDTO<ConsumableListDTO>>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
   }
}
