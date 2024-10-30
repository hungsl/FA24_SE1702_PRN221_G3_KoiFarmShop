using Error = KoiFarmShop.Infrastructure.DTOs.Common.Error;

namespace KoiFarmShop.Application.Common.Result
{
    public class Result
    {
        private Result(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
            Errors = new List<Error>(); // Initialize Errors as an empty list by default
        }

        private Result(bool isSuccess, List<Error> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors ?? new List<Error>(); // Initialize with errors or an empty list
        }

        private Result(bool isSuccess, object obj, List<object> objects)
        {
            IsSuccess = isSuccess;
            Object = obj;
            Objects = objects ?? new List<object>(); // Initialize with objects or an empty list
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;

        public Error Error { get; }
        public List<Error> Errors { get; } = new List<Error>(); // Ensure Errors is always initialized
        public object Object { get; }
        public List<object> Objects { get; }

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static Result Failures(List<Error> errors) => new(false, errors ?? new List<Error>());
        public static Result SuccessWithObject(object obj) => new(true, obj, null);
        public static Result FailureWithObjects(List<object> objects) => new(false, null, objects);
    }

}
