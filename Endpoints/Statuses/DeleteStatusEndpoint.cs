using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Statuses;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.Statuses;

public class DeleteStatusEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapDelete("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out Status status, id))
            return Problems.NotFound<Status>(id);

         if (context.Patrimonies.Any(p => p.StatusId == id))
            return Results.Conflict("Não é possível deletar status, há patrimônios atrelados");

         context.LoadRequirements(status);
         context.Remove(status);
         context.SaveChanges();

         return Results.Ok(StatusDetailDTO.Of(status));
      })
      .WithDescription("Remove o Status do banco de dados. Não permite a remoção de Statuses que estejam atrelados a um ou mais patrimônios")
      .Produces<StatusDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound)
      .Produces<string>(StatusCodes.Status409Conflict);
}
