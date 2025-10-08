/// <summary>
/// Represents the result of a process that can either succeed with a value of type <typeparamref name="TSuccess"/> or fail with a value of type <typeparamref name="TFailure"/>.
/// This is a union type, holding either the success or the failure value, but never both.
/// Recommended for use as a return type to clarify semantics and control flow; not intended to be passed as arguments.
/// Though outside the intended use case, Exceptions make very useful failure types.
/// </summary>
/// <typeparam name="TSuccess">The type of the success value.</typeparam>
/// <typeparam name="TFailure">The type of the failure value.</typeparam>
public abstract record Attempt<TSuccess, TFailure>
{
    /// <summary>
    /// Executes the appropriate function based on whether this instance represents a success or a failure.
    /// </summary>
    /// <typeparam name="TResult">The result type of the functions.</typeparam>
    /// <param name="onSuccess">Function to execute if this is a success.</param>
    /// <param name="onFailure">Function to execute if this is a failure.</param>
    /// <returns>The result of the executed function.</returns>
    public abstract TResult Either<TResult>(Func<TSuccess, TResult> onSuccess, Func<TFailure, TResult> onFailure);

    /// <summary>
    /// Gets a value indicating whether this instance represents a success.
    /// </summary>
    public bool IsSuccess => this is SuccessType<TSuccess, TFailure>;

    /// <summary>
    /// Gets a value indicating whether this instance represents a failure.
    /// </summary>
    public bool IsFailure => this is FailureType<TSuccess, TFailure>;

    /// <summary>
    /// Attempts to get the success value.
    /// </summary>
    /// <param name="success">The success value, if present.</param>
    /// <returns>True if this is a success; otherwise, false.</returns>
    public bool TryGetSuccess(out TSuccess success)
    {
        if (this is SuccessType<TSuccess, TFailure> s)
        {
            success = s.Value;
            return true;
        }
        success = default!;
        return false;
    }

    /// <summary>
    /// Attempts to get the failure value.
    /// </summary>
    /// <param name="failure">The failure value, if present.</param>
    /// <returns>True if this is a failure; otherwise, false.</returns>
    public bool TryGetFailure(out TFailure failure)
    {
        if (this is FailureType<TSuccess, TFailure> f)
        {
            failure = f.Value;
            return true;
        }
        failure = default!;
        return false;
    }

    /// <summary>
    /// Implicitly converts a success value to an <see cref="Attempt{TSuccess, TFailure}"/>.
    /// </summary>
    public static implicit operator Attempt<TSuccess, TFailure>(TSuccess value) => new SuccessType<TSuccess, TFailure>(value);

    /// <summary>
    /// Implicitly converts a failure value to an <see cref="Attempt{TSuccess, TFailure}"/>.
    /// </summary>
    public static implicit operator Attempt<TSuccess, TFailure>(TFailure value) => new FailureType<TSuccess, TFailure>(value);

    /// <summary>
    /// Represents a successful result of an <see cref="Attempt{TSuccess, TFailure}"/>.
    /// </summary>
    /// <typeparam name="TS">The type of the success value.</typeparam>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    public sealed record SuccessType<TS, TF> : Attempt<TS, TF>
    {
        /// <summary>
        /// Gets the success value.
        /// </summary>
        public TS Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessType{TS, TF}"/> record.
        /// </summary>
        /// <param name="value">The success value.</param>
        public SuccessType(TS value) => Value = value;

        /// <inheritdoc/>
        public override TResult Either<TResult>(Func<TS, TResult> onSuccess, Func<TF, TResult> onFailure)
        {
            return onSuccess(Value);
        }
    }

    /// <summary>
    /// Represents a failed result of an <see cref="Attempt{TSuccess, TFailure}"/>.
    /// </summary>
    /// <typeparam name="TS">The type of the success value.</typeparam>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    public sealed record FailureType<TS, TF> : Attempt<TS, TF>
    {
        /// <summary>
        /// Gets the failure value.
        /// </summary>
        public TF Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FailureType{TS, TF}"/> record.
        /// </summary>
        /// <param name="value">The failure value.</param>
        public FailureType(TF value) => Value = value;

        /// <inheritdoc/>
        public override TResult Either<TResult>(Func<TS, TResult> onSuccess, Func<TF, TResult> onFailure)
        {
            return onFailure(Value);
        }
    }
}

/// <summary>
/// Represents the result of a process that can either succeed (without a value) or fail with a value of type <typeparamref name="TFailure"/>.
/// This is a union type, holding either the success or the failure value, but never both.
/// Recommended for use as a return type to clarify semantics and control flow; not intended to be passed as arguments.
/// </summary>
/// <typeparam name="TFailure">The type of the failure value.</typeparam>
public abstract record Attempt<TFailure>
{
    /// <summary>
    /// Executes the appropriate function based on whether this instance represents a success or a failure.
    /// </summary>
    /// <typeparam name="TResult">The result type of the functions.</typeparam>
    /// <param name="onSuccess">Function to execute if this is a success.</param>
    /// <param name="onFailure">Function to execute if this is a failure.</param>
    /// <returns>The result of the executed function.</returns>
    public abstract TResult Either<TResult>(Func<TResult> onSuccess, Func<TFailure, TResult> onFailure);

    /// <summary>
    /// Gets a value indicating whether this instance represents a success.
    /// </summary>
    public bool IsSuccess => this is SuccessType<TFailure>;

    /// <summary>
    /// Gets a value indicating whether this instance represents a failure.
    /// </summary>
    public bool IsFailure => this is FailureType<TFailure>;

    /// <summary>
    /// Attempts to get the failure value.
    /// </summary>
    /// <param name="failure">The failure value, if present.</param>
    /// <returns>True if this is a failure; otherwise, false.</returns>
    public bool TryGetFailure(out TFailure failure)
    {
        if (this is FailureType<TFailure> f)
        {
            failure = f.Value;
            return true;
        }
        failure = default!;
        return false;
    }

    /// <summary>
    /// Implicitly converts a failure value to an <see cref="Attempt{TFailure}"/>.
    /// </summary>
    public static implicit operator Attempt<TFailure>(TFailure value) => new FailureType<TFailure>(value);

    /// <summary>
    /// Represents a successful result of an <see cref="Attempt{TFailure}"/>.
    /// </summary>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    public sealed record SuccessType<TF> : Attempt<TF>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessType{TF}"/> record.
        /// </summary>
        public SuccessType() : base() { }

        /// <inheritdoc/>
        public override TResult Either<TResult>(Func<TResult> onSuccess, Func<TF, TResult> onFailure)
        {
            return onSuccess();
        }
    }

    /// <summary>
    /// Represents a failed result of an <see cref="Attempt{TFailure}"/>.
    /// </summary>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    public sealed record FailureType<TF> : Attempt<TF>
    {
        /// <summary>
        /// Gets the failure value.
        /// </summary>
        public TF Value { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FailureType{TF}"/> record.
        /// </summary>
        /// <param name="value">The failure value.</param>
        public FailureType(TF value) => Value = value;

        /// <inheritdoc/>
        public override TResult Either<TResult>(Func<TResult> onSuccess, Func<TF, TResult> onFailure)
        {
            return onFailure(Value);
        }
    }
}

/// <summary>
/// Represents the result of a process that can either succeed or fail, without holding any value.
/// This is a union type, holding only the state of success or failure.
/// Recommended for use as a return type to clarify semantics and control flow; not intended to be passed as arguments.
/// </summary>
public abstract record Attempt
{
    /// <summary>
    /// Executes the appropriate function based on whether this instance represents a success or a failure.
    /// </summary>
    /// <typeparam name="TResult">The result type of the functions.</typeparam>
    /// <param name="onSuccess">Function to execute if this is a success.</param>
    /// <param name="onFailure">Function to execute if this is a failure.</param>
    /// <returns>The result of the executed function.</returns>
    public abstract TResult Either<TResult>(Func<TResult> onSuccess, Func<TResult> onFailure);

    /// <summary>
    /// Gets a value indicating whether this instance represents a success.
    /// </summary>
    public bool IsSuccess => this is SuccessType;

    /// <summary>
    /// Gets a value indicating whether this instance represents a failure.
    /// </summary>
    public bool IsFailure => this is FailureType;

    /// <summary>
    /// Represents a successful result of an <see cref="Attempt"/>.
    /// </summary>
    public sealed record SuccessType : Attempt
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SuccessType"/> record.
        /// </summary>
        public SuccessType() { }

        /// <inheritdoc/>
        public override TResult Either<TResult>(Func<TResult> onSuccess, Func<TResult> onFailure)
        {
            return onSuccess();
        }
    }

    /// <summary>
    /// Represents a failed result of an <see cref="Attempt"/>.
    /// </summary>
    public sealed record FailureType : Attempt
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FailureType"/> record.
        /// </summary>
        public FailureType() { }

        /// <inheritdoc/>
        public override TResult Either<TResult>(Func<TResult> onSuccess, Func<TResult> onFailure)
        {
            return onFailure();
        }
    }
}

/// <summary>
/// Provides static helper methods for creating and working with <see cref="Attempt"/>, <see cref="Attempt{TFailure}"/>, and <see cref="Attempt{TSuccess, TFailure}"/> instances.
/// </summary>
public static class Attempts
{
    /// <summary>
    /// Creates a successful <see cref="Attempt{TSuccess, TFailure}"/> with the specified value.
    /// </summary>
    public static Attempt<TS, TF> Success<TS, TF>(TS value) => new Attempt<TS, TF>.SuccessType<TS, TF>(value);

    /// <summary>
    /// Creates a failed <see cref="Attempt{TSuccess, TFailure}"/> with the specified value.
    /// </summary>
    public static Attempt<TS, TF> Failure<TS, TF>(TF value) => new Attempt<TS, TF>.FailureType<TS, TF>(value);

    /// <summary>
    /// Creates a successful <see cref="Attempt{TFailure}"/> (no value).
    /// </summary>
    public static Attempt<TF> Success<TF>() => new Attempt<TF>.SuccessType<TF>();

    /// <summary>
    /// Creates a failed <see cref="Attempt{TFailure}"/> with the specified value.
    /// </summary>
    public static Attempt<TF> Failure<TF>(TF value) => new Attempt<TF>.FailureType<TF>(value);

    /// <summary>
    /// Creates a successful untyped <see cref="Attempt"/>.
    /// </summary>
    public static Attempt Success() => new Attempt.SuccessType();

    /// <summary>
    /// Creates a failed untyped <see cref="Attempt"/>.
    /// </summary>
    public static Attempt Failure() => new Attempt.FailureType();


    /// <summary>
    /// Creates a successful <see cref="Attempt{TSuccess, TFailure}"/> if <paramref name="value"/> is not null; 
    /// otherwise, creates a failed attempt with the specified <paramref name="failure"/> value.
    /// </summary>
    /// <typeparam name="TS">The type of the success value.</typeparam>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    /// <param name="value">The value to check for null.</param>
    /// <param name="failure">The failure value to use if <paramref name="value"/> is null.</param>
    /// <returns>
    /// A successful <see cref="Attempt{TSuccess, TFailure}"/> if <paramref name="value"/> is not null; 
    /// otherwise, a failed attempt with the specified <paramref name="failure"/> value.
    /// </returns>
    public static Attempt<TS, TF> FromNullable<TS, TF>(TS value, TF failure)
    {
        if (value is null)
        {
            return Failure<TS, TF>(failure);
        }
        return Success<TS, TF>(value);
    }

    /// <summary>
    /// Creates a successful <see cref="Attempt{TFailure}"/> if <paramref name="value"/> is not null; 
    /// otherwise, creates a failed attempt with the specified <paramref name="failure"/> value.
    /// </summary>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    /// <param name="value">The value to check for null.</param>
    /// <param name="failure">The failure value to use if <paramref name="value"/> is null.</param>
    /// <returns>
    /// A successful <see cref="Attempt{TFailure}"/> if <paramref name="value"/> is not null; 
    /// otherwise, a failed attempt with the specified <paramref name="failure"/> value.
    /// </returns>
    public static Attempt<TF> FromNullable<TF>(object value, TF failure)
    {
        if (value is null)
        {
            return Failure(failure);
        }
        return Success<TF>();
    }

    /// <summary>
    /// Creates a successful untyped <see cref="Attempt"/> if <paramref name="value"/> is not null; 
    /// otherwise, creates a failed untyped <see cref="Attempt"/>.
    /// </summary>
    /// <param name="value">The value to check for null.</param>
    /// <returns>
    /// A successful <see cref="Attempt"/> if <paramref name="value"/> is not null; 
    /// otherwise, a failed <see cref="Attempt"/>.
    /// </returns>
    public static Attempt FromNullable(object value)
    {
        if (value is null)
        {
            return Failure();
        }
        return Success();
    }

    /// <summary>
    /// Gets the success value from an <see cref="Attempt{TSuccess, TFailure}"/> if it is a success; otherwise, throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <typeparam name="TS">The type of the success value.</typeparam>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    /// <param name="attempt">The attempt to extract the success value from.</param>
    /// <returns>The success value.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the attempt is a failure.</exception>
    /// /// <exception cref="ArgumentNullException">Thrown if the attempt is null.</exception>"
    public static TS GetSuccessValueOrThrow<TS, TF>(this Attempt<TS, TF> attempt)
    {
        if (attempt is null) throw new ArgumentNullException(nameof(attempt));
        if (attempt.IsFailure)
        {
            throw new InvalidOperationException("Attempt is a failure.");
        }
        return (attempt as Attempt<TS, TF>.SuccessType<TS, TF>).Value;
    }

    /// <summary>
    /// Gets the failure value from an <see cref="Attempt{TSuccess, TFailure}"/> if it is a failure; otherwise, throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <typeparam name="TS">The type of the success value.</typeparam>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    /// <param name="attempt">The attempt to extract the failure value from.</param>
    /// <returns>The failure value.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the attempt is a success.</exception>
    /// /// <exception cref="ArgumentNullException">Thrown if the attempt is null.</exception>"
    public static TF GetFailureValueOrThrow<TS, TF>(this Attempt<TS, TF> attempt)
    {
        if (attempt is null) throw new ArgumentNullException(nameof(attempt));
        if (attempt.IsSuccess)
        {
            throw new InvalidOperationException("Attempt is a success.");
        }
        return (attempt as Attempt<TS, TF>.FailureType<TS, TF>).Value;
    }

    /// <summary>
    /// Gets the failure value from an <see cref="Attempt{TFailure}"/> if it is a failure; otherwise, throws an <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    /// <param name="attempt">The attempt to extract the failure value from.</param>
    /// <returns>The failure value.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the attempt is a success.</exception>
    /// <exception cref="ArgumentNullException">Thrown if the attempt is null.</exception>"
    public static TF GetFailureValueOrThrow<TF>(this Attempt<TF> attempt)
    {
        if (attempt is null) throw new ArgumentNullException(nameof(attempt));
        if (attempt.IsSuccess)
        {
            throw new InvalidOperationException("Attempt is a success.");
        }
        return (attempt as Attempt<TF>.FailureType<TF>).Value;
    }

    /// <summary>
    /// Executes the specified function and returns a successful <see cref="Attempt{TSuccess, TException}"/> if no exception is thrown,
    /// or a failed attempt with the exception if one of type <typeparamref name="TException"/> is thrown.
    /// </summary>
    /// <typeparam name="TS">The type of the success value.</typeparam>
    /// <typeparam name="TException">The type of exception to catch.</typeparam>
    /// <param name="function">The function to execute.</param>
    /// <returns>An <see cref="Attempt{TSuccess, TException}"/> representing the result.</returns>
    public static Attempt<TS, TException> Try<TS, TException>(Func<TS> function) where TException : Exception
    {
        try
        {
            return Success<TS, TException>(function());
        }
        catch (TException ex)
        {
            return Failure<TS, TException>(ex);
        }
    }

    /// <summary>
    /// Executes the specified function and returns a successful <see cref="Attempt{TSuccess, Exception}"/> if no exception is thrown,
    /// or a failed attempt with the exception if any exception is thrown.
    /// </summary>
    /// <typeparam name="TS">The type of the success value.</typeparam>
    /// <param name="function">The function to execute.</param>
    /// <returns>An <see cref="Attempt{TSuccess, Exception}"/> representing the result.</returns>
    public static Attempt<TS, Exception> Try<TS>(Func<TS> function)
    {
        try
        {
            return Success<TS, Exception>(function());
        }
        catch (Exception ex)
        {
            return Failure<TS, Exception>(ex);
        }
    }

    /// <summary>
    /// Executes the specified action and returns a successful <see cref="Attempt{TException}"/> if no exception is thrown,
    /// or a failed attempt with the exception if one of type <typeparamref name="TException"/> is thrown.
    /// </summary>
    /// <typeparam name="TException">The type of exception to catch.</typeparam>
    /// <param name="action">The action to execute.</param>
    /// <returns>An <see cref="Attempt{TException}"/> representing the result.</returns>
    public static Attempt<TException> Try<TException>(Action action) where TException : Exception
    {
        try
        {
            action();
            return Success<TException>();
        }
        catch (TException ex)
        {
            return Failure(ex);
        }
    }

    /// <summary>
    /// Executes the specified action and returns a successful <see cref="Attempt{Exception}"/> if no exception is thrown,
    /// or a failed attempt with the exception if any exception is thrown.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <returns>An <see cref="Attempt{Exception}"/> representing the result.</returns>
    public static Attempt<Exception> Try(Action action)
    {
        try
        {
            action();
            return Success<Exception>();
        }
        catch (Exception ex)
        {
            return Failure(ex);
        }
    }

    /// <summary>
    /// Groups multiple <see cref="Attempt{TSuccess, TFailure}"/> instances into a single enumerable sequence.
    /// </summary>
    /// <typeparam name="TS">The type of the success value.</typeparam>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    /// <param name="first">The first attempt to include in the group. Optional.</param>
    /// <param name="attempts">Additional attempts to include in the group.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all provided attempts.</returns>
    public static IEnumerable<Attempt<TS, TF>> Group<TS, TF>(Attempt<TS, TF> first = null, params Attempt<TS, TF>[] attempts)
    {
        if (first is not null) yield return first;
        foreach (var item in attempts)
        {
            yield return item;
        }
    }

    /// <summary>
    /// Groups multiple <see cref="Attempt{TFailure}"/> instances into a single enumerable sequence.
    /// </summary>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    /// <param name="first">The first attempt to include in the group. Optional.</param>
    /// <param name="attempts">Additional attempts to include in the group.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all provided attempts.</returns>
    public static IEnumerable<Attempt<TF>> Group<TF>(Attempt<TF> first = null, params Attempt<TF>[] attempts)
    {
        if (first is not null) yield return first;
        foreach (var item in attempts)
        {
            yield return item;
        }
    }

    /// <summary>
    /// Groups multiple untyped <see cref="Attempt"/> instances into a single enumerable sequence.
    /// </summary>
    /// <param name="first">The first attempt to include in the group. Optional.</param>
    /// <param name="attempts">Additional attempts to include in the group.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all provided attempts.</returns>
    public static IEnumerable<Attempt> Group(Attempt first = null, params Attempt[] attempts)
    {
        if (first is not null) yield return first;
        foreach (var item in attempts)
        {
            yield return item;
        }
    }

    /// <summary>
    /// Returns all success values from a sequence of <see cref="Attempt{TSuccess, TFailure}"/> instances.
    /// </summary>
    /// <typeparam name="TS">The type of the success value.</typeparam>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    /// <param name="attempts">The sequence of attempts to inspect for successes.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all success values.</returns>
    public static IEnumerable<TS> GroupSuccesses<TS, TF>(IEnumerable<Attempt<TS, TF>> attempts)
    {
        return attempts.Where(a => a.IsSuccess).Select(GetSuccessValueOrThrow);
    }

    /// <summary>
    /// Returns all failure values from a sequence of <see cref="Attempt{TSuccess, TFailure}"/> instances.
    /// </summary>
    /// <typeparam name="TS">The type of the success value.</typeparam>
    /// <typeparam name="TF">The type of the failure value.</typeparam>
    /// <param name="attempts">The sequence of attempts to inspect for failures.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all failure values.</returns>
    public static IEnumerable<TF> GroupFailures<TS, TF>(IEnumerable<Attempt<TS, TF>> attempts)
    {
        return attempts.Where(a => a.IsFailure).Select(GetFailureValueOrThrow);
    }

    /// <summary>
    /// Aggregates all failure exceptions from a sequence of <see cref="Attempt{TSuccess, Exception}"/> instances into a single <see cref="AggregateException"/>.
    /// Returns true and sets <paramref name="failures"/> if any failures are found; otherwise, returns false and sets <paramref name="failures"/> to null.
    /// </summary>
    /// <typeparam name="TS">The type of the success value.</typeparam>
    /// <param name="attempts">The sequence of attempts to inspect for failures.</param>
    /// <param name="failures">When this method returns, contains the <see cref="AggregateException"/> if any failures are found; otherwise, null.</param>
    /// <returns>True if any failures are found; otherwise, false.</returns>
    public static bool TryAggregateExceptions<TS>(IEnumerable<Attempt<TS, Exception>> attempts, out AggregateException failures)
    {
        Exception[] problems = GroupFailures(attempts).ToArray();

        if (problems.Length > 0)
        {
            failures = new AggregateException("Múltiplos erros foram encontrados", problems);
            return true;
        }
        
        failures = null;
        return false;
    }
}
