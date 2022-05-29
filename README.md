# Geo.NET

This is a simple, light-weight solution for interfacing with multiple geocoding APIs.

The support for this project includes:

 - ArcGIS
	 - [Suggest](https://developers.arcgis.com/rest/geocode/api-reference/geocoding-suggest.htm)
	 - [Address Candidate](https://developers.arcgis.com/labs/rest/search-for-an-address/)
	 - [Place Candidate](https://developers.arcgis.com/labs/rest/find-places/)
	 - [Geocoding](https://developers.arcgis.com/rest/geocode/api-reference/geocoding-geocode-addresses.htm)
	 - [Reverse Geocoding](https://developers.arcgis.com/rest/geocode/api-reference/geocoding-reverse-geocode.htm)
 - Bing
	 - [Geocoding](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-query)
	 - [Reverse Geocoding](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-point)
	 - [By Address](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-address)
 - Google
	 - [Geocoding](https://developers.google.com/maps/documentation/geocoding/start)
	 - [Reverse Geocoding](https://developers.google.com/maps/documentation/geocoding/start)
	 - Places ([Search](https://developers.google.com/places/web-service/search) and [Details](https://developers.google.com/places/web-service/details))
	 - Autocomplete([Places](https://developers.google.com/places/web-service/autocomplete) and [Query](https://developers.google.com/places/web-service/query))
 - HERE
	 - [Geocoding](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-geocode-brief.html)
	 - [Reverse Geocoding](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-reverse-geocode-brief.html)
	 - [Discover](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-discover-brief.html)
	 - [Auto Suggest](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-autosuggest-brief.html)
	 - [Browse](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-browse-brief.html)
	 - [Lookup](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-lookup-brief.html)
 - MapBox
	 - [Geocoding](https://docs.mapbox.com/api/search/#forward-geocoding)
	 - [Reverse Geocoding](https://docs.mapbox.com/api/search/#reverse-geocoding)
 - MapQuest
	 - Open API
		 - [Geocoding](https://developer.mapquest.com/documentation/open/geocoding-api/)
		 - [Reverse Geocoding](https://developer.mapquest.com/documentation/open/geocoding-api/)
	 - Lisenced API
		 - [Geocoding](https://developer.mapquest.com/documentation/geocoding-api/address/get/)
		 - [Reverse Geocoding](https://developer.mapquest.com/documentation/geocoding-api/reverse/get/)


## Configuration and Sample Usage
The configuration and sample usage for each supported interface can be found within each project.

 - [ArcGIS](https://github.com/JustinCanton/Geo.NET/tree/master/src/Geo.ArcGIS)
 - [Bing](https://github.com/JustinCanton/Geo.NET/tree/master/src/Geo.Bing)
 - [Google](https://github.com/JustinCanton/Geo.NET/tree/master/src/Geo.Google)
 - [HERE](https://github.com/JustinCanton/Geo.NET/tree/master/src/Geo.Here)
 - [MapBox](https://github.com/JustinCanton/Geo.NET/tree/master/src/Geo.MapBox)
 - [MapQuest](https://github.com/JustinCanton/Geo.NET/tree/master/src/Geo.MapQuest)


## Roadmap

### 1.0.0
|Status|Goal|
|:--:|--|
|✅|Adding support for ArcGIS Suggest API|
|✅|Adding support for ArcGIS Address Candidate API|
|✅|Adding support for ArcGIS Place Candidate API|
|✅|Adding support for ArcGIS Geocoding API|
|✅|Adding support for ArcGIS Reverse Geocoding API|
|✅|Adding support for Bing Geocoding API|
|✅|Adding support for Bing Reverse Geocoding API|
|✅|Adding support for Bing By Address API|
|✅|Adding support for Google Geocoding API|
|✅|Adding support for Google Reverse Geocoding API|
|✅|Adding support for Google Places Search API|
|✅|Adding support for Google Places Details API|
|✅|Adding support for Google Autocomplete Places API|
|✅|Adding support for Google Autocomplete Query API|
|✅|Adding support for HERE Geocoding API|
|✅|Adding support for HERE Reverse Geocoding API|
|✅|Adding support for HERE Discover API|
|✅|Adding support for HERE Auto Suggest API|
|✅|Adding support for HERE Browse API|
|✅|Adding support for HERE Lookup API|
|✅|Adding support for MapBox Geocoding API|
|✅|Adding support for MapBox Reverse Geocoding API|
|✅|Adding support for MapQuest Geocoding Open API|
|✅|Adding support for MapQuest Reverse Geocoding Open API|
|✅|Adding support for MapQuest Geocoding Lisenced API|
|✅|Adding support for MapQuest Reverse Geocoding Lisenced API|


### 1.1.0
|Status|Goal|
|:--:|--|
|✅|Adding score into the HERE Geocoding response|
|✅|Adding net6.0 support|
|❌|Adding ChangeLog|


### 1.2.0
|Status|Goal|
|:--:|--|
|❌|Adding support for conversion from coordinates to flexible polylines rather than requiring polylines as input for the HERE API|


### Suggestions or Discussion Points
If you would like to weigh in on any suggestion, please make a Github Issue and we can discuss further.

✅ = Accepted
❌ = Rejected
❓ = Still discussing

|Decision|Suggestion|
|:--:|--|
|❓|Adding support for routing algorithms (This may spawn a new repository rather than being placed in this repository)|


## Get Started and How To Contribute

Use a form of [Visual Studio](https://www.visualstudio.com/)  to work with the project for a seamless experience.

Pull the project, and open the Geo.NET.sln file to build this library.

Some of the best ways to contribute are to try things out, file issues, join in design conversations, and make pull-requests.
