using Microsoft.Extensions.Caching.Memory;
using OpenBrewery.HttpClients;
using OpenBrewery.Models;

namespace OpenBrewery.Services;

public class BreweriesService
{
    private readonly IMemoryCache cache;
    private readonly OpenBreweryDbClient openBreweryDbClient;
    private readonly TimeSpan cacheDuration;

    public BreweriesService(IMemoryCache cache, OpenBreweryDbClient openBreweryDbClient)
    {
        this.cache = cache;
        this.openBreweryDbClient = openBreweryDbClient;
        this.cacheDuration = TimeSpan.FromMinutes(10);
    }

    public async Task<List<Brewery>> GetBreweriesAsync(
        string searchQuery, string sortBy, string sortOrder, bool autocomplete)
    {
        string cacheKey = $"breweries_{searchQuery}_{sortBy}_{sortOrder}_{autocomplete}";

        return await cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SetSlidingExpiration(cacheDuration);
            var breweries = await openBreweryDbClient.GetBreweriesAsync(sortBy, sortOrder, searchQuery, autocomplete);
            return breweries;
        });
    }
}
