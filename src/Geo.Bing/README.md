# Bing Geocoding

This allows the simple calling of Bing Geocoding API's. The supported Bing geocoding endpoints are:
- [Geocoding](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-query)
- [Reverse Geocoding](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-point)
- [By Address](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-address)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the Bing service:
```
using Geo.Bing.DependencyInjection;
.
.
.
public void ConfigureServices(IServiceCollection services)
{
    .
    .
    .
    services.AddBingServices(options => options.UseKey(your_bing_api_key_here));
    .
    .
    .
}
```

## Sample Usage

By calling `AddBingServices`, the `IBingGeocoding ` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IBingGeocoding bingGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.