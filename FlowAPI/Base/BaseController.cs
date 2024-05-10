using FlowApi.Enums;
using FlowApi.Models.MongoDb;
using FlowAPI.Services.Context;
using FlowAPI.Services.Errors;
using FlowAPI.Utilities;
using FlowDb.Services.Token;
using Microsoft.AspNetCore.Mvc;

namespace FlowAPI.Controllers.BaseControllersl;


public class FlowAPIBaseCController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IJwtServices _IJwtServices;
    private readonly IErrorsServices _ErrorsServices;
    public ResponseType _ResponseType = ResponseType.Failure;

    protected FlowAPIBaseCController(IJwtServices IJwtServices, AppDbContext context, IErrorsServices errorsServices)
    {
        _context = context;
        _IJwtServices = IJwtServices;
        _ErrorsServices = errorsServices;
    }

    protected AppDbContext Context => _context;

    protected IJwtServices JwtServices => _IJwtServices;

    protected IErrorsServices ErrorsServices => _ErrorsServices;


    protected async void CreateLogErrors(Exception ex)
    {
        await ErrorsServices.CreateAsync(ex.GetErrorModel(_ResponseType));
    }
}