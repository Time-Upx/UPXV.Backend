using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Statuses;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Statuses;

public class UpdateStatusEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPut("/{id}", (int id, StatusUpdateDTO dto, UPXV_Context context, IValidator<StatusUpdateDTO> validator) =>
      {
         if (!context.TryFind(out Status status, id))
            return Problems.NotFound<Status>(id);

         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         context.LoadRequirements(status);

         dto.UpdateEntity(status);
         context.Update(status);
         context.SaveChanges();

         return Results.Ok(StatusDetailDTO.Of(status));
      })
      .WithDescription("Atualiza o Status com os valores novos")
      .Produces<StatusDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
