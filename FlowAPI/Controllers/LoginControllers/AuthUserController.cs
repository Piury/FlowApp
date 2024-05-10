
using FlowApi.Enums;
using FlowApi.Models;
using FlowAPI.Controllers.BaseControllersl;
using FlowAPI.FlowDb.UserDb;
using FlowAPI.Services.Context;
using FlowAPI.Services.Errors;
using FlowAPI.Services.ResponseServices;
using FlowDb.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowAPI.Controllers.LoginControllers;
[ApiController]
[Route("api/[controller]")]
public class AuthUserController : FlowAPIBaseCController
{
    public AuthUserController(IJwtServices jwtServices, AppDbContext context, IErrorsServices errorsServices) : base(jwtServices, context, errorsServices)
    {
    }

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public async Task<IActionResult> Authenticate(IJwtServices JwtServices, UserCredentials credentials)
    {
        try
        {
            var UserRepository = new UserRepository(Context);
            var user = await UserRepository.GetByEmailAsync(credentials.Email);

            var hasher = new Hasher();
            if (user != null && hasher.VerifyPassword(credentials.Password, user.Password))
            {
                var token = JwtServices.GetToken(user.Name, user.Email, user.SuperAdmin);
                _ResponseType = ResponseType.Success;
                return Ok(ResponseHandler.GetAppResponse(_ResponseType, token));
            }

            return Unauthorized(ResponseHandler.GetAppResponse(_ResponseType, "Unauthorized Credentials"));
        }
        catch (Exception ex)
        {
            CreateLogErrors(ex);
            _ResponseType = ResponseType.Failure;
            return StatusCode(500, ResponseHandler.GetAppResponse(_ResponseType, ex.Message));
        }
    }
}