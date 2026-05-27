namespace Seguridad.API.Models.Common
{
    public class ApiSuccessResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public object? Errors { get; set; }

        public ApiSuccessResponse() { }

        public ApiSuccessResponse(T data, string message)
        {
            Data = data;
            Message = message;
        }
    }
}
