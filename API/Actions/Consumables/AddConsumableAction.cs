using Microsoft.AspNetCore.Mvc;
using UPXV.Backend.API.DTOs.Consumables;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Consumables;

public static class AddConsumableAction
{
   public static IResult MapEndpoint (
      [FromBody] ConsumableMovementDTO dto,
      [FromServices] UPXV_Context context
   ) {
      return Execute(dto, context).Either(
         Results.Ok,
         failures => failures switch
         {
            EntityNotFoundException<Consumable> e => Results.NotFound(e),
            Exception e => Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<ConsumableDetailDTO, Exception> Execute (ConsumableMovementDTO dto, UPXV_Context context)
   {
      Consumable? consumable = context.Consumables.Find(dto.Id);
      if (consumable == null) return new EntityNotFoundException<Consumable>(dto.Id);

      consumable.Quantity += dto.Amount;
      context.Update(consumable);
      context.SaveChanges();
      context.LoadRequirements(consumable);

      return ConsumableDetailDTO.Of(consumable);
   }
}
