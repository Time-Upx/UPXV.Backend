using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;
using UPXV.Backend.Data;
using UPXV.Backend.DTOs.Tags;
using UPXV.Backend.Entities;
using UPXV.Backend.Validation;

namespace UPXV.Backend.Endpoints.Tags;

public class CreateTagEndpoint : IEndpoint
{
   public void MapEndpoint (IEndpointRouteBuilder app) =>
      app.MapPost("", (TagCreateDTO dto, UPXV_Context context, IValidator<TagCreateDTO> validator) =>
      {
         if (!validator.TryValidate(dto, out ValidationResult result))
            return Problems.Validation(result.Errors);

         Tag tag = dto.BuildEntity();

         context.Add(tag);
         context.SaveChanges();

         context.LoadRequirements(tag);
         return Results.Ok(TagDetailDTO.Of(tag));
      })
      .WithDescription("Salva uma nova Tag no banco de dados")
      .Produces<TagDetailDTO>(StatusCodes.Status200OK)
      .Produces<List<ValidationFailure>>(StatusCodes.Status400BadRequest);
}
