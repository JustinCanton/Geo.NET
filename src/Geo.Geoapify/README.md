# Geo.Geoapify

A .NET library for communicating with the [Geoapify Geocoding API](https://apidocs.geoapify.com/docs/geocoding/).

## Supported Endpoints

- **Geocoding** (`/v1/geocode/search`) — Forward geocoding: convert a free form or structured address to coordinates.
- **Reverse Geocoding** (`/v1/geocode/reverse`) — Convert coordinates to an address.
- **Autocomplete** (`/v1/geocode/autocomplete`) — Type-ahead address suggestions for real-time search.

All endpoints are called with the default `geojson` response format, and are returned as a `FeatureCollection`.

## Usage

Register the service with dependency injection:

```csharp
services.AddGeoapifyGeocoding()
    .AddKey("YOUR_API_KEY");
```

Inject and call the service:

```csharp
public class MyService
{
    private readonly IGeoapifyGeocoding _geocoding;

    public MyService(IGeoapifyGeocoding geocoding)
    {
        _geocoding = geocoding;
    }

    public async Task<FeatureCollection> GeocodeAsync(string address)
    {
        return await _geocoding.GeocodingAsync(new GeocodingParameters { Text = address });
    }
}
```

Forward geocoding accepts either a free form `Text`, or any combination of the structured
`Name`, `HouseNumber`, `Street`, `PostCode`, `City`, `State`, and `Country` components:

```csharp
var result = await _geocoding.GeocodingAsync(new GeocodingParameters
{
    HouseNumber = "38",
    Street = "Upper Montagu Street",
    PostCode = "W1H 1LJ",
    City = "London",
    Country = "United Kingdom",
});
```

Reverse geocoding takes a coordinate:

```csharp
var result = await _geocoding.ReverseGeocodingAsync(new ReverseGeocodingParameters
{
    Coordinate = new Coordinate { Latitude = 51.21709661403662, Longitude = 6.7189169 },
});
```

### Filters and Biases

Geoapify supports restricting results to an area (`filter`) and preferring results from an area (`bias`).
Multiple filters are combined with AND logic, and multiple biases with OR logic.

```csharp
var parameters = new AutocompleteParameters { Text = "Mosco" };

parameters.Filters.Add(new CircleFilter
{
    Centre = new Coordinate { Latitude = 41.878968, Longitude = -87.770231 },
    Radius = 5000,
});

parameters.Biases.Add(new ProximityBias
{
    Coordinate = new Coordinate { Latitude = 41.878968, Longitude = -87.770231 },
});
```

The available filters are `CircleFilter`, `RectangleFilter`, `CountryCodeFilter`, `PlaceFilter`, and `GeometryFilter`.
The available biases are `ProximityBias`, `CircleBias`, `RectangleBias`, and `CountryCodeBias`.

## Authentication

Obtain an API key from [geoapify.com](https://www.geoapify.com) and pass it via `.AddKey()` during registration,
or override it per-request by setting `Key` on the parameters object.
