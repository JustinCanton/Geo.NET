# MapQuest Geocoding

This allows the simple calling of MapQuest Geocoding API's. The supported MapQuest geocoding endpoints are:
- Open API
	- [Geocoding](https://developer.mapquest.com/documentation/open/geocoding-api/)
	- [Reverse Geocoding](https://developer.mapquest.com/documentation/open/geocoding-api/)
- Licensed  Api
	- [Geocoding](https://developer.mapquest.com/documentation/geocoding-api/address/get/)
	- [Reverse Geocoding](https://developer.mapquest.com/documentation/geocoding-api/reverse/get/)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the MapQuest service:
```
using Geo.MapQuest.DependencyInjection;
.
.
.
public void ConfigureServices(IServiceCollection services)
{
    .
    .
    .
    services.AddMapQuestServices(options => options.UseKey(your_here_api_key_here));
    .
    .
    .
}
```

MapQuest has 2 endpoint types, open and licensed. They are not able to be used together. For more information, refer to the MapQuest [Terms of Service](https://developer.mapquest.com/legal). To specify whether to use the licensed endpoint or not, call the options method `UseLicensedEndpoints`. The default endpoint that is used is the open endpoint.
```
using Geo.MapQuest.DependencyInjection;
.
.
.
public void ConfigureServices(IServiceCollection services)
{
    .
    .
    .
    services.AddMapQuestServices(options => options.UseKey(your_here_api_key_here).UseLicensedEndpoints());
    .
    .
    .
}
```

## Sample Usage

By calling `AddMapQuestServices`, the `IMapQuestGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IMapQuestGeocoding mapQuestGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.