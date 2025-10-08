using UPXV.Backend.API.DTOs.Patrimonies;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Patrimonies;

public static class DeletePatrimonyAction
{
   public static IResult MapEndpoint (int id, UPXV_Context context)
   {
      return Execute(id, context).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failures => failures switch
         {
            EntityNotFoundException<Patrimony> e => Microsoft.AspNetCore.Http.Results.NotFound(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<PatrimonyDetailDTO, Exception> Execute (int id, UPXV_Context context)
   {
      Patrimony? patrimony = context.Patrimonies.Find(id);
      if (patrimony is null) return new EntityNotFoundException<Patrimony>(id);

      context.LoadRequirements(patrimony);
      context.Remove(patrimony);
      context.SaveChanges();

      return PatrimonyDetailDTO.Of(patrimony);
   }
}
