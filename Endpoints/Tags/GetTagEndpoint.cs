using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Tags;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.Tags;

public class GetTagEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapGet("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out Tag tag, id))
            return Problems.NotFound<Tag>(id);

         context.LoadRequirements(tag);
         return Results.Ok(TagDetailDTO.Of(tag));
      })
      .WithDescription("Detalha os dados da Tag")
      .Produces<TagDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
