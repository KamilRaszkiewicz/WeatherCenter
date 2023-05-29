using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using WeatherCenter.Dto.Autocomplete;
using WeatherCenter.Interfaces;

namespace WeatherCenter.Services
{
    public class WeatherapiAutocompleteService : IAutocompleteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherapiAutocompleteService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _httpClient = clientFactory.CreateClient("weatherapi.com");
            _apiKey = configuration["ApiKeys:weatherapi.com"];
        }

        public async Task<IEnumerable<CityAutocompleteDto>> GetAutocompletes(string searchQuery)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"key", _apiKey},
                {"q", searchQuery }
            };

            var query = QueryHelpers.AddQueryString("v1/search.json", parameters);

            var result = await _httpClient.GetAsync(query);

            if (!result.IsSuccessStatusCode)
            {
                return Enumerable.Empty<CityAutocompleteDto>();
            }

            var deserializedResult = JsonSerializer.Deserialize<IEnumerable<WeatherapiAutocompleteResponseDto>>(await result.Content.ReadAsStringAsync());

            if (deserializedResult == null || !deserializedResult.Any())
            {
                return Enumerable.Empty<CityAutocompleteDto>();
            }

            return deserializedResult.Select(x => new CityAutocompleteDto()
            {
                Name = x.name,
                Region = x.region,
                Country = x.country,
                Latitude = x.lat,
                Longtitude = x.lon,
            });
        }
    }
}
