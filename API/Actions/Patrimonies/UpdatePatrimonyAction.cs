using System.ComponentModel.DataAnnotations;
using UPXV.Backend.API.DTOs.Patrimonies;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Patrimonies;

public static class UpdatePatrimonyAction
{
   public static IResult MapEndpoint(PatrimonyUpdateDTO dto, UPXV_Context context)
   {
      return Execute(dto, context).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failures => failures switch
         {
            ValidationException e => Microsoft.AspNetCore.Http.Results.BadRequest(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<PatrimonyDetailDTO, Exception> Execute (PatrimonyUpdateDTO dto, UPXV_Context context)
   {
      if (context.Patrimonies.Any(p => p.Id != dto.Id && p.Name == dto.Name))
      {
         return new ValidationException($"O nome '{dto.Name}' já está sendo utilizado");
      }

      ICollection<Tag>? tags = dto.TagIds is null ? null 
         : context.Tags
            .Where(t => dto.TagIds.Contains(t.Id))
            .ToList();

      Patrimony? patrimony = context.Patrimonies.Find(dto.Id);
      if (patrimony is null) return new EntityNotFoundException<Patrimony>(dto.Id);

      context.LoadRequirements(patrimony);

      dto.UpdateEntity(patrimony, tags);

      context.Patrimonies.Add(patrimony);
      context.SaveChanges();

      return PatrimonyDetailDTO.Of(patrimony);
   }
}
