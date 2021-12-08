using WeatherForecast.Models;

namespace WeatherForecast.Services.Interfaces
{
    public interface IWeatherService
    {
        public WeatherInfoModel GetWeatherByCityName(string city);


        public string API_KEY { get; }
    }
}
