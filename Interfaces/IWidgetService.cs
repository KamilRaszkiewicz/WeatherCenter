using WeatherCenter.Dto.Widget;

namespace WeatherCenter.Interfaces
{
    public interface IWidgetService
    {
        IEnumerable<GetWeatherWidgetResponseDto> GetWeatherWidgets();

        Task<GetWeatherWidgetResponseDto> CreateWeatherWidget(CreateWeatherWidgetDto dto);

        bool UpdateWeatherWidget(int widgetId, string newName);

        GetCoordinatesResult GetCoordinates(int widgetId);

        bool DeleteWeatherWidget(int widgetId);
    }
}
