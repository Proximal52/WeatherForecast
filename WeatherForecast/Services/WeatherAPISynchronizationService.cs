using WeatherForecast.Models;
using WeatherForecast.Services.APIServices;
using WeatherForecast.Services.Interfaces;

namespace WeatherForecast.Services
{
    public interface IWeatherAPISynchronizationService
    {
        public Task<List<WeatherInfoModel>> GetWeatherByCityNameAsync(string city);
        public Task<List<WeatherInfoModel>> GetWeatherHistoryByCityNameAsync(string city);
    }

    public class WeatherAPISynchronizationService : IWeatherAPISynchronizationService
    {
        private readonly List<IAPIService> weatherServices;
        public WeatherAPISynchronizationService(IOpenWeatherMapAPIService openWeatherMapAPIService,
            IWeatherAPIService weatherAPIService)
        {
            weatherServices = new List<IAPIService> { openWeatherMapAPIService, weatherAPIService };
        }

        public async Task<List<WeatherInfoModel>> GetWeatherByCityNameAsync(string city)
        {
            var result = weatherServices.Select(x => x.GetWeatherByCityNameAsync(city));
            var resultList = new List<WeatherInfoModel>();


            foreach (var task in result)
            {
                resultList.Add(await task);
            }

            return resultList;

        }

        public async Task<List<WeatherInfoModel>> GetWeatherHistoryByCityNameAsync(string city)
        {
            var result = weatherServices.Select(x => x.GetWeatherHistoryByCityNameAsync(city));
            var resultList = new List<WeatherInfoModel>();


            foreach (var task in result)
            {
                resultList.Add(await task);
            }

            return resultList;

        }
    }
}
