using EquipmentsBusiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAuthorization.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RoomController : ControllerBase
{

    private readonly ILogger<RoomController> _logger;
    private readonly RoomBusiness _roomBusiness;

    public RoomController(ILogger<RoomController> logger, RoomBusiness roomBusiness)
    {
        _logger = logger;
        _roomBusiness = roomBusiness;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var response = _roomBusiness.GetAll();
        return Ok(response);
    }
}