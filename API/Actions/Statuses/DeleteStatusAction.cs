using UPXV.Backend.API.DTOs.Statuses;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Statuses;

public static class DeleteStatusAction
{
   public static IResult MapEndpoint (UPXV_Context context, int nid)
   {
      return Execute(context, nid).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failure => failure switch
         {
            EntityNotFoundException<Status> e => Microsoft.AspNetCore.Http.Results.NotFound(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500),
         });
   }

   public static Attempt<StatusDetailDTO, Exception> Execute (UPXV_Context context, int nid)
   {
      Status? consumable = context.Status.Find(nid);
      if (consumable is null) return new EntityNotFoundException<Status>(nid);

      context.LoadRequirements(consumable);
      context.Remove(consumable);
      context.SaveChanges();

      return StatusDetailDTO.Of(consumable);
   }
}
