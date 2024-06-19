using System.Net;
using System.Text;
using AuthenticationAuthorizationFE.Request;
using AuthenticationAuthorizationFE.Response;
using EquipmentsBusiness.Base;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuthenticationAuthorizationFE.Controllers;

public class AccountController : Controller
{
    private readonly string API_URL_ENDPOINT = "https://localhost:7088/api/v1/Account";

    public AccountController()
    {
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(API_URL_ENDPOINT + "/Login", content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var contentString = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<BusinessResult>(contentString)!.Data!;
                        var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(data.ToString()!);
                        HttpContext.Session.SetString("accessToken", loginResponse!.Token);
                        HttpContext.Session.SetString("role", loginResponse.Role);
                        return Ok(loginResponse);
                    }

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
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