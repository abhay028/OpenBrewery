# OpenBrewery (.NET 8)

## Project Structure

- **Controllers**: API endpoints (e.g., `BreweriesController`) for brewery data.
- **Services**: Business logic and caching (`BreweriesService`).
- **HttpClients**: External API communication (`OpenBreweryDbClient`).
- **Models**: Data structures (e.g., `Brewery`).
- **Program.cs**: Configures DI, API versioning, Swagger, and middleware.

## Design Decisions

- **Dependency Injection**: All services and clients are registered for DI, promoting testability and separation of concerns.
- **Caching**: `BreweriesService` uses `IMemoryCache` to cache brewery data for a configurable duration.
- **External API Client**: `OpenBreweryDbClient` abstracts calls to the Open Brewery DB API, building URLs dynamically.
- **API Versioning**: Implemented via `Asp.Versioning` for future extensibility.
- **Swagger**: Enabled for easy API exploration and documentation.

## How to Run

1. Ensure you have .NET 8 SDK installed.
2. Restore dependencies:
3. Build the project:
4. Run the API:
5. Access Swagger UI at `https://localhost:<port>/swagger` for interactive API testing.

## How to Test

- Use Swagger UI or tools like Postman to test endpoints.
- For automated tests, create a test project targeting .NET 8 and reference your main project.