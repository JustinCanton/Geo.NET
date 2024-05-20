# Positionstack Geocoding

This allows the simple calling of Positionstack geocoding APIs. The supported Positionstack geocoding endpoints are:
- [Geocoding](https://api.positionstack.com/v1/forward)
- [Reverse Geocoding](https://api.positionstack.com/v1/reverse)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the Positionstack service:
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
    var builder = services.AddPositionstackGeocoding();
    builder.AddKey(your_Positionstack_api_key_here);
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

## Sample Usage

By calling `AddPositionstackGeocoding`, the `IPositionstackGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IPositionstackGeocoding PositionstackGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.