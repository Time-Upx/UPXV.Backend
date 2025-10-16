using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Consumables;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Consumables;

public class UpdateConsumableEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPut("/{id}", (int id, ConsumableUpdateDTO dto, UPXV_Context context, IValidator<ConsumableUpdateDTO> validator) =>
      {
         if (!context.TryFind(out Consumable consumable, id))
            return Problems.NotFound<Consumable>(id);

         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         ICollection<Tag>? tags = dto.TagIds is null ? null : context.Tags
            .Where(t => dto.TagIds.Contains(t.Id))
            .ToList();

         dto.UpdateEntity(consumable, tags);

         context.Update(consumable);
         context.SaveChanges();

         context.LoadRequirements(consumable);
         return Results.Ok(ConsumableDetailDTO.Of(consumable));
      })
      .WithDescription("Atualiza o Consumível com os valores novos")
      .Produces<ConsumableDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
