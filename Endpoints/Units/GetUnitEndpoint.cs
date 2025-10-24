using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Units;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.Units;

public class GetUnitEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out Unit unit, id))
            return Problems.NotFound<Unit>(id);

         context.LoadRequirements(unit);
         return Results.Ok(UnitDetailDTO.Of(unit));
      })
      .WithDescription("Detalha os dados da Unidade")
      .Produces<UnitDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
