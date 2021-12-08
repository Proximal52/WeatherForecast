namespace WeatherForecast.Models
{
    public class WeatherInfoModel
    {
        public int Id { get; set; } = Random.Shared.Next(int.MaxValue);
        public string SourceApiName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }
    }
}
