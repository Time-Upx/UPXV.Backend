using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using UPXV.Backend.API.DTOs.Consumables;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Consumables;

public static class CreateConsumableAction
{
   public static IResult MapEndpoint (
      [FromBody] ConsumableCreateDTO dto, 
      [FromServices] UPXV_Context context
   ) {
      return Execute(dto, context).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failures => failures switch
         {
            ValidationException e => Microsoft.AspNetCore.Http.Results.BadRequest(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: (int) HttpStatusCode.InternalServerError)
         });
   }

   public static Attempt<ConsumableDetailDTO, Exception> Execute (ConsumableCreateDTO dto, UPXV_Context context)
   {
      if (context.Consumables.Any(c => c.Name == dto.Name))
      {
         return new ValidationException($"Já existe um consumível com o nome '{dto.Name}'");
      }

      int[] tagIds = dto.TagIds.ToArray();
      ICollection<Tag> tags = context.Tags
         .Where(t => tagIds.Contains(t.Id))
         .ToList();

      Consumable consumable = dto.BuildEntity(tags);

      context.Consumables.Add(consumable);
      context.SaveChanges();

      context.LoadRequirements(consumable);
      return ConsumableDetailDTO.Of(consumable);
   }
}
