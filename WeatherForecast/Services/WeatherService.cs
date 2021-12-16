using AutoMapper;
using WeatherForecast.Models;
using WeatherForecast.Repositories;

namespace WeatherForecast.Services
{
    public interface IWeatherService
    {
        public void UpdateWeatherHistory();
        public List<WeatherInfoModel> GetWeatherByCityName(string city);
        public List<WeatherInfoModel> GetWeatherHistoryByCityId(int cityId);
        public List<WeatherInfoModel> GetWeatherHistoryByCityName(string cityName);
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

        public void UpdateWeatherHistory()
        {
            var cities = _weatherRepository.GetAllCities();
            List<WeatherInfoModel> newWeatherInfo = new();

            foreach (var city in cities)
            {
                var newWeather = _synchronizationService.GetWeatherByCityName(city.Name);

                newWeather.ForEach(x => x.City.Id = city.Id);

                newWeatherInfo.AddRange(newWeather);
            }

            var weatherDboList = newWeatherInfo.Select(x => _mapper.Map<WeatherDbo>(x)).ToList();

            _weatherRepository.SaveWeatherForHistory(weatherDboList);
        }

        public List<WeatherInfoModel> GetWeatherByCityName(string city)
        {
            return _synchronizationService.GetWeatherByCityName(city);
        }

        public List<WeatherInfoModel> GetWeatherHistoryByCityId(int cityId)
        {
            return _weatherRepository.GetWeatherHistoryByCityId(cityId)
                .Select(x => _mapper.Map<WeatherInfoModel>(x))
                .ToList();
        }

        public List<WeatherInfoModel> GetWeatherHistoryByCityName(string cityName)
        {
            return _synchronizationService.GetWeatherHistoryByCityName(cityName);
        }
    }
}
