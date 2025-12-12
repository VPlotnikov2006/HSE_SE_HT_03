using Microsoft.AspNetCore.Mvc;

namespace FileAnalysis.API.Controllers;

[ApiController]
[Route("internal/swagger")]
public class InternalSwaggerController : ControllerBase
{
    [HttpGet("v1/swagger.json")]
    public IActionResult GetV1()
    {
        using var client = new HttpClient();
        var json = client.GetStringAsync("http://localhost:8080/swagger/v1/swagger.json").GetAwaiter().GetResult();
        return Content(json, "application/json");
    }
}
