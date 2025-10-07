using System.ComponentModel.DataAnnotations;
using UPXV.Backend.API.DTOs.Tags;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Tags;

public static class UpdateTagAction
{
   public static IResult MapEndpoint (TagUpdateDTO dto, UPXV_Context context)
   {
      return Execute(dto, context).Either(
         Results.Ok,
         static failures => failures switch
         {
            ValidationException e => Results.BadRequest(e),
            EntityNotFoundException<Tag> e => Results.NotFound(e),
            Exception e => Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<TagDetailDTO, Exception> Execute (TagUpdateDTO dto, UPXV_Context context)
   {
      if (context.Tags.Any(t => t.Id != dto.Id && t.Name == dto.Name))
      {
         return new ValidationException($"Já existe uma tag com o nome '{dto.Name}'");
      }

      Tag? tag = context.Tags.Find(dto.Id);
      if (tag is null)
      {
         return new EntityNotFoundException<Tag>(dto.Id);
      }

      context.LoadRequirements(tag);

      dto.UpdateEntity(tag);
      context.Update(tag);
      context.SaveChanges();

      return TagDetailDTO.Of(tag);
   }
}
