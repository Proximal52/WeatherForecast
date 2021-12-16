using WeatherForecast.Models;

namespace WeatherForecast.Services.Interfaces
{
    public interface IAPIService
    {
        public WeatherInfoModel GetWeatherByCityName(string city);

        public WeatherInfoModel GetWeatherHistoryByCityName(string city);

        public string API_KEY { get; }
    }
}
