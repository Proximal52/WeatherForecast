using Dapper;
using System.Data;
using System.Data.SqlClient;
using WeatherForecast.Models.Dbo;

namespace WeatherForecast.Repositories
{
    public interface IWeatherRepository
    {
        public Task<IEnumerable<WeatherDbo>> GetWeatherHistoryByCityIdAsync(int city);
        public Task<IEnumerable<CityDbo>> GetAllCitiesAsync();
        public Task SaveWeatherForHistoryAsync(List<WeatherDbo> newWeatherHistory);
    }

    public class WeatherRepository : IWeatherRepository
    {
        private readonly string _connectionString = @"Data Source=CMDB-102619\SQLEXPRESS;Initial Catalog=WeatherForecastDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public async Task<IEnumerable<WeatherDbo>> GetWeatherHistoryByCityIdAsync(int cityId)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT * FROM WeatherForecastHistory WHERE CityId = @cityId";
                return await connection.QueryAsync<WeatherDbo>(sqlQuery, cityId);
            }
        }

        public async Task<IEnumerable<CityDbo>> GetAllCitiesAsync()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = "SELECT Id, Name, Country FROM Cities";
                return await connection.QueryAsync<CityDbo>(sqlQuery);
            }
        }

        public async Task SaveWeatherForHistoryAsync(List<WeatherDbo> newWeatherHistory)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var sqlQuerry = "INSERT INTO WeatherForecastHistory (Id, Date, SourceAPIId, CityId, Temperature, Pressure, Humidity) VALUES (@Id, @Date, @SourceAPIId, @CityId, @Temperature, @Pressure, @Humidity)";

                var param = newWeatherHistory.Select(x => new
                {
                    Id = x.Id,
                    Date = x.Date,
                    SourceAPIId = x.SourceAPIId,
                    CityId = x.City.Id,
                    Temperature = x.Temperature,
                    Pressure = x.Pressure,
                    Humidity = x.Humidity
                });

                await connection.ExecuteAsync(sqlQuerry, param);
            }
        }
    }
}
