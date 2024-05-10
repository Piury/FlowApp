using FlowApi.Models;
using FlowApi.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace FlowAPI.Services.ResponseService;
public class ResponseService
{
    public ResponseService()
    {

    }
    public IActionResult CreateResponse<T>(ApiResponseServicesModel<T> apiResponse)
    {
        if (apiResponse.Success)
            return new OkObjectResult(apiResponse);
        else if (apiResponse.Data != null && !string.IsNullOrEmpty(apiResponse.Message))
            return new BadRequestObjectResult(apiResponse);
        else if (apiResponse.Data == null)
            return new NotFoundObjectResult(apiResponse);
        return new ObjectResult(apiResponse) { StatusCode = 500 };

    }
}