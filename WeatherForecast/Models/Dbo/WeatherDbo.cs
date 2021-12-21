namespace WeatherForecast.Models.Dbo
{
    public class WeatherDbo
    {
        public Guid Id { get; } = Guid.NewGuid();
        public int SourceAPIId { get; set; }
        public CityDbo City { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
        public DateTime Date { get; set; }

    }
}
