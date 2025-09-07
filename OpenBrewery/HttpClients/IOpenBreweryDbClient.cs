using OpenBrewery.Models;

namespace OpenBrewery.HttpClients;

public interface IOpenBreweryDbClient
{
    Task<List<Brewery>> GetBreweriesAsync(
        string sortBy,
        string sortOrder,
        string searchQuery,
        bool isAutocomplete);
}
