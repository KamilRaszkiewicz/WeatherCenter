using WeatherCenter.Dto.Autocomplete;

namespace WeatherCenter.Interfaces
{
    public interface ILocationsService
    {
        Task<IEnumerable<CityAutocompleteDto>> GetAutocompletes(string searchQuery);

        Task<LocationInfo> GetCoordinates(string placeId);
    }
}
