namespace Finan.Contracts.Response
{
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public List<string> Messages { get; set; } = new();
        public bool Success { get; set; }
        public bool Unauthorized { get; set; }

        public ApiResponse() { }

        public ApiResponse(object? data = null, string[]? messages = null, bool success = true)
        {
            Data = (T?)data;
            Messages = messages?.ToList() ?? new List<string>();
            Success = success;
        }

    }
}
