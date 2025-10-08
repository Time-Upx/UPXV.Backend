using UPXV.Backend.API.DTOs.Tags;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Tags;

public static class GetTagAction
{
   public static IResult MapEndpoint (int id, UPXV_Context context)
   {
      return Execute(id, context).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failures => failures switch
         {
            EntityNotFoundException<Tag> e => Microsoft.AspNetCore.Http.Results.NotFound(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<TagDetailDTO, Exception> Execute (int id, UPXV_Context context)
   {
      Tag? tag = context.Tags.Find(id);
      if (tag is null)
      {
         return new EntityNotFoundException<Tag>(id);
      }
      context.LoadRequirements(tag);
      return TagDetailDTO.Of(tag);
   }
}
