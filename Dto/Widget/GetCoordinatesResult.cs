using WeatherCenter.Dto.Autocomplete;

namespace WeatherCenter.Dto.Widget
{
    public class GetCoordinatesResult
    {
        public bool Exists { get; set; }
        public Location Location { get; set; }
    }
}
