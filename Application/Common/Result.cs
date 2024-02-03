
namespace Application.Common
{
    public class Result<TValue>
    {
        private Result(bool isSuccess, Error error, TValue? value)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
            Value = value;
        }

        private Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None ||
                !isSuccess && error == Error.None)
            {
                throw new ArgumentException("Invalid error", nameof(error));
            }

            IsSuccess = isSuccess;
            Error = error;
        }



        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public TValue? Value { get; }
        public Error Error { get; }
        public static Result<TValue> Success(TValue value) => new(true, Error.None, value);
        public static Result<TValue> Success() => new(true, Error.None);
        public static Result<TValue> Failure(Error error) => new(false, error);
    }
}