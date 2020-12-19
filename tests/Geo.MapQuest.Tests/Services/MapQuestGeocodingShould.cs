// <copyright file="MapQuestGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapQuest.Tests.Services
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using FluentAssertions;
    using Geo.MapQuest.Enums;
    using Geo.MapQuest.Models;
    using Geo.MapQuest.Models.Exceptions;
    using Geo.MapQuest.Models.Parameters;
    using Geo.MapQuest.Services;
    using Moq;
    using Moq.Protected;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="MapQuestGeocoding"/> class.
    /// </summary>
    [TestFixture]
    public class MapQuestGeocodingShould
    {
        private Mock<HttpMessageHandler> _mockHandler;
        private MapQuestKeyContainer _keyContainer;
        private MapQuestEndpoint _endpoint;

        /// <summary>
        /// One time setup information.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _keyContainer = new MapQuestKeyContainer("abc123");
            _endpoint = new MapQuestEndpoint(true);

            _mockHandler = new Mock<HttpMessageHandler>();

            // For reverse geocoding, use the places endpoint type
            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("geocoding/v1/address")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        "{\"info\":{\"statuscode\":0,\"copyright\":{\"text\":\"\u00A9 2020 MapQuest, Inc.\",\"imageUrl\":\"http://api.mqcdn.com/res/mqlogo.gif\",\"imageAltText\":\"\u00A9 2020 MapQuest, Inc.\"},\"messages\":[]}," +
                        "\"options\":{\"maxResults\":5,\"thumbMaps\":true,\"ignoreLatLngInput\":false}," +
                        "\"results\":[" +
                        "{\"providedLocation\":{\"location\":\"123 East\"}," +
                        "\"locations\":[" +
                        "{\"street\":\"123 East\",\"adminArea6\":\"\",\"adminArea6Type\":\"Neighborhood\",\"adminArea5\":\"Wisconsin Dells\"," +
                        "\"adminArea5Type\":\"City\",\"adminArea4\":\"Sauk\",\"adminArea4Type\":\"County\",\"adminArea3\":\"WI\",\"adminArea3Type\":\"State\",\"adminArea1\":\"US\",\"adminArea1Type\":\"Country\"," +
                        "\"postalCode\":\"53965\",\"geocodeQualityCode\":\"L1AAA\",\"geocodeQuality\":\"ADDRESS\",\"dragPoint\":false,\"sideOfStreet\":\"L\",\"linkId\":\"rnr3523773|i65517526\",\"unknownInput\":\"\"," +
                        "\"type\":\"s\",\"latLng\":{\"lat\":43.591268,\"lng\":-89.791489},\"displayLatLng\":{\"lat\":43.591468,\"lng\":-89.791492}," +
                        "\"mapUrl\":\"http://www.mapquestapi.com/staticmap/v5/map?key=1tdjnAYrIM60mtjMgyxJ4Gp7c5zCqD1x&type=map&size=225,160&locations=43.591268,-89.791489|marker-sm-50318A-1&scalebar=true&zoom=15&rand=-1701703799\"}" +
                        "]}" +
                        "]}"),
                });

            // For reverse geocoding, use the permanent endpoint type
            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("geocoding/v1/reverse")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        "{\"info\":{\"statuscode\":0,\"copyright\":{\"text\":\"\u00A9 2020 MapQuest, Inc.\",\"imageUrl\":\"http://api.mqcdn.com/res/mqlogo.gif\",\"imageAltText\":\"\u00A9 2020 MapQuest, Inc.\"},\"messages\":[]}," +
                        "\"options\":{\"maxResults\":1,\"thumbMaps\":true,\"ignoreLatLngInput\":false}," +
                        "\"results\":[" +
                        "{\"providedLocation\":{\"latLng\":{\"lat\":56.78,\"lng\":123.45}}," +
                        "\"locations\":[" +
                        "{\"street\":\"\",\"adminArea6\":\"\",\"adminArea6Type\":\"Neighborhood\",\"adminArea5\":\"\",\"adminArea5Type\":\"City\",\"adminArea4\":\"\"," +
                        "\"adminArea4Type\":\"County\",\"adminArea3\":\"F\u00F6derationskreis Ferner Osten\",\"adminArea3Type\":\"State\",\"adminArea1\":\"RU\",\"adminArea1Type\":\"Country\"," +
                        "\"postalCode\":\"\",\"geocodeQualityCode\":\"A3XAX\",\"geocodeQuality\":\"STATE\",\"dragPoint\":false,\"sideOfStreet\":\"N\",\"linkId\":\"0\",\"unknownInput\":\"\",\"type\":\"s\"," +
                        "\"latLng\":{\"lat\":56.78,\"lng\":123.45},\"displayLatLng\":{\"lat\":56.78,\"lng\":123.45}," +
                        "\"mapUrl\":\"http://www.mapquestapi.com/staticmap/v5/map?key=1tdjnAYrIM60mtjMgyxJ4Gp7c5zCqD1x&type=map&size=225,160&locations=56.78,123.45|marker-sm-50318A-1&scalebar=true&zoom=5&rand=1696523291\"}" +
                        "]}" +
                        "]}"),
                });
        }

        /// <summary>
        /// Tests the key is properly set into the query string.
        /// </summary>
        [Test]
        public void AddMapBoxKeySuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, _endpoint);
            var query = new NameValueCollection();

            service.AddMapQuestKey(query);
            query.Count.Should().Be(1);
            query["key"].Should().Be("abc123");
        }

        /// <summary>
        /// Tests the base parameters are properly set into the query string.
        /// </summary>
        [Test]
        public void AddBaseParametersSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, _endpoint);
            var query = new NameValueCollection();
            var parameters = new BaseParameters()
            {
                IncludeThumbMaps = true,
            };

            service.AddBaseParameters(parameters, query);
            query.Count.Should().Be(1);
            query["thumbMaps"].Should().Be("true");
        }

        /// <summary>
        /// Tests the building of the licensed geocoding parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildLicensedGeocodingRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, _endpoint);
            var parameters = new GeocodingParameters()
            {
                Location = "123 East",
                BoundingBox = new BoundingBox()
                {
                    West = 123.45,
                    North = 87.65,
                    East = 165.43,
                    South = 45.67,
                },
                IgnoreLatLngInput = true,
                MaxResults = 7,
                IntlMode = InternationalMode.FiveBox,
                IncludeThumbMaps = false,
            };

            var uri = service.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("location=123 East");
            query.Should().Contain("boundingBox=87.65,123.45,45.67,165.43");
            query.Should().Contain("ignoreLatLngInput=true");
            query.Should().Contain("maxResults=7");
            query.Should().Contain("intlMode=5BOX");
            query.Should().Contain("thumbMaps=false");
            query.Should().Contain("key=abc123");

            var fullUri = HttpUtility.UrlDecode(uri.AbsoluteUri);
            fullUri.Should().Contain("mapquestapi.com/geocoding/v1/address");
        }

        /// <summary>
        /// Tests the building of the non-licensed geocoding parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildNonLicensedGeocodingRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, new MapQuestEndpoint(false));
            var parameters = new GeocodingParameters()
            {
                Location = "123 East",
                BoundingBox = new BoundingBox()
                {
                    West = 0,
                    North = 0,
                    East = 0,
                    South = 0,
                },
                IgnoreLatLngInput = false,
                MaxResults = 1,
                IntlMode = InternationalMode.OneBox,
                IncludeThumbMaps = false,
            };

            var uri = service.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("location=123 East");
            query.Should().Contain("ignoreLatLngInput=false");
            query.Should().Contain("maxResults=1");
            query.Should().Contain("intlMode=1BOX");
            query.Should().Contain("thumbMaps=false");
            query.Should().Contain("key=abc123");

            var fullUri = HttpUtility.UrlDecode(uri.AbsoluteUri);
            fullUri.Should().Contain("open.mapquestapi.com/geocoding/v1/address");
        }

        /// <summary>
        /// Tests the building of the geocoding parameters fails if no location is provided.
        /// </summary>
        [Test]
        public void BuildGeocodingRequestFailsWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, _endpoint);
            Action act = () => service.BuildGeocodingRequest(new GeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The location cannot be null or empty. (Parameter 'Location')");
        }

        /// <summary>
        /// Tests the building of the licensed reverse geocoding parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildLicensedReverseGeocodingRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, _endpoint);
            var parameters = new ReverseGeocodingParameters()
            {
                Location = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                IncludeNearestIntersection = true,
                IncludeRoadMetadata = true,
                IncludeThumbMaps = false,
            };

            var uri = service.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("location=56.78,78.91");
            query.Should().Contain("includeNearestIntersection=true");
            query.Should().Contain("includeRoadMetadata=true");
            query.Should().Contain("thumbMaps=false");
            query.Should().Contain("key=abc123");

            var fullUri = HttpUtility.UrlDecode(uri.AbsoluteUri);
            fullUri.Should().Contain("mapquestapi.com/geocoding/v1/reverse");
        }

        /// <summary>
        /// Tests the building of the non-licensed reverse geocoding parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildNonLicensedReverseGeocodingRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, new MapQuestEndpoint(false));
            var parameters = new ReverseGeocodingParameters()
            {
                Location = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                IncludeNearestIntersection = false,
                IncludeRoadMetadata = false,
                IncludeThumbMaps = true,
            };

            var uri = service.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("location=56.78,78.91");
            query.Should().Contain("includeNearestIntersection=false");
            query.Should().Contain("includeRoadMetadata=false");
            query.Should().Contain("thumbMaps=true");
            query.Should().Contain("key=abc123");

            var fullUri = HttpUtility.UrlDecode(uri.AbsoluteUri);
            fullUri.Should().Contain("open.mapquestapi.com/geocoding/v1/reverse");
        }

        /// <summary>
        /// Tests the building of the reverse geocoding parameters fails if no location is provided.
        /// </summary>
        [Test]
        public void BuildReverseGeocodingRequestFailsWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, _endpoint);
            Action act = () => service.BuildReverseGeocodingRequest(new ReverseGeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The location cannot be null. (Parameter 'Location')");
        }

        /// <summary>
        /// Tests the validation and creation of the reverse geocoding uri is done successfully.
        /// </summary>
        [Test]
        public void ValidateAndCraftUriSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, _endpoint);
            var parameters = new ReverseGeocodingParameters()
            {
                Location = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                IncludeNearestIntersection = false,
                IncludeRoadMetadata = false,
                IncludeThumbMaps = true,
            };

            var uri = service.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("location=56.78,78.91");
            query.Should().Contain("includeNearestIntersection=false");
            query.Should().Contain("includeRoadMetadata=false");
            query.Should().Contain("thumbMaps=true");
            query.Should().Contain("key=abc123");

            var fullUri = HttpUtility.UrlDecode(uri.AbsoluteUri);
            fullUri.Should().Contain("mapquestapi.com/geocoding/v1/reverse");
        }

        /// <summary>
        /// Tests the validation and creation of the reverse geocoding uri fails if the parameters are null.
        /// </summary>
        [Test]
        public void ValidateAndCraftUriFailsWithException1()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, _endpoint);
            Action act = () => service.ValidateAndCraftUri<ReverseGeocodingParameters>(null, service.BuildReverseGeocodingRequest);

            act.Should()
                .Throw<MapQuestException>()
                .WithMessage("The MapQuest parameters are null. Please see the inner exception for more information.")
                .WithInnerException<ArgumentNullException>();
        }

        /// <summary>
        /// Tests the validation and creation of the reverse geocoding uri fails if no id is provided and the exception is wrapped in a here exception.
        /// </summary>
        [Test]
        public void ValidateAndCraftUriFailsWithException2()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, _endpoint);
            Action act = () => service.ValidateAndCraftUri<ReverseGeocodingParameters>(new ReverseGeocodingParameters(), service.BuildReverseGeocodingRequest);

            act.Should()
                .Throw<MapQuestException>()
                .WithMessage("Failed to create the MapQuest uri. Please see the inner exception for more information.")
                .WithInnerException<ArgumentException>()
                .WithMessage("The location cannot be null. (Parameter 'Location')");
        }

        /// <summary>
        /// Tests the geocoding call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task GeocodingAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, _endpoint);
            var parameters = new GeocodingParameters()
            {
                Location = "123 East",
                BoundingBox = new BoundingBox()
                {
                    West = 0,
                    North = 0,
                    East = 0,
                    South = 0,
                },
                IgnoreLatLngInput = false,
                MaxResults = 1,
                IntlMode = InternationalMode.OneBox,
                IncludeThumbMaps = false,
            };

            var result = await service.GeocodingAsync(parameters).ConfigureAwait(false);
            result.Results.Count.Should().Be(1);
            result.Results[0].Locations.Count.Should().Be(1);
            result.Results[0].Locations.Count.Should().Be(1);
            result.Results[0].ProvidedLocation.Location.Should().Be("123 East");
        }

        /// <summary>
        /// Tests the reverse geocoding call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task ReverseGeocodingAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapQuestGeocoding(httpClient, _keyContainer, _endpoint);
            var parameters = new ReverseGeocodingParameters()
            {
                Location = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                IncludeNearestIntersection = false,
                IncludeRoadMetadata = false,
                IncludeThumbMaps = true,
            };

            var result = await service.ReverseGeocodingAsync(parameters).ConfigureAwait(false);
            result.Results.Count.Should().Be(1);
            result.Results[0].Locations.Count.Should().Be(1);
            result.Results[0].Locations.Count.Should().Be(1);
            result.Results[0].ProvidedLocation.Coordinate.ToString().Should().Be(new Coordinate()
            {
                Latitude = 56.78,
                Longitude = 123.45,
            }.ToString());
        }
    }
}