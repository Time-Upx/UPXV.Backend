using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Statuses;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.Statuses;

public class GetStatusEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out Status status, id))
            return Problems.NotFound<Status>(id);

         context.LoadRequirements(status);
         return Results.Ok(StatusDetailDTO.Of(status));
      })
      .Produces<StatusDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
