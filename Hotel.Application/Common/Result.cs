

namespace Hotel.Application.Common
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static Result<T> SuccessResult(T data) => new() { Success = true, Data = data };
        public static Result<T> Failure(string message) => new() { Success = false, Message = message };
    }
}
