namespace WeatherForecast.Helpers
{
    public static class WeatherHelper
    {
        private const double KELVIN_AND_CELCIUS_DIFFERENCE = 273;

        public static double ConvertKelvinToCelsius(double temperature)
        {
            return Math.Round(temperature - KELVIN_AND_CELCIUS_DIFFERENCE);
        }
    }
}