using FlowApi.Enums;
using FlowApi.Models.MongoDb;
using FlowAPI.Services.Errors;
using FlowAPI.Services.ResponseServices;
using Microsoft.AspNetCore.Mvc;

namespace FlowAPI.Controllers.ErrorsControllers;

[ApiController]
[Route("api/[controller]")]
public class ErrorsController : ControllerBase
{
    /// <summary>
    /// Dependency injected instance of the ErrorsServices class.
    /// </summary>
    private readonly IErrorsServices _errorsServices;
    private ResponseType _ResponseType = ResponseType.Failure;

    public ErrorsController(IErrorsServices errorsServices)
    {
        _errorsServices = errorsServices;
    }

    /// <summary>
    /// Retrieves a list of all logged errors.
    /// </summary>
    /// <returns>A list of ErrorModel objects representing logged errors.</returns>
    [HttpGet]
    public async Task<IActionResult> GetErrors()
    {
        var errors = await _errorsServices.GetAsync();
        return Ok(errors);
    }

    /// <summary>
    /// Retrieves a specific error by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the error to retrieve.</param>
    /// <returns>An ErrorModel object representing the requested error, or NotFound if not found.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetError(string id)
    {
        var error = await _errorsServices.GetAsync(id);
        if (error == null)
        {
            _ResponseType = ResponseType.NotFound;
            return NotFound(ResponseHandler.GetAppResponse(_ResponseType, null));
        }
        _ResponseType = ResponseType.Success;
        return Ok(ResponseHandler.GetAppResponse(_ResponseType, error));
    }

    /// <summary>
    /// Creates a new error log entry.
    /// </summary>
    /// <param name="newError">An ErrorModel object representing the error to be logged.</param>
    /// <returns>A CreatedAtRoute result indicating the newly created error and its location.</returns>
    /// <remarks>
    /// The request body should contain a valid ErrorModel object with all required properties.
    /// The endpoint validates the request body before creating the error log entry.
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> AddError([FromBody] ErrorModel newError)
    {
        if (!ModelState.IsValid)
        {
            _ResponseType = ResponseType.Failure;
            return BadRequest(ResponseHandler.GetAppResponse(_ResponseType, ModelState));
        }

        await _errorsServices.CreateAsync(newError);
        _ResponseType = ResponseType.Success;
        return Ok(ResponseHandler.GetAppResponse(_ResponseType, "Success"));
    }

    /// <summary>
    /// Deletes a specific error log entry by its ID.
    /// </summary>
    /// <param name="id">The unique identifier of the error to delete.</param>
    /// <returns>A NoContent response indicating successful deletion.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteError(string id)
    {
        await _errorsServices.RemoveAsync(id);
        return NoContent();
    }
}


