# Ola Geocoding

This allows the simple calling of Radar geocoding APIs. The supported Radar geocoding endpoints are:
- [Geocoding](https://api.olamaps.io/places/v1/geocode)
- [Reverse Geocoding](https://api.olamaps.io/places/v1/reverse-geocode)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the Ola service:
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
    var builder = services.AddOlaGeocoding();
    builder.AddKey(your_Ola_api_key_here);
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

## Sample Usage

By calling `AddOlaGeocoding`, the `IOlaGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IOlaGeocoding OlaGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.