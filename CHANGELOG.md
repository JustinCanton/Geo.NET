# Change Log

All notable changes to this project will be documented in this file. See  [Conventional Commits](https://conventionalcommits.org/)  for commit guidelines.

## [1.4.0](https://github.com/JustinCanton/Geo.NET/compare/1.3.0...1.4.0) (2023-04-08)
### Features
- **core:** removing the asp framework dependency and creating a repository specific QueryString class ([#61](https://github.com/JustinCanton/Geo.NET/issues/61)) ([c6e4688](https://github.com/JustinCanton/Geo.NET/commit/c6e46888e8b6ec4fec96bf6b0a8c0fe77f91d140))
- changing the DI methods to allow for configuration of the http client ([#66](https://github.com/JustinCanton/Geo.NET/issues/66)) ([5369e85](https://github.com/JustinCanton/Geo.NET/commit/5369e85ae2c95aff66ba23bfd444655fdb8a40d8))

## [1.3.0](https://github.com/JustinCanton/Geo.NET/compare/1.2.1...1.3.0) (2023-04-04)
### Features
- **core:** replacing the IStringLocalizer with a Geo.NET specific interface ([#50](https://github.com/JustinCanton/Geo.NET/issues/50)) ([cc43384](https://github.com/JustinCanton/Geo.NET/commit/cc43384815df870cbfb59c64ad0e9fe1e89aabf5))
- **here:** adding support for encoding flexible polylines for the autosuggest, browse, and discover endpoints ([#56](https://github.com/JustinCanton/Geo.NET/issues/56)) ([7b99804](https://github.com/JustinCanton/Geo.NET/commit/98c7dd9df3a8da4258e3c4d7482d8a0d807783e2))

## [1.2.1](https://github.com/JustinCanton/Geo.NET/compare/1.2.0...1.2.1) (2023-04-02)
### Bug Fixes
- **culture:** fixing an issue where the query string is generated incorrectly in different cultures ([#52](https://github.com/JustinCanton/Geo.NET/issues/52)) ([6421df0](https://github.com/JustinCanton/Geo.NET/commit/6421df0c4f314421718b6994b2c96d197ba955b1))

## [1.2.0](https://github.com/JustinCanton/Geo.NET/compare/1.1.1...1.2.0) (2022-08-20)
### Features
-  **core:** adding extra information and logging surrounding exceptions ([#44](https://github.com/JustinCanton/Geo.NET/pull/44)) ([7b5b154](https://github.com/JustinCanton/Geo.NET/commit/7b5b15441181bda16b0a644e2b3ef8e7b06cc074))

### Bug Fixes
- fixing an issue where the query is not properly encoded, and fixing an issue where the query string building is using an outdated method ([#46](https://github.com/JustinCanton/Geo.NET/pull/46)) ([5b55f4d](https://github.com/JustinCanton/Geo.NET/commit/5b55f4d249a617e4667e92b5cb0b2c9b6b02ec6f))

## [1.1.1](https://github.com/JustinCanton/Geo.NET/compare/1.1.0...1.1.1) (2022-07-21)
### Bug Fixes
- fixing a vulnerability in newtonsoft nuget ([#39](https://github.com/JustinCanton/Geo.NET/pull/39)) ([fa192ca](https://github.com/JustinCanton/Geo.NET/commit/fa192cab2a965503aa5a50885010836461cb822b))

## [1.1.0](https://github.com/JustinCanton/Geo.NET/compare/1.0.0...1.1.0) (2022-06-04)
### Features
- Adding score into the HERE Geocoding response ([#33](https://github.com/JustinCanton/Geo.NET/pull/33)) ([c8f42e1](https://github.com/JustinCanton/Geo.NET/commit/c8f42e1f155da17dd3869f304c3b9e36a938da71))
- Adding net6.0 support ([#34](https://github.com/JustinCanton/Geo.NET/pull/34)) ([e8eebca](https://github.com/JustinCanton/Geo.NET/commit/e8eebca37d82e3659e7c5e6e2ea4f4777f45f4f7))

## 1.0.0 (2021-01-10)
### Features
- Adding support for ArcGIS Suggest API
- Adding support for ArcGIS Address Candidate API
- Adding support for ArcGIS Place Candidate API
- Adding support for ArcGIS Geocoding API
- Adding support for ArcGIS Reverse Geocoding API
- Adding support for Bing Geocoding API
- Adding support for Bing Reverse Geocoding API
- Adding support for Bing By Address API
- Adding support for Google Geocoding API
- Adding support for Google Reverse Geocoding API
- Adding support for Google Places Search API
- Adding support for Google Places Details API
- Adding support for Google Autocomplete Places API
- Adding support for Google Autocomplete Query API
- Adding support for HERE Geocoding API
- Adding support for HERE Reverse Geocoding API
- Adding support for HERE Discover API
- Adding support for HERE Auto Suggest API
- Adding support for HERE Browse API
- Adding support for HERE Lookup API
- Adding support for MapBox Geocoding API
- Adding support for MapBox Reverse Geocoding API
- Adding support for MapQuest Geocoding Open API
- Adding support for MapQuest Reverse Geocoding Open API
- Adding support for MapQuest Geocoding Lisenced API
- Adding support for MapQuest Reverse Geocoding Lisenced API