namespace UPXV.Common;

public readonly struct Result<TSuccess, TFailure>
{
   private readonly TSuccess? _value;
   private readonly TFailure? _problem;
   private readonly ResultType _type;
   public readonly TSuccess? Value => _value;
   public readonly TFailure? Problem => _problem;
   public readonly bool IsSuccess => _type == ResultType.Success;
   public readonly bool IsFailure => _type == ResultType.Failure;

   public Result (TSuccess value) => (_value, _problem, _type) = (value, default, ResultType.Success);
   public Result (TFailure exception) => (_value, _problem, _type) = (default, exception, ResultType.Failure);

   public static implicit operator Result<TSuccess, TFailure> (TSuccess value) => new Result<TSuccess, TFailure>(value);
   public static implicit operator Result<TSuccess, TFailure> (TFailure failure) => new Result<TSuccess, TFailure>(failure);

   public static Result<TSuccess, TFailure> Success (TSuccess value) => new Result<TSuccess, TFailure>(value);
   public static Result<TSuccess, TFailure> Failure (TFailure exception) => new Result<TSuccess, TFailure>(exception);

   public override string ToString () => IsSuccess
         ? $"Success({nameof(TSuccess)}: {_value?.ToString() ?? "null"})"
         : $"Failure({nameof(TFailure)}: {_problem?.ToString() ?? "null"})";

   public bool Equals (Result<TSuccess, TFailure> other)
   {
      if (this.IsSuccess && other.IsSuccess)
      {
         return this._value!.Equals(other._value);
      }
      else if (this.IsFailure && other.IsFailure)
      {
         return this._problem!.Equals(other._problem);
      }
      else
      {
         return false;
      }
   }
   public override bool Equals (object? obj) => obj is Result<TSuccess, TFailure> other ? Equals(other) : false;
   public override int GetHashCode () => IsSuccess
      ? _value!.GetHashCode()
      : _problem!.GetHashCode();
   public static bool operator == (Result<TSuccess, TFailure> a, Result<TSuccess, TFailure> b) => a.Equals(b);
   public static bool operator != (Result<TSuccess, TFailure> a, Result<TSuccess, TFailure> b) => !(a == b);
   public R Either<R> (Func<TSuccess, R> onSuccess, Func<TFailure, R> onFailure) => IsSuccess
      ? onSuccess(_value!)
      : onFailure(_problem!);

   public Result<R, TFailure> Map<R> (Func<TSuccess, R> mapper) => IsSuccess
      ? mapper(_value!)
      : new Result<R, TFailure>(_problem!);
   public async Task<Result<R, TFailure>> MapAsync<R> (Func<TSuccess, Task<R>> mapper) => IsSuccess
      ? await mapper(_value!)
      : new Result<R, TFailure>(_problem!);

   public Result<R, TFailure> FlatMap<R> (Func<TSuccess, Result<R, TFailure>> mapper) => IsSuccess
      ? mapper(Value!)
      : new Result<R, TFailure>(_problem!);
   public async Task<Result<R, TFailure>> FlatMap<R> (Func<TSuccess, Task<Result<R, TFailure>>> mapper) => IsSuccess
      ? await mapper(Value!)
      : new Result<R, TFailure>(_problem!);

   public enum ResultType
   {
      Success, Failure
   }
}