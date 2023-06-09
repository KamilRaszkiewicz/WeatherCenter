﻿using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using WeatherCenter.Dto.Autocomplete;
using WeatherCenter.Interfaces;

namespace WeatherCenter.Services.Apis
{
    public class GooglePlacesService : ILocationsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public GooglePlacesService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _httpClient = clientFactory.CreateClient("maps.googleapis.com");
            _apiKey = configuration["ApiKeys:googlePlaces"];
        }

        public async Task<IEnumerable<CityAutocompleteDto>> GetAutocompletes(string searchQuery)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"key", _apiKey},
                {"types", "(cities)" },
                {"input", searchQuery }
            };


            var query = QueryHelpers.AddQueryString("maps/api/place/autocomplete/json", parameters);

            var result = await _httpClient.GetAsync(query);

            if (!result.IsSuccessStatusCode)
            {
                return Enumerable.Empty<CityAutocompleteDto>();
            }

            var deserializedResult = JsonSerializer.Deserialize<GoogleApiAutocompleteResponseDto>(await result.Content.ReadAsStringAsync());

            if (deserializedResult == null || deserializedResult.status != "OK" || !deserializedResult.predictions.Any())
            {
                return Enumerable.Empty<CityAutocompleteDto>();
            }

            return deserializedResult.predictions.Select(x => new CityAutocompleteDto()
            {
                PlaceId = x.place_id,
                FullName = x.description,
                City = x.structured_formatting.main_text,
                SecondPart = x.structured_formatting.secondary_text
            });
        }

        public async Task<LocationInfo> GetCoordinates(string placeId)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"key", _apiKey},
                {"place_id", placeId },
                {"fields", "utc_offset,geometry/location" }
            };

            var query = QueryHelpers.AddQueryString("maps/api/place/details/json", parameters);

            var result = await _httpClient.GetAsync(query);

            if (!result.IsSuccessStatusCode)
            {
                return new LocationInfo()
                {
                    Latitude = 0,
                    Longtitude = 0,
                    UtcOffset = 0
                };
            }

            var deserializedResult = JsonSerializer.Deserialize<GoogleApiLocationResponseDto>(await result.Content.ReadAsStringAsync());

            if (deserializedResult == null || deserializedResult.status != "OK")
            {
                return new LocationInfo()
                {
                    Latitude = 0,
                    Longtitude = 0,
                    UtcOffset = 0
                };
            }

            return new LocationInfo()
            {
                Latitude = deserializedResult.result.geometry.location.lat,
                Longtitude = deserializedResult.result.geometry.location.lng,
                UtcOffset = deserializedResult.result.utc_offset
            };
        }
    }
}
