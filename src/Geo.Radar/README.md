# Radar Geocoding

This allows the simple calling of Radar geocoding APIs. The supported Radar geocoding endpoints are:
- [Geocoding](https://api.radar.io/v1/geocode/forward)
- [Reverse Geocoding](https://api.radar.io/v1/geocode/reverse)
- [Autocomplete](https://api.radar.io/v1/search/autocomplete)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the Radar service:
```
using Geo.Extensions.DependencyInjection;
.
.
.
public void ConfigureServices(IServiceCollection services)
{
    .
    .
    .
    var builder = services.AddRadarGeocoding();
    builder.AddKey(your_Radar_api_key_here);
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

## Sample Usage

By calling `AddRadarGeocoding`, the `IRadarGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IRadarGeocoding RadarGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.