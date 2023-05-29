namespace WeatherCenter.Dto.Autocomplete
{
    public class CityAutocompleteDto
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }

        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}
