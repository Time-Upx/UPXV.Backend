using System.ComponentModel.DataAnnotations;
using UPXV.Backend.API.DTOs.Consumables;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Consumables;

public static class UpdateConsumableAction
{
   public static IResult MapEndpoint (UpdateConsumableDTO dto, UPXV_Context context) 
   {
      return Execute(dto, context).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failure => failure switch
         {
            ValidationException e => Microsoft.AspNetCore.Http.Results.BadRequest(e),
            EntityNotFoundException<Consumable> e => Microsoft.AspNetCore.Http.Results.NotFound(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500),
         });
   }

   public static Attempt<ConsumableDetailDTO, Exception> Execute (UpdateConsumableDTO dto, UPXV_Context context)
   {
      if (context.Consumables.Any(c => c.Id != dto.Id && c.Name == dto.Name))
      {
         return new ValidationException($"O nome '{dto.Name}' já está sendo utilizado");
      }

      Consumable? consumable = context.Consumables.Find(dto.Id);

      if (consumable is null) return new EntityNotFoundException<Consumable>(dto.Id);
      
      ICollection<Tag>? tags = dto.TagIds is null ? null
         : context.Tags
            .Where(t => dto.TagIds.Contains(t.Id))
            .ToList();

      dto.UpdateEntity(consumable, tags);

      context.Consumables.Add(consumable);
      context.SaveChanges();

      context.LoadRequirements(consumable);
      return ConsumableDetailDTO.Of(consumable);
   }
}
