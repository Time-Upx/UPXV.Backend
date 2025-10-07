using UPXV.Backend.API.DTOs.Page;
using UPXV.Backend.API.DTOs.Patrimonies;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Patrimonies;

public static class ListPatrimoniesAction
{
   public static IResult MapEndpoint (int pageIndex, int pageSize, UPXV_Context context)
   {
      var page = Execute(pageIndex, pageSize, context);
      return Results.Ok(page);
   }

   public static PageDTO<PatrimonyListDTO> Execute (int pageIndex, int pageSize, UPXV_Context context)
   {
      IEnumerable<PatrimonyListDTO> page = context.Patrimonies
         .Paging(pageIndex, pageSize)
         .ToList()
         .Peek(context.LoadRequirements)
         .Select(PatrimonyListDTO.Of);

      return new PageDTO<PatrimonyListDTO>(page, pageIndex, pageSize);
   }
}
