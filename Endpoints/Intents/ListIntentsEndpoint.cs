using FluentValidation.Results;
using UPXV.Backend.Common.Extensions;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Intents;

namespace UPXV.Backend.Endpoints.Intents;

public class ListIntentsEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("", (UPXV_Context context) =>
      {
         IEnumerable<IntentListDTO> page = context.Intents
            .ToList()
            .Peek(context.LoadRequirements)
            .Select(IntentListDTO.Of);

         return Results.Ok(page.ToList());
      })
      .WithDescription("""
         Lista todos as Intenções sem paginação.
         Tipo 1 é 'Redirecionamento' e 2 é 'Ação'.
         """)
      .Produces<List<IntentListDTO>>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
}
