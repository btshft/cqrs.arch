namespace Cqrs.WebApi.Infrastructure.Models
{
    public class ApiError
    {
        public string Message { get; }
        public string Description { get; }
        public string TraceId { get; }

        public ApiError(string message, string description, string traceId)
        {
            Message = message;
            Description = description;
            TraceId = traceId;
        }
    }
}