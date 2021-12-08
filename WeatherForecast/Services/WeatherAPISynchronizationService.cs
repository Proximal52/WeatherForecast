using WeatherForecast.Models;
using WeatherForecast.Services.Interfaces;

namespace WeatherForecast.Services
{
    public interface IWeatherAPISynchronizationService
    {
        public List<WeatherInfoModel> GetForecastsByCityName(string city);
    }

    public class WeatherAPISynchronizationService : IWeatherAPISynchronizationService
    {
        private readonly ILogger<WeatherAPISynchronizationService> _logger;

        private List<IWeatherService> weatherServices;
        public WeatherAPISynchronizationService(ILogger<WeatherAPISynchronizationService> logger,
            IOpenWeatherMapAPIService openWeatherMapAPIService,
            IWeatherAPIService weatherAPIService)
        {
            _logger = logger;

            weatherServices = new List<IWeatherService> { openWeatherMapAPIService, weatherAPIService };
        }

        public List<WeatherInfoModel> GetForecastsByCityName(string city)
        {
            return weatherServices.Select(x => x.GetWeatherByCityName(city)).ToList();
        }
    }
}
