using AutoMapper;
using System.Text.Json;
using WeatherForecast.Models;
using WeatherForecast.Services.Interfaces;

namespace WeatherForecast.Services
{
    public interface IOpenWeatherMapAPIService : IWeatherService
    {

    }

    public class OpenWeatherMapAPIService : IOpenWeatherMapAPIService
    {
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

        public WeatherInfoModel GetWeatherByCityName(string city)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={API_KEY}")
                };

                using (var response = _httpClient.SendAsync(request).Result)
                {
                    response.EnsureSuccessStatusCode();
                    var body = response.Content.ReadAsStringAsync().Result;

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
