using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Patrimonies;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Patrimonies;

public class UpdatePatrimonyEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPut("/{id}", (int id, PatrimonyUpdateDTO dto, UPXV_Context context, IValidator<PatrimonyUpdateDTO> validator) =>
      {
         if (!context.TryFind(out Patrimony patrimony, id))
            return Problems.NotFound<Patrimony>(id);

         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         context.LoadRequirements(patrimony);

         ICollection<Tag>? tags = dto.TagIds is null ? null : context.Tags
            .Where(t => dto.TagIds.Contains(t.Id))
            .ToList();

         dto.UpdateEntity(patrimony, tags);

         context.Update(patrimony);
         context.SaveChanges();

         return Results.Ok(PatrimonyDetailDTO.Of(patrimony));
      })
      .WithDescription("Atualiza o Patrimônio com os valores novos")
      .Produces<PatrimonyDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
