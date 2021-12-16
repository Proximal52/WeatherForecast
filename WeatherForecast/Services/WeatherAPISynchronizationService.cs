using WeatherForecast.Models;
using WeatherForecast.Services.APIServices;
using WeatherForecast.Services.Interfaces;

namespace WeatherForecast.Services
{
    public interface IWeatherAPISynchronizationService
    {
        public List<WeatherInfoModel> GetWeatherByCityName(string city);
        public List<WeatherInfoModel> GetWeatherHistoryByCityName(string city);
    }

    public class WeatherAPISynchronizationService : IWeatherAPISynchronizationService
    {
        private readonly List<IAPIService> weatherServices;
        public WeatherAPISynchronizationService(IOpenWeatherMapAPIService openWeatherMapAPIService,
            IWeatherAPIService weatherAPIService)
        {
            weatherServices = new List<IAPIService> { openWeatherMapAPIService, weatherAPIService };
        }

        public List<WeatherInfoModel> GetWeatherByCityName(string city)
        {
            return weatherServices.Select(x => x.GetWeatherByCityName(city)).ToList();
        }

        public List<WeatherInfoModel> GetWeatherHistoryByCityName(string city)
        {
            return weatherServices.Select(x => x.GetWeatherHistoryByCityName(city)).ToList();
        }
    }
}
