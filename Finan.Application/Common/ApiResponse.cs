using Finan.Domain.Messages;

namespace Finan.Application.Common
{
    public class ApiResponse
    {
        public object? Data { get; set; }
        public string[]? Messages { get; set; }
        public bool Success { get; set; }

        public ApiResponse(object? data = null, string[]? messages = null, bool success = true)
        {
            Data = data;
            Messages = messages;
            Success = success;
        }

    }
}
