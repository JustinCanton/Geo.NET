# MapQuest Geocoding

This allows the simple calling of MapQuest Geocoding API's. The supported MapQuest geocoding endpoints are:
- [x] Open API
	- [x] [Geocoding](https://developer.mapquest.com/documentation/open/geocoding-api/)
	 - [x] [Reverse Geocoding](https://developer.mapquest.com/documentation/open/geocoding-api/)
- [x] Lisenced Api
	- [x] [Geocoding](https://developer.mapquest.com/documentation/geocoding-api/address/get/)
	- [x] [Reverse Geocoding](https://developer.mapquest.com/documentation/geocoding-api/reverse/get/)

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

MapQuest has 2 endpoint types, open and lisenced. They are not able to be used together. For more information, refer to the MapQuest [Terms of Service](https://developer.mapquest.com/legal). To specify whether to use the lisenced endpoint or not, call the options method `UseLicensedEndpoints`. The default endpoint that is used is the open endpoint.
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