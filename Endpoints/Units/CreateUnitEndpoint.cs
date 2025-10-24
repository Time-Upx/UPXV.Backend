using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Units;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Units;

public class CreateUnitEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("", (UnitCreateDTO dto, UPXV_Context context, IValidator<UnitCreateDTO> validator) =>
      {
         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         Unit unit = dto.BuildEntity();

         context.Add(unit);
         context.SaveChanges();

         context.LoadRequirements(unit);
         return Results.Ok(UnitDetailDTO.Of(unit));
      })
      .WithDescription("Salva uma nova Unidade no banco de dados")
      .Produces<UnitDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
}
