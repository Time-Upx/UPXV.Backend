using UPXV.Backend.API.DTOs.Consumables;
using UPXV.Backend.API.DTOs.Page;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Consumables;

public static class ListConsumablesAction
{
   public static IResult MapEndpoint (int pageIndex, int pageSize, UPXV_Context context)
   {
      var page = Execute(pageIndex, pageSize, context);
      return Results.Ok(page);
   }

   public static PageDTO<ConsumableListDTO> Execute (int pageIndex, int pageSize, UPXV_Context context)
   {
      IEnumerable<ConsumableListDTO> page = context.Consumables
         .Paging(pageIndex, pageSize)
         .ToList()
         .Peek(context.LoadRequirements)
         .Select(ConsumableListDTO.Of);

      return new PageDTO<ConsumableListDTO>(page, pageIndex, pageSize);
   }
}
