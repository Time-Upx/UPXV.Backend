using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Patrimonies;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Patrimonies;

public class SwitchPatrimonyStatusEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("/{id}/switch-status", (int id, int statusId, UPXV_Context context) =>
      {
         if (!context.TryFind(out Patrimony patrimony, id))
            return Problems.NotFound<Patrimony>(id);

         if (Validate.TryFails(out ValidationResult result, 
            (!context.Exists<Status>(statusId), nameof(statusId), "Status não existe", statusId)))
            return Problems.Validation(result.Errors.FirstOrDefault());

         patrimony.StatusId = statusId;

         context.Update(patrimony);
         context.SaveChanges();
         context.LoadRequirements(patrimony);

         return Results.Ok(PatrimonyDetailDTO.Of(patrimony));
      })
      .Produces<PatrimonyDetailDTO>(StatusCodes.Status200OK)
      .Produces<ValidationFailure>(StatusCodes.Status400BadRequest)
      .Produces<EntityNotFoundDetails>(StatusCodes.Status404NotFound);
}
