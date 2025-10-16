using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Patrimonies;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Patrimonies;

public class CreatePatrimonyEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("", (PatrimonyCreateDTO dto, UPXV_Context context, IValidator<PatrimonyCreateDTO> validator) =>
      {
         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         ICollection<Tag> tags = context.Tags
            .Where(t => dto.TagIds.Contains(t.Id))
            .ToList();

         Patrimony patrimony = dto.BuildEntity(tags);

         context.Add(patrimony);
         context.SaveChanges();

         context.LoadRequirements(patrimony);
         return Results.Ok(PatrimonyDetailDTO.Of(patrimony));
      })
      .WithDescription("Salva um novo Patrimônio no banco de dados")
      .Produces<PatrimonyDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
}
