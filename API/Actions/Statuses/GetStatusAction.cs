using UPXV.Backend.API.DTOs.Statuses;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Statuses;

public static class GetStatusAction
{
   public static IResult MapEndpoint (int id, UPXV_Context context)
   {
      return Execute(id, context).Either(
         Results.Ok,
         failures => failures switch
         {
            EntityNotFoundException<Status> e => Results.NotFound(e),
            Exception e => Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<StatusDetailDTO, Exception> Execute (int id, UPXV_Context context)
   {
      Status? status = context.Status.Find(id);
      if (status is null)
      {
         return new EntityNotFoundException<Status>(id);
      }
      context.LoadRequirements(status);
      return StatusDetailDTO.Of(status);
   }
}
