using WeatherCenter.Dto.Weather;

namespace WeatherCenter.Dto.Widget
{
    public class GetWeatherWidgetResponseDto
    {
        public int WidgetId { get; set; }
        public string LocalTime { get; set; }
        public string WidgetName { get; set; }

        public string FullLocation { get; set; }
    }
}
