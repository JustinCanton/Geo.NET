# Trimble Maps Geocoding

This allows the simple calling of Trimble Maps geocoding APIs. The supported Trimble Maps geocoding endpoints are:
- [Geocoding](https://pcmiler.alk.com/apis/rest/v1.0/service.svc/locations)
- [Reverse Geocoding](https://pcmiler.alk.com/apis/rest/v1.0/service.svc/locations/reverse)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the Trimble Maps service:
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
    var builder = services.AddTrimbleGeocoding();
    builder.AddKey(your_Trimble_Maps_api_key_here);
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

## Sample Usage

By calling `AddTrimbleGeocoding`, the `ITrimbleGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(ITrimbleGeocoding trimbleGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.
