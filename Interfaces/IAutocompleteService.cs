using WeatherCenter.Dto.Autocomplete;

namespace WeatherCenter.Interfaces
{
    public interface IAutocompleteService
    {
        Task<IEnumerable<CityAutocompleteDto>> GetAutocompletes(string searchQuery);

        Task<Location> GetCoordinates(string placeId);
    }
}
