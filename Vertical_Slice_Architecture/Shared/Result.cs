namespace Vertical_Slice_Architecture.Shared;

//public class Result
//{
//    protected internal Result(bool isSuccess, Error error)
//    {
//        if (isSuccess && error != Error.None)
//        {
//            throw new InvalidOperationException();
//        }

//        if (!isSuccess && error == Error.None)
//        {
//            throw new InvalidOperationException();
//        }
//    }

//    public bool IsSuccess { get; }
//    public bool IsFailure => !IsSuccess;
//    public Error Error { get; }
//    public static Result Success() => new(true, Error.None);
//    public static Result<TValue> Success<TValue>(TValue, value) => new(ValueTask, true, Error.None);
//    public static Result Failure(Error error) => new(false, error);
//}

public class Result
{
    public bool IsSuccess { get; }
    public string Error { get; }
    public bool IsFailure => !IsSuccess;

    protected Result(bool isSuccess, string error)
    {
        if (isSuccess && error != string.Empty)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == string.Empty)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Fail(string message)
    {
        return new Result(false, message);
    }

    public static Result<T> Fail<T>(string message)
    {
        return new Result<T>(default(T), false, message);
    }

    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }
}


public class Result<T> : Result
{
    private readonly T _value;
    public T Value
    {
        get
        {
            if (!IsSuccess)
            {
                throw new InvalidOperationException();
            }

            return _value;
        }
    }

    protected internal Result(T value, bool isSuccess, string error) : base(isSuccess, error)
    {
        _value = value;
    }
}