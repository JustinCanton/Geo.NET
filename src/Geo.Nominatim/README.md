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
    builder.AddServer(your_nominatim_server_here);
    builder.AddUserAgent(your_application_identifier_here);
    builder.AddEmail(your_nominatim_email_here);
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

### Choosing a Nominatim instance

Nominatim is open source, so it can be called through the public instance run by the OpenStreetMap Foundation,
through another publicly hosted instance, or through your own deployment. Use `AddServer` to pick which one:

```
// The public OpenStreetMap Foundation instance. This is the default and does not need to be set.
builder.AddServer("https://nominatim.openstreetmap.org");

// Another publicly hosted instance.
builder.AddServer("https://nominatim.qgis.org");

// A self hosted deployment, including one served under a path.
builder.AddServer("https://my-server.example.com/nominatim");
```

When `AddServer` is not called, the public OpenStreetMap Foundation instance is used.

### Usage policy of the public instance

The [usage policy](https://operations.osmfoundation.org/policies/nominatim/) of the public instance requires that
requests identify the calling application, and it rejects the stock user agents set by http libraries. Use
`AddUserAgent` to set one, and `AddEmail` to provide a contact address. The policy also limits calls to the public
instance to at most one request per second. Neither is needed when calling a self hosted instance.

## Sample Usage

By calling `AddNominatimGeocoding`, the `INominatimGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(INominatimGeocoding nominatimGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.
