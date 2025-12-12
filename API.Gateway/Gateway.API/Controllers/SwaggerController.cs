using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers
{
    [ApiController]
    [Route("swagger/all")]
    public class SwaggerAggregatorController(IConfiguration config, IHttpClientFactory http) : ControllerBase
    {
        private readonly IConfiguration _config = config;
        private readonly IHttpClientFactory _http = http;

        [HttpGet]
        public IActionResult GetAllSwaggerJson()
        {
            var urls = _config.GetSection("Services")
                              .Get<Dictionary<string, string>>()!;

            var results = new Dictionary<string, object>();

            foreach (var entry in urls)
            {
                try
                {
                    var client = _http.CreateClient();
                    var url = entry.Value?.TrimEnd('/') + "/internal/swagger/v1/swagger.json";
                    var json = client.GetStringAsync(url).GetAwaiter().GetResult();
                    var doc = System.Text.Json.JsonSerializer.Deserialize<object>(json);

                    results[entry.Key] = doc!;
                }
                catch (Exception ex)
                {
                    results[entry.Key] = new { error = ex.Message };
                }
            }

            return Ok(results);
        }
        
        [HttpGet("remote/{service}/swagger.json")]
        public IActionResult GetRemoteSwagger(string service)
        {
            var urls = _config.GetSection("Services").Get<Dictionary<string, string>>();
            if (urls == null || !urls.TryGetValue(service, out var baseUrl) || string.IsNullOrWhiteSpace(baseUrl))
                return NotFound();

            try
            {
                var client = _http.CreateClient();
                var json = client.GetStringAsync(baseUrl.TrimEnd('/') + "/internal/swagger/v1/swagger.json").GetAwaiter().GetResult();
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(502, new { error = ex.Message });
            }
        }
    }
}
