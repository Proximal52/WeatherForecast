using AutoMapper;
using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using React.AspNet;
using WeatherForecast.Configuration;
using WeatherForecast.Services;

namespace Startup
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services);

            var app = builder.Build();

            ConfigureApp(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvc();
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddTransient<IWeatherAPISynchronizationService, WeatherAPISynchronizationService>();
            serviceCollection.AddTransient<IOpenWeatherMapAPIService, OpenWeatherMapAPIService>();
            serviceCollection.AddTransient<IWeatherAPIService, WeatherAPIService>();
            serviceCollection.AddReact();
            serviceCollection.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName).AddChakraCore();
            serviceCollection.AddAutoMapper(cfg => cfg.AddProfile<WeatherProfile>());
        }

        private static void ConfigureApp(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseDeveloperExceptionPage();
            app.UseReact(config => { });

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Weather}/{action=Index}/{id?}");
        }
    }
}