using UPXV.Backend.API.DTOs.Consumables;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Consumables;

public static class TakeConsumableAction
{
   public static IResult MapEndpoint(ConsumableMovementDTO dto, UPXV_Context context)
   {
      return Execute(dto, context).Either(
         () => Microsoft.AspNetCore.Http.Results.Ok(),
         failure => failure switch
         {
            EntityNotFoundException<Consumable> e => Microsoft.AspNetCore.Http.Results.NotFound(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500)
         });
   }
   public static Attempt<Exception> Execute (ConsumableMovementDTO dto, UPXV_Context context)
   {
      Consumable? consumable = context.Consumables.Find(dto.Id);
      if (consumable is null)
      {
         return new EntityNotFoundException<Consumable>(dto.Id);
      }

      consumable.Quantity -= dto.Amount;
      if (consumable.Quantity < 0) consumable.Quantity = 0;

      context.Update(consumable);
      context.SaveChanges();

      return Attempts.Success<Exception>();
   }
}
