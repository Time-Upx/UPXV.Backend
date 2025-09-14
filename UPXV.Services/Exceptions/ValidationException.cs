using FluentValidation.Results;

namespace UPXV.Services.Exceptions;

public class ValidationException : Exception
{
   private IEnumerable<ValidationFailure> _failures = [];
   public ValidationFailure[] Failures => _failures.ToArray();
   public ValidationException () { }
   public ValidationException (string? message) : base(message) { }
   public ValidationException (string? message, Exception? innerException) : base(message, innerException) { }
   public ValidationException (IEnumerable<ValidationFailure> failures) => _failures = failures;
}
