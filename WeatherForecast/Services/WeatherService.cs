using AutoMapper;
using WeatherForecast.Models;
using WeatherForecast.Models.Dbo;
using WeatherForecast.Repositories;

namespace WeatherForecast.Services
{
    public interface IWeatherService
    {
        public Task UpdateWeatherHistoryAsync();
        public Task<List<WeatherInfoModel>> GetWeatherByCityNameAsync(string city);
        public Task<List<WeatherInfoModel>> GetWeatherHistoryByCityIdAsync(int cityId);
        public Task<List<WeatherInfoModel>> GetWeatherHistoryByCityNameAsync(string cityName);
    }

    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IWeatherAPISynchronizationService _synchronizationService;
        private readonly IMapper _mapper;

        public WeatherService(
            IWeatherRepository weatherRepository,
            IWeatherAPISynchronizationService synchronizationService,
            IMapper mapper)
        {
            _weatherRepository = weatherRepository;
            _synchronizationService = synchronizationService;
            _mapper = mapper;
        }

        public async Task UpdateWeatherHistoryAsync()
        {
            var cities = await _weatherRepository.GetAllCitiesAsync();
            List<WeatherInfoModel> newWeatherInfo = new();

            foreach (var city in cities)
            {
                var newWeather = await _synchronizationService.GetWeatherByCityNameAsync(city.Name);

                newWeather.ForEach(x => x.City.Id = city.Id);

                newWeatherInfo.AddRange(newWeather);
            }

            var weatherDboList = newWeatherInfo.Select(x => _mapper.Map<WeatherDbo>(x)).ToList();

            await _weatherRepository.SaveWeatherForHistoryAsync(weatherDboList);
        }

        public async Task<List<WeatherInfoModel>> GetWeatherByCityNameAsync(string city)
        {
            return await _synchronizationService.GetWeatherByCityNameAsync(city);
        }

        public async Task<List<WeatherInfoModel>> GetWeatherHistoryByCityIdAsync(int cityId)
        {
            var result = await _weatherRepository.GetWeatherHistoryByCityIdAsync(cityId);
            return result.Select(x => _mapper.Map<WeatherInfoModel>(x)).ToList();
        }

        public async Task<List<WeatherInfoModel>> GetWeatherHistoryByCityNameAsync(string cityName)
        {
            return await _synchronizationService.GetWeatherHistoryByCityNameAsync(cityName);
        }
    }
}
