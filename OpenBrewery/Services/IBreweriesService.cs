using OpenBrewery.Models;

namespace OpenBrewery.Services;

public interface IBreweriesService
{
    Task<List<Brewery>> GetBreweriesAsync(
        string searchQuery, 
        string sortBy, 
        string sortOrder, 
        bool autocomplete);
}
