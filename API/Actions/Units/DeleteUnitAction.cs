using UPXV.Backend.API.DTOs.Units;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Units;

public static class DeleteUnitAction
{
   public static IResult MapEndpoint (UPXV_Context context, int nid)
   {
      return Execute(context, nid).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failure => failure switch
         {
            EntityNotFoundException<Unit> e => Microsoft.AspNetCore.Http.Results.NotFound(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500),
         });
   }

   public static Attempt<UnitDetailDTO, Exception> Execute (UPXV_Context context, int nid)
   {
      Unit? unit = context.Units.Find(nid);
      if (unit is null) return new EntityNotFoundException<Unit>(nid);

      context.LoadRequirements(unit);
      context.Remove(unit);
      context.SaveChanges();

      return UnitDetailDTO.Of(unit);
   }
}