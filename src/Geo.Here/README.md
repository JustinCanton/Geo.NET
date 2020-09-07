# here Geocoding

This allows the simple calling of here Geocoding API's. The supported here geocoding endpoints are:
- [x] [Geocoding](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-geocode-brief.html)
- [x] [Reverse Geocoding](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-reverse-geocode-brief.html)
- [ ] [Discover](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-discover-brief.html)
- [ ] [Auto Suggest](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-autosuggest-brief.html)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the here service:
```
using Geo.Here.DependencyInjection;
.
.
.
public void ConfigureServices(IServiceCollection services)
{
    .
    .
    .
    services.AddHereServices(options => options.UseKey(your_here_api_key_here));
    .
    .
    .
}
```

## Sample Usage

By calling `AddHereServices`, the `IHereGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IHereGeocoding hereGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.