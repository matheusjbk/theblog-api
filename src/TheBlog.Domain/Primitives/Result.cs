using TheBlog.Domain.Errors;

namespace TheBlog.Domain.Primitives;

public class Result
{
    public bool IsSuccess { get; private set; }
    public IError? Error { get; private set; }

    protected Result(bool isSuccess = true, IError? error = null)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new();
    public static Result Failure(IError error) => new(false, error);
}

public class ResultValue<T> : Result
{
    private readonly T? _value;
    public T? Value => IsSuccess ? _value : default;

    protected ResultValue(T? value, bool isSuccess, IError? error) : base(isSuccess, error) => _value = value;

    public static ResultValue<T> Success(T value) => new(value, true, null);
    public static new ResultValue<T> Failure(IError error) => new(default, false, error);
}