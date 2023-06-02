namespace WeatherCenter.Models.Entities
{
    public class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public string? Region { get; set; }

        public string FullLocation { get; set; }

        public int UtcOffset { get; set; }
        public double Latitude { get; set; }
        public double Longtitude { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
