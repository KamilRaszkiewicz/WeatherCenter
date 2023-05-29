using WeatherCenter.Dto.Weather;

namespace WeatherCenter.Dto.Widget
{
    public class GetWeatherWidgetDto
    {
        public int WidgetId { get; set; }
        public string WidgetName { get; set; }

        public string FullLocation { get; set; }

        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}
