using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Consumables;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.Consumables;

public class GetConsumableEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out Consumable consumable, id))
            return Problems.NotFound<Consumable>(id);

         context.LoadRequirements(consumable);
         return Results.Ok(ConsumableDetailDTO.Of(consumable));
      })
      .WithDescription("Gets a consumable by its id")
      .Produces<ConsumableDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}