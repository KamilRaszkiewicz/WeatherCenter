using WeatherCenter.Dto.Widget;

namespace WeatherCenter.Interfaces
{
    public interface IWidgetService
    {
        IEnumerable<GetWeatherWidgetDto> GetWeatherWidgets();

        Task<bool> CreateWeatherWidget(CreateWeatherWidgetDto dto);
        bool DeleteWeatherWidget(int widgetId);
    }
}
