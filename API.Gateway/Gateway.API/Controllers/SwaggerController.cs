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
                    var json = client.GetStringAsync($"{entry.Value}/swagger/v1/swagger.json").GetAwaiter().GetResult();
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
    }
}
