using EquipmentsBusiness;
using EquipmentsBusiness.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAuthorization.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EquipmentController : ControllerBase
{

    private readonly ILogger<EquipmentController> _logger;
    private readonly EquipmentBusiness _equipmentsBusiness;

    public EquipmentController(ILogger<EquipmentController> logger, EquipmentBusiness equipmentsBusiness)
    {
        _logger = logger;
        _equipmentsBusiness = equipmentsBusiness;
    }

    [Authorize(Roles = "Staff, Manager, Admin")]
    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var response = _equipmentsBusiness.GetAll();
        return Ok(response);
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpPost("Create")]
    public IActionResult Create([FromBody] CreateEquipmentRequest request)
    {
        var response = _equipmentsBusiness.Create(request);
        return Ok(response);
    }

    [Authorize(Roles = "Manager, Admin")]
    [HttpPut("Update")]
    public IActionResult Update([FromBody] UpdateEquipmentRequest request)
    {
        var response = _equipmentsBusiness.Update(request);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("Delete")]
    public IActionResult Delete([FromBody] DeleteEquipmentRequest request)
    {
        var response = _equipmentsBusiness.Delete(request);
        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}