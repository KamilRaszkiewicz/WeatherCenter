using WeatherCenter.Dto.Autocomplete;

namespace WeatherCenter.Interfaces
{
    public interface IAutocompleteService
    {
        Task<IEnumerable<CityAutocompleteDto>> GetAutocompletes(string searchQuery);
    }
}
