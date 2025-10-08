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
         Microsoft.AspNetCore.Http.Results.Ok,
         failures => failures switch
         {
            EntityNotFoundException<Status> e => Microsoft.AspNetCore.Http.Results.NotFound(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500)
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
