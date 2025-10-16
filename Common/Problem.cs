using FluentValidation.Results;

namespace UPXV.Backend.Common;

public record EntityNotFoundDetails(string Entity, string Message, object Id);
public static class Problems
{
   public static IResult Error (Exception ex, int status = 500)
   {
      return Results.Problem(ex.Message + Environment.NewLine + ex.StackTrace, statusCode: status, type: ex.GetType().Name);
   }
   public static IResult NotFound<TEntity> (object? id) => 
      Results.NotFound(new EntityNotFoundDetails(typeof(TEntity).Name, "Entity not found", id ?? "null"));

   public static ValidationFailure Invalid (string parameter, string? message = null, object? value = null)
   {
      var endValue = value is null ? "" : (" " + value);
      var endMessage = message ?? "Invalid value" + endValue;
      return new ValidationFailure(parameter, endMessage, value);
   }
   public static IResult Validation (string parameter, string? message = null, object? value = null)
   {
      var endValue = value is null ? "" : (" " + value);
      var endMessage = message ?? "Invalid value" + endValue;
      return Results.BadRequest(new ValidationFailure(parameter, endMessage, value));
   }
   public static IResult Validation (params IEnumerable<ValidationFailure> failures)
   {
      return Results.BadRequest(failures);
   }
   public static IResult Validation (ValidationFailure? first = null, params IEnumerable<ValidationFailure> failures)
   {
      return Results.BadRequest(failures.Prepend(first));
   }
}
