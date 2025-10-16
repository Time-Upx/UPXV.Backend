using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Patrimonies;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.Patrimonies;

public class GetPatrimonyEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out Patrimony patrimony, id))
            return Problems.NotFound<Patrimony>(id);

         context.LoadRequirements(patrimony);
         return Results.Ok(PatrimonyDetailDTO.Of(patrimony));
      })
      .Produces<PatrimonyDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
