using System.Text;
using AuthenticationAuthorizationFE.Response;
using EquipmentsBusiness.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthenticationAuthorizationFE.Controllers;

public class RoomController : Controller

{
    private readonly string API_URL_ENDPOINT = "https://localhost:7088/api/v1/Room";

    public RoomController()
    {
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(API_URL_ENDPOINT + "/GetAll"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var contentString = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<BusinessResult>(contentString)!.Data!;
                        var responses = JsonConvert.DeserializeObject<List<RoomResponse>>(data.ToString()!);
                        return Ok(responses);
                    }
                    var errorMessage = $"Request failed with status code: {response.StatusCode} and reason: {response.ReasonPhrase}";
                    return StatusCode((int)response.StatusCode, errorMessage);
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}