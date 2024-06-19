using System.Net;
using AuthenticationAuthorizationFE.Response;
using EquipmentsBusiness.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthenticationAuthorizationFE.Controllers;

public class EquipmentController : Controller

{
    private readonly string API_URL_ENDPOINT = "https://localhost:7088/api/v1/Equipment";

    public EquipmentController()
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
                var token = HttpContext.Session.GetString("accessToken") ?? "";
                // Set the Authorization header with the Bearer token
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                using (var response = await httpClient.GetAsync(API_URL_ENDPOINT + "/GetAll"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var contentString = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<BusinessResult>(contentString)!.Data!;
                        var responses = JsonConvert.DeserializeObject<List<EquipmentResponse>>(data.ToString()!);
                        return Ok(responses);
                    }

                    if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
                    {
                        var contentString = await response.Content.ReadAsStringAsync();
                        var errorMessage = JsonConvert.DeserializeObject<BusinessResult>(contentString)!.Message;
                        return BadRequest(errorMessage);
                    }

                    else
                    {
                        var errorMessage = $"Request failed with status code: {response.StatusCode} and reason: {response.ReasonPhrase}";
                        return StatusCode((int)response.StatusCode, errorMessage);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}