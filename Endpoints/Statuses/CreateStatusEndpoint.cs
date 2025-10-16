using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Statuses;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Statuses;

public class CreateStatusEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("", (StatusCreateDTO dto, UPXV_Context context, IValidator<StatusCreateDTO> validator) =>
      {
         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         Status status = dto.BuildEntity();

         context.Add(status);
         context.SaveChanges();

         context.LoadRequirements(status);
         return Results.Ok(StatusDetailDTO.Of(status));
      })
      .WithDescription("Salva um novo Status no banco de dados")
      .Produces<StatusDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
}