using WeatherCenter.Dto.Weather;

namespace WeatherCenter.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherInfoDto> GetCurrentWeather(double latitude, double longitude);
    }
}
