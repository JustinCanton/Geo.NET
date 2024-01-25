# Bing Geocoding

This allows the simple calling of Bing geocoding APIs. The supported Bing geocoding endpoints are:
- [Geocoding](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-query)
- [Reverse Geocoding](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-point)
- [By Address](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-address)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the Bing service:
```
using Geo.DependencyInjection;
.
.
.
public void ConfigureServices(IServiceCollection services)
{
    .
    .
    .
    var builder = services.AddBingGeocoding();
    builder.AddKey(your_bing_api_key_here);
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

## Sample Usage

By calling `AddBingGeocoding`, the `IBingGeocoding ` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IBingGeocoding bingGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.