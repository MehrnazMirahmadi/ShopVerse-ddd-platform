namespace ShopVerse.BuildingBlocks.Result;
public class Result
{
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public string Message { get; init; } = string.Empty;

    public static Result Success(string message = "Success") =>
        new() { IsSuccess = true, Message = message };

    public static Result Fail(string message) =>
        new() { IsSuccess = false, Message = message };
}

public class Result<T> : Result
{
    public T? Value { get; init; }

    public static Result<T> Success(T value, string message = "Success") =>
        new() { IsSuccess = true, Value = value, Message = message };

    public new static Result<T> Fail(string message) =>
        new() { IsSuccess = false, Value = default, Message = message };
}

