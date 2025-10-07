using UPXV.Backend.API.DTOs.Consumables;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Consumables;

public static class DeleteConsumableAction
{
   public static IResult MapEndpoint (UPXV_Context context, int nid) 
   {
      return Execute(context, nid).Either(
         Results.Ok,
         failure => failure switch
         {
            EntityNotFoundException<Consumable> e => Results.NotFound(e),
            Exception e => Results.Problem(e.Message, statusCode: 500),
         });
   }

   public static Attempt<ConsumableDetailDTO, Exception> Execute (UPXV_Context context, int nid)
   {
      Consumable? consumable = context.Consumables.Find(nid);
      if (consumable is null) return new EntityNotFoundException<Consumable>(nid);

      context.LoadRequirements(consumable);
      context.Remove(consumable);
      context.SaveChanges();

      return ConsumableDetailDTO.Of(consumable);
   }
}
