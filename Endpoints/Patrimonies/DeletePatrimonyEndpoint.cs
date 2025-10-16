using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Patrimonies;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.Patrimonies;

public class DeletePatrimonyEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapDelete("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out Patrimony patrimony, id))
            return Problems.NotFound<Patrimony>(id);

         context.LoadRequirements(patrimony);
         context.Remove(patrimony);
         context.SaveChanges();

         return Results.Ok(PatrimonyDetailDTO.Of(patrimony));
      })
      .WithDescription("Remove o Patrimônio do banco de dados")
      .Produces<PatrimonyDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}