using AutoMapper;
using System.Text.Json;
using WeatherForecast.Models;
using WeatherForecast.Services.Interfaces;

namespace WeatherForecast.Services.APIServices
{
    public interface IOpenWeatherMapAPIService : IAPIService
    {

    }

    public class OpenWeatherMapAPIService : IOpenWeatherMapAPIService
    {
        private readonly DateTime _minDate = new DateTime(1970, 1, 1);

        private readonly HttpClient _httpClient;
        private readonly ILogger<OpenWeatherMapAPIService> _logger;
        private readonly IMapper _mapper;

        public string API_KEY => "2789f17e7392b9dbae54116a52f7f8bb";

        public OpenWeatherMapAPIService(ILogger<OpenWeatherMapAPIService> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _httpClient = new();
        }

        public async Task<WeatherInfoModel> GetWeatherByCityNameAsync(string city)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API_KEY}")
                };

                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<OpenWeatherMapModel>(body) ?? new();

                    return _mapper.Map<OpenWeatherMapModel, WeatherInfoModel>(result);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<WeatherInfoModel> GetWeatherHistoryByCityNameAsync(string city)
        {
            try
            {
                var startDateUnix = (int)DateTime.UtcNow.AddDays(-2).Subtract(_minDate).TotalSeconds;
                var endDateUnix = (int)DateTime.UtcNow.Subtract(_minDate).TotalSeconds;

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"http://history.openweathermap.org/data/2.5/history/city?q={city}&type=hour&start={startDateUnix}&end={endDateUnix}&appid={API_KEY}")
                };

                using (var response = await _httpClient.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();

                    var result = JsonSerializer.Deserialize<OpenWeatherMapModel>(body) ?? new();

                    return _mapper.Map<OpenWeatherMapModel, WeatherInfoModel>(result);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
