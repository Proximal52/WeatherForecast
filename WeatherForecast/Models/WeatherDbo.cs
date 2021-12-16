namespace WeatherForecast.Models
{
    public class WeatherDbo
    {
        public Guid Id { get; } = Guid.NewGuid();
        public int SourceAPIId { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public DateTime Date { get; set; }

    }
}
