using UPXV.Backend.API.DTOs.Units;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Units;

public static class GetUnitAction
{
   public static IResult MapEndpoint (int id, UPXV_Context context)
   {
      return Execute(id, context).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failures => failures switch
         {
            EntityNotFoundException<Unit> e => Microsoft.AspNetCore.Http.Results.NotFound(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<UnitDetailDTO, Exception> Execute (int id, UPXV_Context context)
   {
      Unit? unit = context.Units.Find(id);
      if (unit is null)
      {
         return new EntityNotFoundException<Unit>(id);
      }
      context.LoadRequirements(unit);
      return UnitDetailDTO.Of(unit);
   }
}
