namespace FlowApi.Models.ResponseModels;

public class ApiResponseServicesModel<T>
{
    public bool Success { get; private set; }
    public string Message { get; private set; }
    public T Data { get; private set; }

    private ApiResponseServicesModel(bool success, string message, T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static ApiResponseServicesModel<T> SuccessResponse(T data, string message = "")
        => new ApiResponseServicesModel<T>(true, message, data);

    public static ApiResponseServicesModel<T> NotFoundResponse(string message)
        => new ApiResponseServicesModel<T>(false, message, default);

    public static ApiResponseServicesModel<T> ErrorResponse(string message)
        => new ApiResponseServicesModel<T>(false, message, default);

    public static ApiResponseServicesModel<T> BadRequest(T data, string message)
        => new ApiResponseServicesModel<T>(false, message, data);
}
