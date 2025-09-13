using FluentValidation.Results;
using System.Collections;
using System.Collections.Immutable;

namespace UPXV.Services.Exceptions;

public class ValidationException : Exception
{
   private IEnumerable<ValidationFailure> _failures = [];
   public ImmutableArray<ValidationFailure> Failures => _failures.ToImmutableArray();
   public ValidationException () { }
   public ValidationException (string? message) : base(message) { }
   public ValidationException (string? message, Exception? innerException) : base(message, innerException) { }
   public ValidationException (IEnumerable<ValidationFailure> failures) => _failures = failures;
}
