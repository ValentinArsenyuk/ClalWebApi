using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static ClalWebApi.Models.Watcher;

namespace ClalWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast/{city}")]
        public string Get(string city)
        {
            var responseJson = GetCityInfo(city);
            var deptList = JsonSerializer.Deserialize<WatcherInfo>(responseJson.Result);

            return $"the weatehr in {city} is: {deptList?.current.temp_c} condition {deptList?.current.condition.text}";
        }

        private static HttpClient httpClient = new()
        {
            
        };
        private async Task<string> GetCityInfo(string city)
        {
            using HttpResponseMessage response = await httpClient.GetAsync($"https://api.weatherapi.com/v1/forecast.json?key=39f8ecaf506c4f76b3f55139222906&q={city}&days=3&aqi=yes&alerts=yes");
            var statusCode = response.EnsureSuccessStatusCode();
            var json =  await response.Content.ReadAsStringAsync();

            return json;
        }
    }
}