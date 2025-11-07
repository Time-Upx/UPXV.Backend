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

         bool hasPatrimonies = context.Patrimonies.Any(p => p.Tags.Any(t => t.Id == id));
         bool hasConsumables = context.Consumables.Any(c => c.Tags.Any(t => t.Id == id));

         string msg = (hasConsumables, hasPatrimonies) switch
         {
            (true, true) => "patrimônios e consumíveis",
            (true, false) => "consumíveis",
            (false, true) => "patrimônios",
            _ => null!
         };

         if (msg is not null)
            return Results.Conflict($"Não foi possível deletar tag, há {msg} atrelados");

         context.LoadRequirements(tag);
         context.Remove(tag);
         context.SaveChanges();

         return Results.Ok(TagDetailDTO.Of(tag));
      })
      .WithDescription("Remove a Tag do banco de dados. Não permite a remoção de Tags que estejam atreladas a um ou mais consumíveis ou patrimônios")
      .Produces<TagDetailDTO>(StatusCodes.Status200OK)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound)
      .Produces<string>(StatusCodes.Status409Conflict);
}
