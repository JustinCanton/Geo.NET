# MapBox Geocoding

This allows the simple calling of MapBox Geocoding API's. The supported MapBox geocoding endpoints are:
 - [x] [Geocoding](https://docs.mapbox.com/api/search/#forward-geocoding)
 - [x] [Reverse Geocoding](https://docs.mapbox.com/api/search/#reverse-geocoding)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the MapBox service:
```
using Geo.MapBox.DependencyInjection;
.
.
.
public void ConfigureServices(IServiceCollection services)
{
    .
    .
    .
    services.AddMapBoxServices(options => options.UseKey(your_here_api_key_here));
    .
    .
    .
}
```

## Sample Usage

By calling `AddMapBoxServices`, the `IMapBoxGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IMapBoxGeocoding mapBoxGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.

## Endpoint Types
MapQuest has 2 endpoint types, permanent and non-permanent.

 - Requests to the non-permanent endpoint must be triggered by user activity. Any results must be displayed on a Mapbox map and cannot be stored permanently, as described in Mapbox�s [terms of service](https://www.mapbox.com/tos/#geocoding).
 - The permanent endpoint gives you access to two services: permanent geocoding and batch geocoding.