using Dapper;
using System.Data;
using System.Data.SqlClient;
using WeatherForecast.Models;

namespace WeatherForecast.Repositories
{
    public interface IWeatherRepository
    {
        public List<WeatherDbo> GetWeatherHistoryByCityId(int city);
        public List<CityModel> GetAllCities();
        public void SaveWeatherForHistory(List<WeatherDbo> newWeatherHistory);
    }

    public class WeatherRepository : IWeatherRepository
    {
        private readonly string _connectionString = @"Data Source=CMDB-102619\SQLEXPRESS;Initial Catalog=WeatherForecastDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<WeatherDbo> GetWeatherHistoryByCityId(int cityId)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT * FROM WeatherForecastHistory WHERE CityId = @cityId";
                return connection.Query<WeatherDbo>(sqlQuery, cityId).ToList();
            }
        }

        public List<CityModel> GetAllCities()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT Id, Name, Country FROM Cities";
                return connection.Query<CityModel>(sqlQuery).ToList();
            }
        }

        public void SaveWeatherForHistory(List<WeatherDbo> newWeatherHistory)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuerry = "INSERT INTO WeatherForecastHistory (Id, Date, SourceAPIId, CityId, Temperature, Pressure, Humidity) VALUES (@Id, @Date, @SourceAPIId, @CityId, @Temperature, @Pressure, @Humidity)";
                Task.Run(async () => await connection.ExecuteAsync(sqlQuerry, newWeatherHistory));
            }
        }
    }
}
