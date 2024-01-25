# Google Geocoding

This allows the simple calling of Google geocoding APIs. The supported Google geocoding endpoints are:
 - [Geocoding](https://developers.google.com/maps/documentation/geocoding/start)
 - [Reverse Geocoding](https://developers.google.com/maps/documentation/geocoding/start)
 - Places ([Search](https://developers.google.com/places/web-service/search) and [Details](https://developers.google.com/places/web-service/details))
 - Autocomplete([Places](https://developers.google.com/places/web-service/autocomplete) and [Query](https://developers.google.com/places/web-service/query))

## Configuration

In the startup `ConfigureServices` method, add the configuration for the Google service:
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
    var builder = services.AddGoogleGeocoding();
    builder.AddKey(your_google_api_key_here);
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

## Sample Usage

By calling `AddGoogleGeocoding`, the `IGoogleGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IGoogleGeocoding googleGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.