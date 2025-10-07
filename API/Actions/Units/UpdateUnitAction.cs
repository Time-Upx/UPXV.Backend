using System.ComponentModel.DataAnnotations;
using UPXV.Backend.API.DTOs.Units;
using UPXV.Backend.API.Entities;
using UPXV.Backend.Common.Exceptions;
using UPXV.Backend.Data;

namespace UPXV.Backend.API.Actions.Units;

public static class UpdateUnitAction
{
   public static IResult MapEndpoint (UnitUpdateDTO dto, UPXV_Context context)
   {
      return Execute(dto, context).Either(
         Results.Ok,
         static failures => failures switch
         {
            ValidationException e => Results.BadRequest(e),
            EntityNotFoundException<Unit> e => Results.NotFound(e),
            Exception e => Results.Problem(e.Message, statusCode: 500)
         });
   }

   public static Attempt<UnitDetailDTO, Exception> Execute (UnitUpdateDTO dto, UPXV_Context context)
   {
      if (context.Units.Any(u => u.Id != dto.Id && u.Name == dto.Name))
      {
         return new ValidationException($"Já existe uma unit com o nome '{dto.Name}'");
      }

      Unit? unit = context.Units.Find(dto.Id);
      if (unit is null)
      {
         return new EntityNotFoundException<Unit>(dto.Id);
      }

      context.LoadRequirements(unit);

      dto.UpdateEntity(unit);
      context.Update(unit);
      context.SaveChanges();

      return UnitDetailDTO.Of(unit);
   }
}
