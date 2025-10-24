using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Consumables;
using UPXV.Backend.Endpoints;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Consumables;

public class CreateConsumableEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("", (ConsumableCreateDTO dto, UPXV_Context context, IValidator<ConsumableCreateDTO> validator) =>
      {
         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         ICollection<Tag> tags = context.Tags
            .Where(t => dto.TagIds.Contains(t.Id))
            .ToList();

         Consumable consumable = dto.BuildEntity(tags);

         context.Add(consumable);
         context.SaveChanges();

         context.LoadRequirements(consumable);
         return Results.Ok(ConsumableDetailDTO.Of(consumable));
      })
      .WithDescription("Salva um novo Consumível no banco de dados")
      .Produces<ConsumableDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
}
