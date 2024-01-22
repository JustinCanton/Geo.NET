# Change Log

All notable changes to this project will be documented in this file. See  [Conventional Commits](https://conventionalcommits.org/)  for commit guidelines.

## [2.0.0](https://github.com/JustinCanton/Geo.NET/compare/1.6.0...2.0.0) (2024-01-30)
### ⚠ BREAKING CHANGES
- removed native support for net5.0 since it is an out of support item, and dropped netstandard2.1 since this supports netstandard2.0
- removed the usage of Newtonsoft.Json and moved to use System.Text.Json
- changed the exceptions returned from all services to GeoNETException instead of individual exceptions per API, as well as removed some now-deprecated interface types and implementations

### Features
- **runtime**: updating the .net version support for net8.0, and removing native support for netstandard2.1 and net5.0 ([#58](https://github.com/JustinCanton/Geo.NET/issues/58)) ([4398a10](https://github.com/JustinCanton/Geo.NET/commit/4398a10afb21d3e7e86fba0fa4052adb67ca1faa))
- **serialization**: updating to use System.Text.Json instead of Newtonsoft.Json ([#40](https://github.com/JustinCanton/Geo.NET/issues/40)) ([e108ec6](https://github.com/JustinCanton/Geo.NET/commit/e108ec65d895d8d7fa792845973f14cdcc7335ae))
- **exceptions**: updating how exceptions are handled and removing unused interfaces ([#95](https://github.com/JustinCanton/Geo.NET/issues/95)) ([e108ec6](https://github.com/JustinCanton/Geo.NET/commit/e108ec65d895d8d7fa792845973f14cdcc7335ae))

## [1.6.0](https://github.com/JustinCanton/Geo.NET/compare/1.5.2...1.6.0) (2023-12-29)
### Features
- **arcgis**: updating the parameter objects to include new parameters for endpoints ([#88](https://github.com/JustinCanton/Geo.NET/issues/88)) ([0c9028a](https://github.com/JustinCanton/Geo.NET/commit/0c9028a76ecc4695f105062393203aae5a43eeff))

## [1.5.2](https://github.com/JustinCanton/Geo.NET/compare/1.5.1...1.5.2) (2023-09-18)
### Bug Fixes
- **here**:  fixing an issue where the in parameter of the geocoding endpoint is not passed correctly ([#87](https://github.com/JustinCanton/Geo.NET/issues/87)) ([7f2adf0](https://github.com/JustinCanton/Geo.NET/commit/7f2adf0383c85bb8d79cf6b321d125bef9a4c7f8))


## [1.5.1](https://github.com/JustinCanton/Geo.NET/compare/1.5.0...1.5.1) (2023-09-01)
### Features
- **mapbox**: adding the new worldview parameter to the forward and reverse geocode parameters ([#77](https://github.com/JustinCanton/Geo.NET/issues/77)) ([cfc987c](https://github.com/JustinCanton/Geo.NET/commit/cfc987cc1f7db2e5d0a7e3981f3a0c4325d2211b))
- **here**: adding new parameters to the different geocoding requests ([#78](https://github.com/JustinCanton/Geo.NET/issues/78)) ([fd96319](https://github.com/JustinCanton/Geo.NET/commit/fd9631956001379b67c2f93f7d1a462157b7007c))
- **bing**: adding a missing culture parameter for Bing ([#82](https://github.com/JustinCanton/Geo.NET/issues/82)) ([2fc79ac](https://github.com/JustinCanton/Geo.NET/commit/2fc79ac6c9b7fe7fd5350c942e048f4813bcb52c))

### Bug Fixes
- **here**: fixing an issue where using an invariant culture leads to an empty language in the query string ([#83](https://github.com/JustinCanton/Geo.NET/issues/83)) ([176aca9](https://github.com/JustinCanton/Geo.NET/commit/176aca900dafc47eafc3434cc7a50c414738002a))


## [1.5.0](https://github.com/JustinCanton/Geo.NET/compare/1.4.0...1.5.0) (2023-06-04)
### Features
- **here**: Adding missing fields on Address model type for the Here Geocoding API ([e121354](https://github.com/JustinCanton/Geo.NET/commit/e121354c204eabd6bd63cbd651cf9b635108f498))
- **framework**: adding support for netstandard2.0 ([#73](https://github.com/JustinCanton/Geo.NET/issues/73)) ([23e3569](https://github.com/JustinCanton/Geo.NET/commit/23e35698ecfad4ae22b02c851e5c7657d0356937))

### Bug Fixes
- **core**: fixing an issue where too many requests to create the same resource provider causes an exception in the dictionary ([#70](https://github.com/JustinCanton/Geo.NET/issues/70)) ([6f47d9a](https://github.com/JustinCanton/Geo.NET/commit/6f47d9a277ba8db6e5d9e33edd642d069e23b456))
- **here**: Adding missing enum values on ResultType ([e7ed451](https://github.com/JustinCanton/Geo.NET/commit/e7ed4516a8aff0e6cc1cc69f8555ca993b86901e))


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