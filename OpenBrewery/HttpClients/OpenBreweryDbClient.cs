using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OpenBrewery.Models;

namespace OpenBrewery.HttpClients;

public class OpenBreweryDbClient
{
    private readonly string baseUrl;
    private readonly HttpClient httpClient;

    public OpenBreweryDbClient(IOptions<ApiSettings> options)
    {
        baseUrl = options.Value.BaseUrl;
        httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };
        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<List<Brewery>> GetBreweriesAsync(
        string sortBy,
        string sortOrder,
        string searchQuery,
        bool isAutocomplete)
    {
        var url = BuildRequestUrl(sortBy, sortOrder, searchQuery, isAutocomplete);

        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<Brewery>>(content);
    }

    private string BuildRequestUrl(
        string sortBy,
        string sortOrder,
        string searchQuery,
        bool isAutocomplete)
    {
        var url = baseUrl;
        var queryParams = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            url += isAutocomplete ? "/autocomplete" : "/search";
            queryParams["query"] = searchQuery;
        }

        if (!string.IsNullOrWhiteSpace(sortBy) && !string.IsNullOrWhiteSpace(sortOrder))
        {
            queryParams["sort"] = $"{sortBy}:{sortOrder}";
        }

        return QueryHelpers.AddQueryString(url, queryParams);
    }
}
