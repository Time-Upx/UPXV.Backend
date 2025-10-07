using System.ComponentModel.DataAnnotations;
using UPXV.Backend.API.DTOs.Patrimonies;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Patrimonies;

public static class CreatePatrimonyAction
{
   public static IResult MapEndpoint (PatrimonyCreateDTO dto, UPXV_Context context)
   {
      return Execute(dto, context).Either(
         Results.Ok,
         failures => failures switch
         {
            ValidationException e => Results.BadRequest(e),
            Exception e => Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<PatrimonyDetailDTO, Exception> Execute (PatrimonyCreateDTO dto, UPXV_Context context)
   {
      if (context.Patrimonies.Any(p => p.Name == dto.Name))
      {
         return new ValidationException($"Já existe um patrimônio com o nome '{dto.Name}'");
      }

      int[] tagIds = dto.TagIds.ToArray();
      ICollection<Tag> tags = context.Tags
         .Where(t => tagIds.Contains(t.Id))
         .ToList();

      Patrimony patrimony = dto.BuildEntity(tags);

      context.Patrimonies.Add(patrimony);
      context.SaveChanges();

      context.LoadRequirements(patrimony);
      return PatrimonyDetailDTO.Of(patrimony);
   }

}
