using UPXV.Backend.API.DTOs.Page;
using UPXV.Backend.API.DTOs.Units;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Units;

public static class ListUnitsAction
{
   public static IResult MapEndpoint (int pageIndex, int pageSize, UPXV_Context context)
   {
      var page = Execute(pageIndex, pageSize, context);
      return Results.Ok(page);
   }

   public static PageDTO<UnitListDTO> Execute (int pageIndex, int pageSize, UPXV_Context context)
   {
      IEnumerable<UnitListDTO> page = context.Units
         .Paging(pageIndex, pageSize)
         .ToList()
         .Peek(context.LoadRequirements)
         .Select(UnitListDTO.Of);

      return new PageDTO<UnitListDTO>(page, pageIndex, pageSize);
   }
}
