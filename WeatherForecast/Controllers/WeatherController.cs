using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Services;

namespace WeatherForecast.Controllers
{
    public class WeatherController : Controller
    {
        private readonly IWeatherAPISynchronizationService _weatherAPISynchronizationService;

        public WeatherController(IWeatherAPISynchronizationService weatherAPISynchronizationService)
        {
            _weatherAPISynchronizationService = weatherAPISynchronizationService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetForecastsByCityName(string city)
        {
            return Ok(_weatherAPISynchronizationService.GetForecastsByCityName(city));
        }
    }
}
