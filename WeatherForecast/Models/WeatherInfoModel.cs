using System.Text.Json.Serialization;
using WeatherForecast.Enums;

namespace WeatherForecast.Models
{
    public class WeatherInfoModel
    {
        public Guid Id { get; } = Guid.NewGuid();
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public APINames SourceAPIName { get; set; }
        public CityModel City { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public DateTime Date { get; set; }
    }
}
