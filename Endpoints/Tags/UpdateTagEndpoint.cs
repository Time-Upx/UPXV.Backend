using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Tags;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Tags;

public class UpdateTagEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPut("/{id}", (int id, TagUpdateDTO dto, UPXV_Context context, IValidator<TagUpdateDTO> validator) =>
      {
         if (!context.TryFind(out Tag tag, id))
            return Problems.NotFound<Tag>(id);

         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         context.LoadRequirements(tag);

         dto.UpdateEntity(tag);
         context.Update(tag);
         context.SaveChanges();

         return Results.Ok(TagDetailDTO.Of(tag));
      })
      .WithDescription("Atualiza a Tag com os valores novos")
      .Produces<TagDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
