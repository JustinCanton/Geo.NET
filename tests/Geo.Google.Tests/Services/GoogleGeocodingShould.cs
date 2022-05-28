﻿// <copyright file="GoogleGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using FluentAssertions;
    using Geo.Core;
    using Geo.Core.Extensions;
    using Geo.Google.Enums;
    using Geo.Google.Models;
    using Geo.Google.Models.Parameters;
    using Geo.Google.Services;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="GoogleGeocoding"/> class.
    /// </summary>
    public class GoogleGeocodingShould : IDisposable
    {
        private readonly Mock<HttpMessageHandler> _mockHandler;
        private readonly GoogleKeyContainer _keyContainer;
        private readonly IStringLocalizer<GoogleGeocoding> _localizer;
        private readonly IStringLocalizer<ClientExecutor> _coreLocalizer;
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleGeocodingShould"/> class.
        /// </summary>
        public GoogleGeocodingShould()
        {
            _keyContainer = new GoogleKeyContainer("abc123");

            _mockHandler = new Mock<HttpMessageHandler>();

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'results':[" +
                    "{'address_components':[{'long_name':'1600','short_name':'1600','types':['street_number']}," +
                    "{'long_name':'Amphitheatre Parkway','short_name':'Amphitheatre Pkwy','types':['route']},{'long_name':'Mountain View','short_name':'Mountain View','types':['locality','political']}," +
                    "{'long_name':'Santa Clara County','short_name':'Santa Clara County','types':['administrative_area_level_2','political']}," +
                    "{'long_name':'California','short_name':'CA','types':['administrative_area_level_1','political']},{'long_name':'United States','short_name':'US','types':['country','political']}," +
                    "{'long_name':'94043','short_name':'94043','types':['postal_code']}],'formatted_address':'1600 Amphitheatre Pkwy, Mountain View, CA 94043, USA'," +
                    "'geometry':{'location':{'lat':37.4220578,'lng':-122.0840897},'location_type':'ROOFTOP'," +
                    "'viewport':{'northeast':{'lat':37.4234067802915,'lng':-122.0827407197085},'southwest':{'lat':37.4207088197085,'lng':-122.0854386802915}}}," +
                    "'place_id':'ChIJtYuu0V25j4ARwu5e4wwRYgE','plus_code':{'compound_code':'CWC8+R9 Mountain View, CA, USA','global_code':'849VCWC8+R9'},'types':['street_address']}," +
                    "{'address_components':[{'long_name':'1600','short_name':'1600','types':['street_number']}," +
                    "{'long_name':'Amphitheatre Parkway','short_name':'Amphitheatre Parkway','types':['route']},{'long_name':'Mountain View','short_name':'Mountain View','types':['locality','political']}," +
                    "{'long_name':'Santa Clara County','short_name':'Santa Clara County','types':['administrative_area_level_2','political']}," +
                    "{'long_name':'California','short_name':'CA','types':['administrative_area_level_1','political']},{'long_name':'United States','short_name':'US','types':['country','political']}," +
                    "{'long_name':'94043','short_name':'94043','types':['postal_code']}],'formatted_address':'1600 Amphitheatre Parkway, Mountain View, CA 94043, USA'," +
                    "'geometry':{'location':{'lat':37.4121802,'lng':-122.0905099},'location_type':'ROOFTOP'," +
                    "'viewport':{'northeast':{'lat':37.4135291802915,'lng':-122.0891609197085},'southwest':{'lat':37.41083121970851,'lng':-122.0918588802915}}}," +
                    "'place_id':'ChIJVYBZP-Oxj4ARls-qJ_G3tgM','plus_code':{'compound_code':'CW65+VQ Mountain View, CA, USA','global_code':'849VCW65+VQ'},'types':['street_address']}]," +
                    "'status':'OK'}"),
            });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("maps/api/geocode/json?address")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'results':[" +
                    "{'formattedAddress':'111 8th Ave, New York, NY 10011, USA','addressComponents':[{'longName':'111','shortName':'111','types':[34]}," +
                    "{'longName':'8th Avenue','shortName':'8th Ave','types':[2]},{'longName':'Manhattan','shortName':'Manhattan','types':[4,13,14]}," +
                    "{'longName':'New York','shortName':'New York','types':[12,4]},{'longName':'New York County','shortName':'New York County','types':[7,4]}," +
                    "{'longName':'New York','shortName':'NY','types':[6,4]},{'longName':'United States','shortName':'US','types':[5,4]}," +
                    "{'longName':'10011','shortName':'10011','types':[23]}],'types':[40,41,29,42,27],'geometry':{'location':{'latitude':40.7414688,'longitude':-74.0033873}," +
                    "'locationType':1,'viewport':{'northeast':{'latitude':40.7428177802915,'longitude':-74.0020383197085},'southwest':{'latitude':40.7401198197085,'longitude':-74.00473628029151}}," +
                    "'bounds':null},'plusCode':{'globalCode':'87G7PXRW+HJ','compoundCode':'PXRW+HJ New York, NY, USA'},'placeId':'ChIJj9Hwdun8ZUARbS4pqpAS_Qk','partialMatch':false,'postcodeLocalities':[]}]," +
                    "'status':'OK'}"),
            });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("maps/api/geocode/json?latlng")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            _localizer = new StringLocalizer<GoogleGeocoding>(factory);
            _coreLocalizer = new StringLocalizer<ClientExecutor>(factory);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Tests the key is properly set into the query string.
        /// </summary>
        [Fact]
        public void AddGoogleKeySuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var query = new NameValueCollection();

            service.AddGoogleKey(query);
            query.Count.Should().Be(1);
            query["key"].Should().Be("abc123");
        }

        /// <summary>
        /// Tests the base parameters are properly set into the query string.
        /// </summary>
        [Fact]
        public void AddBaseParametersSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var query = new NameValueCollection();
            var parameters = new BaseParameters()
            {
                Language = new CultureInfo("da"),
            };

            service.AddBaseParameters(parameters, query);
            query.Count.Should().Be(1);
            query["language"].Should().Be("da");
        }

        /// <summary>
        /// Tests the coordinate parameters are properly set into the query string.
        /// </summary>
        [Fact]
        public void AddCoordinateParametersSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var query = new NameValueCollection();
            var parameters = new CoordinateParameters()
            {
                Location = new Coordinate()
                {
                    Latitude = 76.54,
                    Longitude = 34.56,
                },
                Radius = 10000,
                Language = new CultureInfo("da"),
            };

            service.AddCoordinateParameters(parameters, query);
            query.Count.Should().Be(3);
            query["location"].Should().Be("76.54,34.56");
            query["radius"].Should().Be("10000");
            query["language"].Should().Be("da");
        }

        /// <summary>
        /// Tests the base search parameters are properly set into the query string.
        /// </summary>
        [Fact]
        public void AddBaseSearchParametersSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var query = new NameValueCollection();
            var parameters = new BaseSearchParameters()
            {
                MinimumPrice = 2,
                MaximumPrice = 3,
                OpenNow = false,
                PageToken = 987654,
                Type = "Restaurant",
                Location = new Coordinate()
                {
                    Latitude = 76.14,
                    Longitude = 34.54,
                },
                Radius = 10001,
                Language = new CultureInfo("pt-BR"),
            };

            service.AddBaseSearchParameters(parameters, query);
            query.Count.Should().Be(8);
            query["minprice"].Should().Be("2");
            query["maxprice"].Should().Be("3");
            query["opennow"].Should().Be("false");
            query["pagetoken"].Should().Be("987654");
            query["type"].Should().Be("Restaurant");
            query["location"].Should().Be("76.14,34.54");
            query["radius"].Should().Be("10001");
            query["language"].Should().Be("pt-BR");
        }

        /// <summary>
        /// Tests the autocomplete parameters are properly set into the query string.
        /// </summary>
        [Fact]
        public void AddAutocompleteParametersSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var query = new NameValueCollection();
            var parameters = new QueryAutocompleteParameters()
            {
                Offset = 64,
                Input = "123 East",
                Location = new Coordinate()
                {
                    Latitude = 6.14,
                    Longitude = 3.54,
                },
                Radius = 25000,
                Language = new CultureInfo("fr"),
            };

            service.AddAutocompleteParameters(parameters, query);
            query.Count.Should().Be(5);
            query["offset"].Should().Be("64");
            query["input"].Should().Be("123 East");
            query["location"].Should().Be("6.14,3.54");
            query["radius"].Should().Be("25000");
            query["language"].Should().Be("fr");
        }

        /// <summary>
        /// Tests the base search parameters are properly set into the query string only if they meet the requirements.
        /// </summary>
        [Fact]
        public void AddBaseSearchParametersWithRestrictions1()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var query = new NameValueCollection();
            var parameters = new BaseSearchParameters()
            {
                MinimumPrice = 5,
                MaximumPrice = 6,
                OpenNow = false,
                Location = new Coordinate()
                {
                    Latitude = 76.14,
                    Longitude = 34.54,
                },
                Radius = 60000,
                Language = new CultureInfo("es"),
            };

            service.AddBaseSearchParameters(parameters, query);
            query.Count.Should().Be(3);
            query["opennow"].Should().Be("false");
            query["location"].Should().Be("76.14,34.54");
            query["language"].Should().Be("es");
        }

        /// <summary>
        /// Tests the base search parameters are properly set into the query string only if they meet the requirements.
        /// </summary>
        [Fact]
        public void AddBaseSearchParametersWithRestrictions2()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var query = new NameValueCollection();
            var parameters = new BaseSearchParameters()
            {
                MinimumPrice = 3,
                MaximumPrice = 1,
                OpenNow = false,
                Location = new Coordinate()
                {
                    Latitude = 76.14,
                    Longitude = 34.54,
                },
                Radius = 0,
                Language = new CultureInfo("es"),
            };

            service.AddBaseSearchParameters(parameters, query);
            query.Count.Should().Be(3);
            query["opennow"].Should().Be("false");
            query["location"].Should().Be("76.14,34.54");
            query["language"].Should().Be("es");
        }

        /// <summary>
        /// Tests the building of the query autocomplete parameters is done successfully.
        /// </summary>
        [Fact]
        public void BuildQueryAutocompleteRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new QueryAutocompleteParameters()
            {
                Offset = 64,
                Input = "123 East",
                Location = new Coordinate()
                {
                    Latitude = 6.14,
                    Longitude = 3.54,
                },
                Radius = 25000,
                Language = new CultureInfo("fr"),
            };

            var uri = service.BuildQueryAutocompleteRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("offset=64");
            query.Should().Contain("input=123 East");
            query.Should().Contain("location=6.14,3.54");
            query.Should().Contain("radius=25000");
            query.Should().Contain("language=fr");
            query.Should().Contain("key=abc123");
        }

        /// <summary>
        /// Tests the query autocomplete uri isn't built if an input isn't passed in.
        /// </summary>
        [Fact]
        public void BuildQueryAutocompleteRequestWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildQueryAutocompleteRequest(new QueryAutocompleteParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Input')");
        }

        /// <summary>
        /// Tests the building of the place autocomplete parameters is done successfully.
        /// </summary>
        [Fact]
        public void BuildPlaceAutocompleteRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new PlacesAutocompleteParameters()
            {
                Offset = 64,
                Input = "123 East",
                Location = new Coordinate()
                {
                    Latitude = 6.14,
                    Longitude = 3.54,
                },
                Radius = 25000,
                Language = new CultureInfo("fr"),
                SessionToken = "test123",
                Origin = new Coordinate()
                {
                    Latitude = 34.12,
                    Longitude = 69.45,
                },
                Components = new Component()
                {
                    Locality = "tests",
                    PostalCode = "12345",
                },
                StrictBounds = true,
            };

            parameters.Types.Add(PlaceType.Address);
            parameters.Types.Add(PlaceType.Establishment);
            parameters.Types.Add(PlaceType.Regions);

            var uri = service.BuildPlaceAutocompleteRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("offset=64");
            query.Should().Contain("input=123 East");
            query.Should().Contain("location=6.14,3.54");
            query.Should().Contain("radius=25000");
            query.Should().Contain("language=fr");
            query.Should().Contain("sessiontoken=test123");
            query.Should().Contain("origin=34.12,69.45");
            query.Should().Contain("types=address,establishment,regions");
            query.Should().Contain("components=postal_code:12345|locality:tests");
            query.Should().Contain("strictbounds=true");
            query.Should().Contain("key=abc123");
        }

        /// <summary>
        /// Tests the place autocomplete uri isn't built if an input isn't passed in.
        /// </summary>
        [Fact]
        public void BuildPlaceAutocompleteRequestWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildPlaceAutocompleteRequest(new PlacesAutocompleteParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Input')");
        }

        /// <summary>
        /// Tests the building of the details parameters is done successfully.
        /// </summary>
        [Fact]
        public void BuildDetailsRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new DetailsParameters()
            {
                PlaceId = "1a2b3c",
                Region = new RegionInfo("ES"),
                SessionToken = "test123",
                Language = new CultureInfo("fr"),
            };

            parameters.Fields.Add("field1");
            parameters.Fields.Add("field2");
            parameters.Fields.Add("field3");

            var uri = service.BuildDetailsRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("place_id=1a2b3c");
            query.Should().Contain("region=es");
            query.Should().Contain("sessiontoken=test123");
            query.Should().Contain("fields=field1,field2,field3");
            query.Should().Contain("language=fr");
            query.Should().Contain("key=abc123");
        }

        /// <summary>
        /// Tests the details uri isn't built if an place id isn't passed in.
        /// </summary>
        [Fact]
        public void BuildDetailsRequestWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildDetailsRequest(new DetailsParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'PlaceId')");
        }

        /// <summary>
        /// Tests the building of the text search parameters is done successfully.
        /// </summary>
        [Fact]
        public void BuildTextSearchRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new TextSearchParameters()
            {
                Query = "456 West",
                Region = new RegionInfo("ES"),
                MinimumPrice = 1,
                MaximumPrice = 3,
                OpenNow = true,
                Language = new CultureInfo("es"),
            };

            var uri = service.BuildTextSearchRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("query=456 West");
            query.Should().Contain("region=es");
            query.Should().Contain("minprice=1");
            query.Should().Contain("maxprice=3");
            query.Should().Contain("opennow=true");
            query.Should().Contain("language=es");
            query.Should().Contain("key=abc123");
        }

        /// <summary>
        /// Tests the text search uri isn't built if an query isn't passed in.
        /// </summary>
        [Fact]
        public void BuildTextSearchRequestWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildTextSearchRequest(new TextSearchParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Query')");
        }

        /// <summary>
        /// Tests the building of the nearby search parameters is done successfully.
        /// </summary>
        [Fact]
        public void BuildNearbySearchRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new NearbySearchParameters()
            {
                RankBy = RankType.Prominence,
                Keyword = "Test",
                Location = new Coordinate()
                {
                    Latitude = 39.28,
                    Longitude = -21.04,
                },
                Radius = 2,
                Language = new CultureInfo("es"),
            };

            var uri = service.BuildNearbySearchRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("keyword=Test");
            query.Should().Contain("rankby=prominence");
            query.Should().Contain("location=39.28,-21.04");
            query.Should().Contain("radius=2");
            query.Should().Contain("language=es");
            query.Should().Contain("key=abc123");
        }

        /// <summary>
        /// Tests the nearby search uri isn't built if an location isn't passed in.
        /// </summary>
        [Fact]
        public void BuildNearbySearchRequestWithException1()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildNearbySearchRequest(new NearbySearchParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Location')");
        }

        /// <summary>
        /// Tests the nearby search uri isn't built if an query isn't passed in.
        /// </summary>
        [Fact]
        public void BuildNearbySearchRequestWithException2()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new NearbySearchParameters()
            {
                Location = new Coordinate()
                {
                    Latitude = 39.28,
                    Longitude = -21.04,
                },
                RankBy = RankType.Distance,
                Radius = 1,
            };

            Action act = () => service.BuildNearbySearchRequest(parameters);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Radius')");
        }

        /// <summary>
        /// Tests the nearby search uri isn't built if an query isn't passed in.
        /// </summary>
        [Fact]
        public void BuildNearbySearchRequestWithException3()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new NearbySearchParameters()
            {
                Location = new Coordinate()
                {
                    Latitude = 39.28,
                    Longitude = -21.04,
                },
                RankBy = RankType.Distance,
                Radius = 0,
            };

            Action act = () => service.BuildNearbySearchRequest(parameters);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'RankBy')");
        }

        /// <summary>
        /// Tests the nearby search uri isn't built if an query isn't passed in.
        /// </summary>
        [Fact]
        public void BuildNearbySearchRequestWithException4()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new NearbySearchParameters()
            {
                Location = new Coordinate()
                {
                    Latitude = 39.28,
                    Longitude = -21.04,
                },
                RankBy = RankType.Prominence,
                Radius = 0,
            };

            Action act = () => service.BuildNearbySearchRequest(parameters);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Radius')");
        }

        /// <summary>
        /// Tests the building of the find place parameters is done successfully.
        /// </summary>
        [Fact]
        public void BuildFindPlaceRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new FindPlacesParameters()
            {
                Input = "97 Test",
                InputType = InputType.TextQuery,
                LocationBias = new Circle()
                {
                    Coordinate = new Coordinate()
                    {
                        Latitude = 28.15,
                        Longitude = -91.23,
                    },
                    Radius = 54,
                },
                Language = new CultureInfo("fr"),
            };

            parameters.Fields.Add("field1");
            parameters.Fields.Add("field2");
            parameters.Fields.Add("field3");

            var uri = service.BuildFindPlaceRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("input=97 Test");
            query.Should().Contain("inputtype=textquery");
            query.Should().Contain("locationbias=circle:54@28.15,-91.23");
            query.Should().Contain("fields=field1,field2,field3");
            query.Should().Contain("language=fr");
            query.Should().Contain("key=abc123");
        }

        /// <summary>
        /// Tests the find places uri isn't built if an input isn't passed in.
        /// </summary>
        [Fact]
        public void BuildFindPlaceRequestWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildFindPlaceRequest(new FindPlacesParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Input')");
        }

        /// <summary>
        /// Tests the geocoding uri isn't built if an address isn't passed in.
        /// </summary>
        [Fact]
        public void BuildGeocodingRequestWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildGeocodingRequest(new GeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Address')");
        }

        /// <summary>
        /// Tests the geocoding uri is built properly.
        /// </summary>
        [Fact]
        public void BuildGeocodingRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new GeocodingParameters()
            {
                Address = "123 East",
                Components = new Component()
                {
                    Route = "cctld",
                    Country = new RegionInfo("CA"),
                    AdministrativeArea = "detroit",
                },
                Bounds = new Boundaries()
                {
                    Northeast = new Coordinate()
                    {
                        Latitude = 80.012,
                        Longitude = 123.456,
                    },
                    Southwest = new Coordinate()
                    {
                        Latitude = 43.219,
                        Longitude = 87.654,
                    },
                },
                Region = new RegionInfo("US"),
                Language = new CultureInfo("en"),
            };

            var uri = service.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("address=123 East");
            query.Should().Contain("components=country:ca|route:cctld|administrative_area:detroit");
            query.Should().Contain("bounds=43.219,87.654|80.012,123.456");
            query.Should().Contain("region=us");
            query.Should().Contain("language=en");
            query.Should().Contain("key=abc123");
        }

        /// <summary>
        /// Tests the reverse geocoding uri isn't built if an address isn't passed in.
        /// </summary>
        [Fact]
        public void BuildReverseGeocodingRequestWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildReverseGeocodingRequest(new ReverseGeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Coordinate')");
        }

        /// <summary>
        /// Tests the reverse geocoding uri is built properly.
        /// </summary>
        [Fact]
        public void BuildReverseGeocodingRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate()
                {
                    Latitude = 80.012,
                    Longitude = 123.456,
                },
                ResultTypes = new List<ResultType>()
                {
                    ResultType.Park,
                    ResultType.Airport,
                    ResultType.Sublocality,
                },
                LocationTypes = new List<LocationType>()
                {
                    LocationType.Approximate,
                    LocationType.Rooftop,
                },
                Language = new CultureInfo("en"),
            };

            var uri = service.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("latlng=80.012,123.456");
            query.Should().Contain($"result_type={ResultType.Park.ToEnumString<ResultType>()}|{ResultType.Airport.ToEnumString<ResultType>()}|{ResultType.Sublocality.ToEnumString<ResultType>()}");
            query.Should().Contain($"location_type={LocationType.Approximate.ToEnumString<LocationType>()}|{LocationType.Rooftop.ToEnumString<LocationType>()}");
            query.Should().Contain("language=en");
            query.Should().Contain("key=abc123");
        }

        /// <summary>
        /// Tests the geocoding returns a response successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task GeocodingAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new GeocodingParameters()
            {
                Address = "1600 Amphitheatre Pkwy, Mountain View",
            };

            var response = await service.GeocodingAsync(parameters).ConfigureAwait(false);
            response.Status.Should().Be("OK");
            response.Results.Count().Should().Be(2);
        }

        /// <summary>
        /// Tests the reverse geocoding returns a response successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task ReverseGeocodingAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate()
                {
                    Latitude = 40.7415,
                    Longitude = -74.0034,
                },
            };

            var response = await service.ReverseGeocodingAsync(parameters).ConfigureAwait(false);
            response.Status.Should().Be("OK");
            response.Results.Count().Should().Be(1);
        }

        /// <summary>
        /// Tests the region info to ccTLD works properly.
        /// </summary>
        [Fact]
        public void RegionInfoToCCTLDSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new GoogleGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);

            service.RegionInfoToCCTLD(new RegionInfo("GB")).Should().Be("uk");
            service.RegionInfoToCCTLD(new RegionInfo("US")).Should().Be("us");
            service.RegionInfoToCCTLD(new RegionInfo("en-CA")).Should().Be("ca");
            service.RegionInfoToCCTLD(new RegionInfo("fr-FR")).Should().Be("fr");
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">A boolean flag indicating whether or not to dispose of objects.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var message in _responseMessages)
                {
                    message?.Dispose();
                }
            }

            _disposed = true;
        }
    }
}