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
        public IActionResult GetWeatherByCityId(int cityId)
        {
            return Ok(_weatherService.GetWeatherHistoryByCityId(cityId));
        }

        [HttpGet]
        public IActionResult GetWeatherByCityName(string city)
        {
            return Ok(_weatherService.GetWeatherByCityName(city));
        }
    }
}
