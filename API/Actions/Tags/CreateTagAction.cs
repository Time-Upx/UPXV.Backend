using System.ComponentModel.DataAnnotations;
using UPXV.Backend.API.DTOs.Tags;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Tags;

public static class CreateTagAction
{
   public static IResult MapEndpoint (TagCreateDTO dto, UPXV_Context context)
   {
      return Execute(dto, context).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failures => failures switch
         {
            ValidationException e => Microsoft.AspNetCore.Http.Results.BadRequest(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<TagDetailDTO, Exception> Execute (TagCreateDTO dto, UPXV_Context context)
   {
      if (context.Tags.Any(c => c.Name == dto.Name))
      {
         return new ValidationException($"Já existe uma tag com o nome '{dto.Name}'");
      }

      Tag tag = dto.BuildEntity();

      context.Add(tag);
      context.SaveChanges();

      context.LoadRequirements(tag);
      return TagDetailDTO.Of(tag);
   }
}
