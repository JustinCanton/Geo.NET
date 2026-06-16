# Geo.OpenRouteService

A .NET library for communicating with the [OpenRouteService Geocoding API](https://openrouteservice.org/dev/#/api-docs/geocode).

## Supported Endpoints

- **Search** (`/geocode/search`) — Forward geocoding: convert a text query to coordinates.
- **Autocomplete** (`/geocode/autocomplete`) — Type-ahead suggestions for real-time search.
- **Structured Search** (`/geocode/search/structured`) — Forward geocoding using individual address components.
- **Reverse** (`/geocode/reverse`) — Reverse geocoding: convert coordinates to an address.

## Usage

Register the service with dependency injection:

```csharp
services.AddOpenRouteServiceGeocoding()
    .AddKey("YOUR_API_KEY");
```

Inject and call the service:

```csharp
public class MyService
{
    private readonly IOpenRouteServiceGeocoding _geocoding;

    public MyService(IOpenRouteServiceGeocoding geocoding)
    {
        _geocoding = geocoding;
    }

    public async Task<FeatureCollection> SearchAsync(string query)
    {
        return await _geocoding.SearchAsync(new SearchParameters { Text = query });
    }
}
```

## Authentication

Obtain an API key from [openrouteservice.org](https://openrouteservice.org) and pass it via `.AddKey()` during registration, or override it per-request by setting `Key` on the parameters object.
