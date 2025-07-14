namespace Trichechus.Application.Common;

public class Result
{

	public bool IsSuccess { get; }
	public IEnumerable<string> Errors { get; }

	protected Result(bool isSuccess, IEnumerable<string> errors)
	{
		IsSuccess = isSuccess;
		Errors = errors;
	}

	public static Result Success() => new Result(true, Array.Empty<string>());
	public static Result Failure(IEnumerable<string> errors) => new Result(false, errors);
}

public class Result<T> : Result
{
	public T Value { get; }

	protected Result(T value, bool isSuccess, IEnumerable<string> errors)
		: base(isSuccess, errors)
	{
		Value = value;
	}

	public static Result<T> Success(T value) => new Result<T>(value, true, Array.Empty<string>());
	public static new Result<T> Failure(IEnumerable<string> errors) => new Result<T>(default!, false, errors);
}