﻿namespace WeatherCenter.Dto.Weather
{
    public class WeatherInfoDto
    {
        public bool IsSuccess { get; set; }

        public double Temperature { get; set; }

        public double WindSpeed { get; set; }
        public int CloudPercentage { get; set; }

        public string SummaryIconUrl { get; set; }
    }
}
