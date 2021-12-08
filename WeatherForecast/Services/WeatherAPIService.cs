using AutoMapper;
using WeatherAPI.Standard;
using WeatherForecast.Models;
using WeatherForecast.Services.Interfaces;

namespace WeatherForecast.Services
{
    public interface IWeatherAPIService : IWeatherService
    {

    }

    public class WeatherAPIService : IWeatherAPIService
    {
        public string API_KEY => "6d6e92c6609b4ef7a4294349210312";

        private readonly WeatherAPIClient _weatherAPIClient;
        private readonly IMapper _mapper;

        public WeatherAPIService(IMapper mapper)
        {
            _mapper = mapper;
            _weatherAPIClient = new(API_KEY);
        }

        public WeatherInfoModel GetWeatherByCityName(string city)
        {
            try
            {
                var weather = _weatherAPIClient.APIs.GetRealtimeWeather(city);

                //_weatherAPIClient.APIs.

                return _mapper.Map<WeatherInfoModel>(weather);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
