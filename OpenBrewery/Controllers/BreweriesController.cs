using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenBrewery.Models;
using OpenBrewery.Services;

namespace OpenBrewery.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class BreweriesController : ControllerBase
{
    private readonly ILogger<BreweriesController> logger;
    private readonly IBreweriesService breweryService;

    public BreweriesController(ILogger<BreweriesController> logger, IBreweriesService breweryService)
    {
        this.logger = logger;
        this.breweryService = breweryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBreweriesAsync(
        [FromQuery] string sortBy = null,
        [FromQuery] string sortOrder = "asc",
        [FromQuery] string searchQuery = null,
        [FromQuery] bool autocomplete = false)
    {
        logger.LogInformation($"Fetching breweries with sortBy: {sortBy}, sortOrder: {sortOrder}, searchQuery: {searchQuery}, autocomplete: {autocomplete}");

        try
        {
            var breweries = await breweryService.GetBreweriesAsync(searchQuery, sortBy, sortOrder, autocomplete);

            List<BreweryDto> breweriesDto = ConvertToDto(breweries);

            var serializedDto = JsonConvert.SerializeObject(breweriesDto);
            logger.LogInformation("Successfully fetched breweries: {BreweriesDto}", serializedDto);

            return Ok(breweriesDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while fetching breweries");
            return StatusCode(500, "An error occurred while fetching breweries.");
        }
    }

    private static List<BreweryDto> ConvertToDto(List<Brewery> breweries)
    {
        return breweries.Select(brewery => new BreweryDto
        {
            Name = brewery.Name,
            City = brewery.City,
            Phone = brewery.Phone
        }).ToList();
    }
}
