using EquipmentsBusiness;
using EquipmentsBusiness.Request;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAuthorization.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AccountController : ControllerBase
{

    private readonly ILogger<AccountController> _logger;    
    private readonly AccountBusiness _accountBusiness;

    public AccountController(ILogger<AccountController> logger, IConfiguration config, AccountBusiness accountBusiness)
    {
        _logger = logger;
        _accountBusiness = accountBusiness;
    }


    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var response = _accountBusiness.AuthenticateAccount(request);
        if (response.IsSuccess)
        {
            return Ok(response);
        }
        return Unauthorized(response);
    }

    
}