# ArcGIS Geocoding

This allows the simple calling of ArcGIS geocoding APIs. The supported ArcGIS geocoding endpoints are:
- [Suggest](https://developers.arcgis.com/rest/geocode/api-reference/geocoding-suggest.htm)
- [Address Candidate](https://developers.arcgis.com/labs/rest/search-for-an-address/)
- [Place Candidate](https://developers.arcgis.com/labs/rest/find-places/)
- [Geocoding](https://developers.arcgis.com/rest/geocode/api-reference/geocoding-geocode-addresses.htm)
- [Reverse Geocoding](https://developers.arcgis.com/rest/geocode/api-reference/geocoding-reverse-geocode.htm)

## Configuration

In the startup `ConfigureServices` method, add the configuration for the ArcGIS service:
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
    var builder = services.AddArcGISGeocoding();
    builder.AddClientCredentials(your_arcgis_client_id_here, your_arcgis_client_secret_here);
    builder.HttpClientBuilder.ConfigureHttpClient(configure_client);
    .
    .
    .
}
```

### Client Id and Secret

Not all ArcGIS geocoding endpoints require a client id and secret. The endpoints that require the id and secret are:

 - Geocoding
 - Reverse Geocoding

The endpoints where it is optional based on the request type:

 - Address Candidate
 - Place Candidate

The endpoints where it is not required at all:

 - Suggest

## Sample Usage

By calling `AddArcGISGeocoding`, the `IArcGISGeocoding` interface has been added to the IOC container. Just request it as a DI item:
```
public MyService(IArcGISGeocoding arcgisGeocoding)
{
    ...
}
```

Now simply call the geocoding methods in the interface.

### Storing results

For some ArcGIS endpoints, it is required to specify whether or not the information is being stored. The parameters have a property called `ForStorage`. If the result of the request to ArcGIS is being stored, this **MUST** be set to true. As per the ArcGIS documentation:

> Specifies whether the results of the operation will be persisted. The default value is  false, which indicates the results of the operation can't be stored, but they can be temporarily displayed on a map for instance. If you store the results, in a database, for example, you need to set this parameter to  true.
> 
> Applications are contractually prohibited from storing the results of geocoding transactions unless they make the request by passing the  forStorage  parameter with a value of  true  and the  token  parameter with a valid ArcGIS Online token. Instructions for composing a request with a valid token are provided in the  [authentication topic](https://developers.arcgis.com/rest/geocode/api-reference/geocoding-authenticate-a-request.htm).

This library/product **does not** take any responsibility for misusing the `ForStorage` flag.
