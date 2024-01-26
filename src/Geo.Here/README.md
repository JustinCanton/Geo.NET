# HERE Geocoding

This allows the simple calling of HERE geocoding APIs. The supported HERE geocoding endpoints are:
- [Geocoding](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-geocode-brief.html)
- [Reverse Geocoding](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-reverse-geocode-brief.html)
- [Discover](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-discover-brief.html)
- [Auto Suggest](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-autosuggest-brief.html)
- [Browse](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-browse-brief.html)
- [Lookup](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-lookup-brief.html)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the HERE service:
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
    var builder = services.AddHereGeocoding();
    builder.AddKey(your_here_api_key_here);
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

## Sample Usage

By calling `AddHereGeocoding`, the `IHereGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IHereGeocoding hereGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.