using System.ComponentModel.DataAnnotations;

namespace WeatherCenter.Dto.Widget
{
    public class CreateWeatherWidgetDto
    {
        [Required(AllowEmptyStrings = false)]
        public string WidgetName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string PlaceId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string SecondPart { get; set; }
    }
}
