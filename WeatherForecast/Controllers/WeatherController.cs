using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Services;

namespace WeatherForecast.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherByCityId(int cityId)
        {
            return Ok(await _weatherService.GetWeatherHistoryByCityIdAsync(cityId));
        }

        [HttpGet]
        public async Task<IActionResult> GetWeatherByCityName(string city)
        {
            return Ok(await _weatherService.GetWeatherByCityNameAsync(city));
        }
    }
}
