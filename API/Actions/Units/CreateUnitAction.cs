using System.ComponentModel.DataAnnotations;
using UPXV.Backend.API.DTOs.Units;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Units;

public static class CreateUnitAction
{
   public static IResult MapEndpoint (UnitCreateDTO dto, UPXV_Context context)
   {
      return Execute(dto, context).Either(
         Microsoft.AspNetCore.Http.Results.Ok,
         failures => failures switch
         {
            ValidationException e => Microsoft.AspNetCore.Http.Results.BadRequest(e),
            Exception e => Microsoft.AspNetCore.Http.Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<UnitDetailDTO, Exception> Execute (UnitCreateDTO dto, UPXV_Context context)
   {
      if (context.Units.Any(u => u.Name == dto.Name))
      {
         return new ValidationException($"Já existe uma unidade de medida com o nome '{dto.Name}'");
      }

      Unit unit = dto.BuildEntity();

      context.Add(unit);
      context.SaveChanges();

      context.LoadRequirements(unit);
      return UnitDetailDTO.Of(unit);
   }
}
