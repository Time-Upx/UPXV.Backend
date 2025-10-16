using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Units;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Units;

public class UpdateUnitEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPut("/{id}", (int id, UnitUpdateDTO dto, UPXV_Context context, IValidator<UnitUpdateDTO> validator) =>
      {
         if (!context.TryFind(out Unit unit, id))
            return Problems.NotFound<Unit>(id);

         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         context.LoadRequirements(unit);

         dto.UpdateEntity(unit);
         context.Update(unit);
         context.SaveChanges();

         return Results.Ok(UnitDetailDTO.Of(unit));
      })
      .WithDescription("Atualiza a Unidade com os valores novos")
      .Produces<UnitDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
