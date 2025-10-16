using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Items;
using UPXV.Backend.DTOs.Page;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Items;

public class ListItemsEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("", (int pageIndex, int pageSize, UPXV_Context context) =>
      {
         if (Validate.TryFails(out ValidationResult result,
            (pageIndex < 0, nameof(pageIndex), "Número da página não pode ser negativo", pageIndex),
            (pageSize < 0, nameof(pageSize), "Tamanho da página não pode ser negativo", pageSize)))
            return Problems.Validation(result.Errors);

         IEnumerable<ItemListDTO> consumables = context.Consumables
            .ToList()
            .Peek(context.LoadRequirements)
            .Select(ItemListDTO.Of);

         IEnumerable<ItemListDTO> patrimonies = context.Patrimonies
            .ToList()
            .Peek(context.LoadRequirements)
            .Select(ItemListDTO.Of);

         IEnumerable<ItemListDTO> items = consumables
            .Concat(patrimonies)
            .OrderBy(i =>
            {
               if (i.Consumable is not null) return i.Consumable.Name;
               if (i.Patrimony is not null) return i.Patrimony.Name;
               return "";
            })
            .Paging(pageIndex, pageSize);

         return Results.Ok(new PageDTO<ItemListDTO>(items, pageIndex, pageSize));
      })
      .WithDescription("Lista todos os Items (patrimônios / consumíveis) ordenados por seus nomes, com paginação")
      .Produces<PageDTO<ItemListDTO>>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
}
