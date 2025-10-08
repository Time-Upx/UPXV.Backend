using UPXV.Backend.API.DTOs.Consumables;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Consumables;

public static class GetConsumableAction
{
   public static IResult MapEndpoint (int id, UPXV_Context context)
   {
      return Execute(id, context).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failures => failures switch
         {
            EntityNotFoundException<Consumable> e => Microsoft.AspNetCore.Http.Results.NotFound(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<ConsumableDetailDTO, Exception> Execute (int id, UPXV_Context context)
   {
      Consumable? consumable = context.Consumables.Find(id);
      if (consumable is null)
      {
         return new EntityNotFoundException<Consumable>(id);
      }
      context.LoadRequirements(consumable);
      return ConsumableDetailDTO.Of(consumable);
   }
}