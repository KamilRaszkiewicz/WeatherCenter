using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using WeatherCenter.Dto.Autocomplete;
using WeatherCenter.Dto.Weather;
using WeatherCenter.Dto.Weather.Weatherapi;
using WeatherCenter.Interfaces;

namespace WeatherCenter.Services
{
    public class WeatherapiWeatherService : IWeatherService
    {

        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherapiWeatherService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _httpClient = clientFactory.CreateClient("weatherapi.com");
            _apiKey = configuration["ApiKeys:weatherapi.com"];
        }

        public async Task<WeatherInfoDto> GetCurrentWeather(double latitude, double longitude)
        {
            var parameters = new Dictionary<string, string?>()
            {
                {"key", _apiKey},
                {"q", latitude.ToString() + "," + longitude.ToString() }
            };

            var query = QueryHelpers.AddQueryString("v1/current.json", parameters);

            var result = await _httpClient.GetAsync(query);

            if (!result.IsSuccessStatusCode)
            {
                return new WeatherInfoDto() 
                { 
                    IsSuccess = false
                };
            }

            var deserializedResult = JsonSerializer.Deserialize<WeatherapiCurrentResponse>(await result.Content.ReadAsStringAsync());

            if (deserializedResult == null)
            {
                return new WeatherInfoDto()
                {
                    IsSuccess = false
                };
            }

            return new WeatherInfoDto()
            {
                IsSuccess = true,

                Temperature = deserializedResult.current.temp_c,
                FeelsLikeTemperature = deserializedResult.current.feelslike_c,

                WindSpeed = deserializedResult.current.wind_kph,
                Humidity = deserializedResult.current.humidity,
                CloudPercentage = deserializedResult.current.cloud,

                LocalTime = deserializedResult.location.localtime,

                SummaryIconUrl = deserializedResult.current.condition.icon
            };

        }
    }
}
