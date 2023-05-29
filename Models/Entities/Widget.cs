﻿namespace WeatherCenter.Models.Entities
{
    public class Widget
    {
        public int Id { get; set; }

        public string City { get; set; }
        public string CountryCode { get; set; }
        public string? State { get; set; }

        public double Latitude { get; set; }
        public double Longtitude { get; set; }
    }
}