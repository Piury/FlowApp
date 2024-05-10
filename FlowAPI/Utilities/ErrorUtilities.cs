using FlowApi.Enums;
using FlowApi.Models.MongoDb;
using FlowAPI.Services.Context;

namespace FlowAPI.Utilities;

public static class ErrorUtilities
{
    public static ErrorModel GetErrorModel(this Exception exception, ResponseType responseType)
    => new ErrorModel
    {
        Timestamp = DateTime.UtcNow,
        Level = responseType.ToString(),
        Message = exception.Message,
        Exception = exception, // Consider filtering sensitive information if needed
        StackTrace = exception.StackTrace,
    };
}