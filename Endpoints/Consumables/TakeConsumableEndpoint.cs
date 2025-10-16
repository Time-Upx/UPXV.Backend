using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Consumables;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Consumables;

public class TakeConsumableEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("/{id}/take", (int id, double amount, UPXV_Context context) =>
      {
         if (!context.TryFind(out Consumable consumable, id))
            return Problems.NotFound<Consumable>(id);

         if (Validate.TryFails(out ValidationResult result,
            (amount < 0, nameof(amount), "Quantidade não pode ser um valor negativo", amount)))
            return Problems.Validation(result.Errors.FirstOrDefault());

         consumable.Quantity -= amount;
         if (consumable.Quantity < 0) consumable.Quantity = 0;

         context.Update(consumable);
         context.SaveChanges();
         context.LoadRequirements(consumable);

         return Results.Ok(ConsumableDetailDTO.Of(consumable));
      })
      .WithDescription("Deminishes the quantity of the consumable by the amount passed in the query")
      .Produces<ConsumableDetailDTO>(StatusCodes.Status200OK)
      .Produces<ValidationFailure>(StatusCodes.Status400BadRequest)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
