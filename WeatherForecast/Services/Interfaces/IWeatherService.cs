using WeatherForecast.Models;

namespace WeatherForecast.Services.Interfaces
{
    public interface IAPIService
    {
        public Task<WeatherInfoModel> GetWeatherByCityNameAsync(string city);

        public Task<WeatherInfoModel> GetWeatherHistoryByCityNameAsync(string city);

        public string API_KEY { get; }
    }
}
