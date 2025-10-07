using UPXV.Backend.API.DTOs.Tags;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Tags;

public static class DeleteTagAction
{
   public static IResult MapEndpoint (UPXV_Context context, int nid)
   {
      return Execute(context, nid).Either(
         Results.Ok,
         failure => failure switch
         {
            EntityNotFoundException<Tag> e => Results.NotFound(e),
            Exception e => Results.Problem(e.Message, statusCode: 500),
         });
   }

   public static Attempt<TagDetailDTO, Exception> Execute (UPXV_Context context, int nid)
   {
      Tag? tag = context.Tags.Find(nid);
      if (tag is null) return new EntityNotFoundException<Tag>(nid);

      context.LoadRequirements(tag);
      context.Remove(tag);
      context.SaveChanges();

      return TagDetailDTO.Of(tag);
   }
}