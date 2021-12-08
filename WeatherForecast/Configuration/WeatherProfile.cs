using AutoMapper;
using WeatherAPI.Standard.Models;
using WeatherForecast.Models;

namespace WeatherForecast.Configuration
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile()
        {
            CreateMap<OpenWeatherMapModel, WeatherInfoModel>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.sys.country))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.main.temp))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.main.pressure))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.main.humidity))
                .ForMember(dest => dest.SourceApiName, opt => opt.MapFrom(src => "OpenWeatherMap"));

            CreateMap<CurrentJsonResponse, WeatherInfoModel>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Location.Name))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Location.Country))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Current.TempC))
                .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Current.PressureMb))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Current.Humidity))
                .ForMember(dest => dest.SourceApiName, opt => opt.MapFrom(src => "WeatherAPI"));
        }
    }
}
