using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Tags;
using UPXV.Backend.Entities;

namespace UPXV.Backend.Endpoints.Tags;

public class DeleteTagEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapDelete("/{id}", (int id, UPXV_Context context) =>
      {
         if (!context.TryFind(out Tag tag, id))
            return Problems.NotFound<Tag>(id);

         context.LoadRequirements(tag);
         context.Remove(tag);
         context.SaveChanges();

         return Results.Ok(TagDetailDTO.Of(tag));
      })
      .WithDescription("Remove a Tag do banco de dados")
      .Produces<TagDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
