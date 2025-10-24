using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Consumables;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.Consumables;

public class DeleteConsumableEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapDelete("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out Consumable consumable, id))
            return Problems.NotFound<Consumable>(id);

         context.LoadRequirements(consumable);
         context.Remove(consumable);
         context.SaveChanges();

         return Results.Ok(ConsumableDetailDTO.Of(consumable));
      })
      .WithDescription("Remove o Consumível do banco de dados")
      .Produces<ConsumableDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
