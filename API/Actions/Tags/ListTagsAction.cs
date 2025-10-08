using UPXV.Backend.API.DTOs.Page;
using UPXV.Backend.API.DTOs.Tags;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Tags;

public static class ListTagsAction
{
   public static IResult MapEndpoint (int pageIndex, int pageSize, UPXV_Context context)
   {
      var page = Execute(pageIndex, pageSize, context);
      return Microsoft.AspNetCore.Http.Results.Ok(page);
   }

   public static PageDTO<TagListDTO> Execute (int pageIndex, int pageSize, UPXV_Context context)
   {
      IEnumerable<TagListDTO> page = context.Tags
         .Paging(pageIndex, pageSize)
         .ToList()
         .Peek(context.LoadRequirements)
         .Select(TagListDTO.Of);

      return new PageDTO<TagListDTO>(page, pageIndex, pageSize);
   }
}
