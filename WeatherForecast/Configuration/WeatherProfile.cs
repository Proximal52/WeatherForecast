using AutoMapper;
using WeatherAPI.Standard.Models;
using WeatherForecast.Enums;
using WeatherForecast.Helpers;
using WeatherForecast.Models;
using WeatherForecast.Models.Dbo;

namespace WeatherForecast.Configuration
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<OpenWeatherMapModel, WeatherInfoModel>()
                .ForPath(dest => dest.City.Name, opt => opt.MapFrom(src => src.name))
                .ForPath(dest => dest.City.Country, opt => opt.MapFrom(src => src.sys.country))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => WeatherHelper.ConvertKelvinToCelsius(src.main.temp)))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.main.pressure))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.main.humidity))
                .ForMember(dest => dest.SourceAPIName, opt => opt.MapFrom(src => APINames.OpenWeatherMapAPI))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<CurrentJsonResponse, WeatherInfoModel>()
                .ForPath(dest => dest.City.Name, opt => opt.MapFrom(src => src.Location.Name))
                .ForPath(dest => dest.City.Country, opt => opt.MapFrom(src => src.Location.Country))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Current.TempC))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Current.PressureMb))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Current.Humidity))
                .ForMember(dest => dest.SourceAPIName, opt => opt.MapFrom(src => APINames.WeatherAPI))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Current.LastUpdated)));

            CreateMap<WeatherInfoModel, WeatherDbo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.SourceAPIId, opt => opt.MapFrom(src => src.SourceAPIName))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Pressure))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Humidity))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.SourceAPIName, opt => opt.MapFrom(src => (APINames)src.SourceAPIId))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Pressure))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Humidity))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));

            CreateMap<CityModel, CityDbo>().ReverseMap();
        }
    }
}
