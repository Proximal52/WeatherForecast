using AutoMapper;
using WeatherAPI.Standard;
using WeatherForecast.Models;
using WeatherForecast.Services.Interfaces;

namespace WeatherForecast.Services.APIServices
{
    public interface IWeatherAPIService : IAPIService
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

        public async Task<WeatherInfoModel> GetWeatherByCityNameAsync(string city)
        {
            var weather = await _weatherAPIClient.APIs.GetRealtimeWeatherAsync(city);

            return _mapper.Map<WeatherInfoModel>(weather);
        }

        public async Task<WeatherInfoModel> GetWeatherHistoryByCityNameAsync(string city)
        {
            throw new NotImplementedException();
        }
    }
}
