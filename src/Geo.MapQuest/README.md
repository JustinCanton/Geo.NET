# MapQuest Geocoding

This allows the simple calling of MapQuest geocoding APIs. The supported MapQuest geocoding endpoints are:
- Open API
	- [Geocoding](https://developer.mapquest.com/documentation/open/geocoding-api/)
	- [Reverse Geocoding](https://developer.mapquest.com/documentation/open/geocoding-api/)
- Licensed API
	- [Geocoding](https://developer.mapquest.com/documentation/geocoding-api/address/get/)
	- [Reverse Geocoding](https://developer.mapquest.com/documentation/geocoding-api/reverse/get/)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the MapQuest service:
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
    var builder = services.AddMapQuestGeocoding();
    builder.AddKey(your_mapquest_api_key_here);
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

MapQuest has 2 endpoint types, open and licensed. They are not able to be used together. For more information, refer to the MapQuest [Terms of Service](https://developer.mapquest.com/legal). To specify whether to use the licensed endpoint or not, call the options method `UseLicensedEndpoints`. The default endpoint that is used is the open endpoint.
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
    var builder = services.AddMapQuestGeocoding();
    builder.AddKey(your_mapquest_api_key_here);
    builder.UseLicensedEndpoints();
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

## Sample Usage

By calling `AddMapQuestGeocoding`, the `IMapQuestGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IMapQuestGeocoding mapQuestGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.