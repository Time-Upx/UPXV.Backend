using UPXV.Backend.API.DTOs.Page;
using UPXV.Backend.API.DTOs.Statuses;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Statuses;

public static class ListStatusAction
{
   public static IResult MapEndpoint (int pageIndex, int pageSize, UPXV_Context context)
   {
      var page = Execute(pageIndex, pageSize, context);
      return Results.Ok(page);
   }

   public static PageDTO<StatusListDTO> Execute (int pageIndex, int pageSize, UPXV_Context context)
   {
      IEnumerable<StatusListDTO> page = context.Status
         .Paging(pageIndex, pageSize)
         .ToList()
         .Peek(s => context.LoadRequirements(s))
         .Select(StatusListDTO.Of);

      return new PageDTO<StatusListDTO>(page, pageIndex, pageSize);
   }
}
