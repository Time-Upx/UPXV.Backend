using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Consumables;
using UPXV.Backend.DTOs.Units;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.Units;

public class DeleteUnitEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapDelete("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out Unit unit, id))
            return Problems.NotFound<Unit>(id);

         context.LoadRequirements(unit);
         context.Remove(unit);
         context.SaveChanges();

         return Results.Ok(UnitDetailDTO.Of(unit));
      })
      .WithDescription("Remove a Unidade do banco de dados")
      .Produces<UnitDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
