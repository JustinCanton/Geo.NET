# Geo.NET

This is a simple, light-weight solution for interfacing with multiple geocoding APIs.

The planned support for this project includes:

 - Google
	 - [x] [Geocoding](https://developers.google.com/maps/documentation/geocoding/start)
	 - [x] [Reverse Geocoding](https://developers.google.com/maps/documentation/geocoding/start)
	 - [ ] Places ([Search](https://developers.google.com/places/web-service/search) and [Details](https://developers.google.com/places/web-service/details))
	 - [ ] [Autocomplete](https://developers.google.com/places/web-service/query)
 - Bing
	 - [x] [Geocoding](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-query)
	 - [x] [Reverse Geocoding](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-point)
	 - [x] [By Address](https://docs.microsoft.com/en-us/bingmaps/rest-services/locations/find-a-location-by-address)
 - ArcGIS
	 - [x] [Suggest](https://developers.arcgis.com/rest/geocode/api-reference/geocoding-suggest.htm)
	 - [x] [Address Candidate](https://developers.arcgis.com/labs/rest/search-for-an-address/)
	 - [x] [Place Candidate](https://developers.arcgis.com/labs/rest/find-places/)
	 - [x] [Geocoding](https://developers.arcgis.com/rest/geocode/api-reference/geocoding-geocode-addresses.htm)
	 - [x] [Reverse Geocoding](https://developers.arcgis.com/rest/geocode/api-reference/geocoding-reverse-geocode.htm)
 - here
	 - [ ] [Geocoding](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-geocode-brief.html)
	 - [ ] [Reverse Geocoding](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-reverse-geocode-brief.html)
	 - [ ] [Discover](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-discover-brief.html)
	 - [ ] [Auto Suggest](https://developer.here.com/documentation/geocoding-search-api/dev_guide/topics/endpoint-autosuggest-brief.html)
 - MapQuest
	 - [ ] Open API
		 - [ ] [Geocoding](https://developer.mapquest.com/documentation/open/geocoding-api/)
		 - [ ] [Reverse Geocoding](https://developer.mapquest.com/documentation/open/geocoding-api/)
	 - [ ] Api
		 - [ ] [Geocoding](https://developer.mapquest.com/documentation/geocoding-api/address/get/)
		 - [ ] [Reverse Geocoding](https://developer.mapquest.com/documentation/geocoding-api/reverse/get/)
 - MapBox
	 - [ ] [Geocoding](https://docs.mapbox.com/api/search/#forward-geocoding)
	 - [ ] [Reverse Geocoding](https://docs.mapbox.com/api/search/#reverse-geocoding)

## Get Started

Use a form of [Visual Studio](https://www.visualstudio.com/)  to work with the project for a seamless experience.

Pull the project, and open the Geo.NET.sln file to build this library.

Some of the best ways to contribute are to try things out, file issues, join in design conversations, and make pull-requests.

## Configuration and Sample Usage
The configuration and sample usage for each supported interface can be found within each project.

 - [Google](https://github.com/JustinCanton/Geo.NET/src/Geo.Google)
 - [Bing](https://github.com/JustinCanton/Geo.NET/src/Geo.Bing)
 - [ArcGIS](https://github.com/JustinCanton/Geo.NET/src/Geo.ArcGIS)
 - here
 - MapQuest
 - MapBox