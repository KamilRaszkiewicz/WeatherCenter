namespace WeatherCenter.Dto.Autocomplete
{

    public class GoogleApiLocationResponseDto
    {
        public object[] html_attributions { get; set; }
        public Result result { get; set; }
        public string status { get; set; }
    }

    public class Result
    {
        public Geometry geometry { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

}
