using System;

namespace CodeFlow
{
    public class Result : Result<Unit>
    {
	    protected internal Result(bool isSuccess, string error)
		    : base(Unit.Value, isSuccess, error)
	    {
	    }

        public static Result Combine(params Result[] results)
	    {
		    foreach (Result result in results)
		    {
			    if (result.IsFailure)
				    return result;
		    }

		    return Ok();
	    }

        public static Result Ok()
	    {
		    return new Result(true, string.Empty);
	    }

        public static Result<T> Fail<T>(string message)
	    {
		    return new Result<T>(default(T), false, message);
	    }

	    public static Result<T> Ok<T>(T value)
	    {
		    return new Result<T>(value, true, string.Empty);
	    }

        public static Result Fail(string message)
	    {
		    return new Result(false, message);
	    }
    }
    
    public class Result<T> 
    {
	    private readonly T _value;

        public bool IsSuccess { get; }
	    public string Error { get; }
	    public bool IsFailure => !IsSuccess;

	    public Result(T value, bool isSuccess, string error)
	    {
		    if (isSuccess && error != string.Empty)
			    throw new InvalidOperationException();
		    if (!isSuccess && error == string.Empty)
			    throw new InvalidOperationException();

		    IsSuccess = isSuccess;
		    Error = error;
            this._value = value;
	    }

	    public T Value
	    {
		    get
		    {
			    if (!IsSuccess)
				    throw new InvalidOperationException();

			    return _value;
		    }
	    }

	    
    }
}
