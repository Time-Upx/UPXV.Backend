using FluentValidation;
using FluentValidation.Results;
using UPXV.Backend.Common;

namespace UPXV.Backend.Validation;

public static class Validate
{
   public static bool TryValidate<T> (this IValidator<T> validator, T value, out ValidationResult result)
   {
      result = validator.Validate(value);
      return result.IsValid;
   }
   public static bool TryFails(out ValidationResult result, params IEnumerable<(bool Failed, string Parameter, string? Message, object? Value)> failures)
   {
      List<ValidationFailure> failuresList = failures
         .Where(f => f.Failed)
         .Select(f => Problems.Invalid(f.Parameter, f.Message, f.Value))
         .ToList();

      if (failuresList.Any())
      {
         result = new ValidationResult(failuresList);
         return true;
      }
      result = null!;
      return true;
   }
}
