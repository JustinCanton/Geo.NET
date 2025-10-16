# Nominatim Geocoding

This allows the simple calling of Nominatim geocoding APIs. The supported Nominatim geocoding endpoints are:
- [Search](https://nominatim.org/release-docs/latest/api/Search/)
- [Reverse Geocoding](https://nominatim.org/release-docs/latest/api/Reverse/)
- [Lookup](https://nominatim.org/release-docs/latest/api/Lookup/)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the Nominatim service:
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
    var builder = services.AddNominatimGeocoding();
    builder.AddEmail(your_nominatim_email_here);
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

## Sample Usage

By calling `AddNominatimGeocoding`, the `INominatimGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(INominatimGeocoding nominatimGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.
