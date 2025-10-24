using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Intents;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.Intents;

public class GetIntentEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out Intent intent, id))
            return Problems.NotFound<Intent>(id);

         context.LoadRequirements(intent);
         return Results.Ok(IntentDetailDTO.Of(intent));
      })
      .WithDescription("""
         Detalha os dados da Intenção.
         Tipo 1 é 'Redirecionamento' e 2 é 'Ação'.
         """)
      .Produces<IntentDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
