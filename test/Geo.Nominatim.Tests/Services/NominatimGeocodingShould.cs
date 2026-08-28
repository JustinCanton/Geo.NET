// <copyright file="NominatimGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using FluentAssertions;
    using Geo.Core;
    using Geo.Core.Models.Exceptions;
    using Geo.Nominatim.Enums;
    using Geo.Nominatim.Models.Parameters;
    using Geo.Nominatim.Services;
    using Geo.Nominatim.Settings;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="NominatimGeocoding"/> class.
    /// </summary>
    public class NominatimGeocodingShould : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Mock<IOptions<NominatimOptions>> _options = new Mock<IOptions<NominatimOptions>>();
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="NominatimGeocodingShould"/> class.
        /// </summary>
        public NominatimGeocodingShould()
        {
            _options
                .Setup(x => x.Value)
                .Returns(new NominatimOptions()
                {
                    Email = "test@example.com",
                });

            var mockHandler = new Mock<HttpMessageHandler>();

            // Mock response for search endpoint
            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    "[{" +
                    "\"place_id\": 297586605," +
                    "\"licence\": \"Data © OpenStreetMap contributors, ODbL 1.0. http://osm.org/copyright\"," +
                    "\"osm_type\": \"node\"," +
                    "\"osm_id\": 240109189," +
                    "\"lat\": \"52.5170365\"," +
                    "\"lon\": \"13.3888599\"," +
                    "\"category\": \"place\"," +
                    "\"type\": \"city\"," +
                    "\"place_rank\": 15," +
                    "\"importance\": 0.9654895725296," +
                    "\"addressrank\": 16," +
                    "\"display_name\": \"Berlin, Deutschland\"," +
                    "\"boundingbox\": [\"52.3570365\", \"52.6770365\", \"13.2288599\", \"13.5488599\"]" +
                    "}]"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Contains("search")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            // Mock response for reverse endpoint
            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    "{" +
                    "\"place_id\": 297586605," +
                    "\"licence\": \"Data © OpenStreetMap contributors, ODbL 1.0. http://osm.org/copyright\"," +
                    "\"osm_type\": \"node\"," +
                    "\"osm_id\": 240109189," +
                    "\"lat\": \"52.5170365\"," +
                    "\"lon\": \"13.3888599\"," +
                    "\"display_name\": \"Berlin, Deutschland\"," +
                    "\"address\": {" +
                    "\"city\": \"Berlin\"," +
                    "\"country\": \"Deutschland\"," +
                    "\"country_code\": \"de\"" +
                    "}," +
                    "\"boundingbox\": [\"52.3570365\", \"52.6770365\", \"13.2288599\", \"13.5488599\"]" +
                    "}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Contains("reverse")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            // Mock response for lookup endpoint
            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    "[{" +
                    "\"place_id\": 297586605," +
                    "\"licence\": \"Data © OpenStreetMap contributors, ODbL 1.0. http://osm.org/copyright\"," +
                    "\"osm_type\": \"node\"," +
                    "\"osm_id\": 240109189," +
                    "\"lat\": \"52.5170365\"," +
                    "\"lon\": \"13.3888599\"," +
                    "\"category\": \"place\"," +
                    "\"type\": \"city\"," +
                    "\"place_rank\": 15," +
                    "\"importance\": 0.9654895725296," +
                    "\"addressrank\": 16," +
                    "\"display_name\": \"Berlin, Deutschland\"," +
                    "\"boundingbox\": [\"52.3570365\", \"52.6770365\", \"13.2288599\", \"13.5488599\"]" +
                    "}]"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.Contains("lookup")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _httpClient = new HttpClient(mockHandler.Object);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void AddEmail_WithOptions_SuccessfullyAddsEmail()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new SearchParameters();

            sut.AddEmail(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["email"].Should().Be("test@example.com");
        }

        [Fact]
        public void AddEmail_WithParameterOverride_SuccessfullyAddsEmail()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new SearchParameters() { Email = "override@example.com" };

            sut.AddEmail(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["email"].Should().Be("override@example.com");
        }

        /// <summary>
        /// Tests the base parameters are properly set into the query string.
        /// </summary>
        [Fact]
        public void AddBaseParametersSuccessfully()
        {
            var query = QueryString.Empty;
            var parameters = new SearchParameters()
            {
                AddressDetails = true,
                ExtraInfo = false,
                NameDetails = true,
                AcceptLanguage = "en,de",
            };

            NominatimGeocoding.AddBaseParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(5);
            queryParameters["format"].Should().Be("jsonv2");
            queryParameters["addressdetails"].Should().Be("1");
            queryParameters["extratags"].Should().Be("0");
            queryParameters["namedetails"].Should().Be("1");
            queryParameters["accept-language"].Should().Be("en,de");
        }

        /// <summary>
        /// Tests the building of the search parameters is done successfully.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildSearchRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new SearchParameters()
            {
                Query = "Berlin",
                ViewBox = new BoundingBox { West = 13.0, South = 52.3, East = 13.7, North = 52.7 },
                Bounded = true,
                Limit = 5,
                AddressDetails = true,
            };

            parameters.CountryCodes.Add("de");
            parameters.CountryCodes.Add("at");

            // Act
            var uri = sut.BuildSearchRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("q=Berlin");
            query.Should().Contain("viewbox=13,52.7,13.7,52.3");
            query.Should().Contain("bounded=1");
            query.Should().Contain("limit=5");
            query.Should().Contain("countrycodes=de,at");
            query.Should().Contain("addressdetails=1");
            query.Should().Contain("format=jsonv2");
            query.Should().Contain("email=test@example.com");

            var fullUri = HttpUtility.UrlDecode(uri.AbsoluteUri);
            fullUri.Should().Contain("nominatim.openstreetmap.org/search");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the building of the structured search parameters is done successfully.
        /// </summary>
        [Fact]
        public void BuildStructuredSearchRequestSuccessfully()
        {
            var sut = BuildService();

            var parameters = new SearchParameters()
            {
                Street = "Unter den Linden",
                City = "Berlin",
                Country = "Germany",
                AddressDetails = false,
            };

            var uri = sut.BuildSearchRequest(parameters);

            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("street=Unter den Linden");
            query.Should().Contain("city=Berlin");
            query.Should().Contain("country=Germany");
            query.Should().Contain("addressdetails=0");
            query.Should().Contain("format=jsonv2");

            var fullUri = HttpUtility.UrlDecode(uri.AbsoluteUri);
            fullUri.Should().Contain("nominatim.openstreetmap.org/search");
        }

        /// <summary>
        /// Tests the building of the search parameters fails if no query is provided.
        /// </summary>
        [Fact]
        public void BuildSearchRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildSearchRequest(new SearchParameters());

            act.Should().Throw<ArgumentException>();
        }

        /// <summary>
        /// Tests the building of the reverse geocoding parameters is done successfully.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildReverseGeocodeRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Latitude = 52.517037,
                Longitude = 13.388860,
                Zoom = 18,
                PolygonOutput = PolygonOutput.GeoJSON,
                PolygonThreshold = 0.01f,
                AddressDetails = true,
            };

            parameters.Layers.Add(LayerType.Address);
            parameters.Layers.Add(LayerType.POI);

            // Act
            var uri = sut.BuildReverseGeocodeRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("lat=52.517037");
            query.Should().Contain("lon=13.38886");
            query.Should().Contain("zoom=18");
            query.Should().Contain("layer=address,poi");
            query.Should().Contain("polygon_geojson=1");
            query.Should().Contain("polygon_threshold=0.01");
            query.Should().Contain("addressdetails=1");
            query.Should().Contain("format=jsonv2");

            var fullUri = HttpUtility.UrlDecode(uri.AbsoluteUri);
            fullUri.Should().Contain("nominatim.openstreetmap.org/reverse");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the building of the lookup parameters is done successfully.
        /// </summary>
        [Fact]
        public void BuildLookupRequestSuccessfully()
        {
            var sut = BuildService();

            var parameters = new LookupParameters()
            {
                PolygonOutput = PolygonOutput.KML,
                AddressDetails = true,
            };

            parameters.OsmIds.Add("R146656");
            parameters.OsmIds.Add("W104393803");
            parameters.OsmIds.Add("N240109189");

            var uri = sut.BuildLookupRequest(parameters);

            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("osm_ids=R146656,W104393803,N240109189");
            query.Should().Contain("polygon_kml=1");
            query.Should().Contain("addressdetails=1");
            query.Should().Contain("format=jsonv2");

            var fullUri = HttpUtility.UrlDecode(uri.AbsoluteUri);
            fullUri.Should().Contain("nominatim.openstreetmap.org/lookup");
        }

        /// <summary>
        /// Tests the validation and creation of the search uri is done successfully.
        /// </summary>
        [Fact]
        public void ValidateAndCraftUriSuccessfully()
        {
            var sut = BuildService();

            var parameters = new SearchParameters()
            {
                Query = "Berlin",
                Limit = 1,
            };

            var uri = sut.ValidateAndBuildUri(parameters, sut.BuildSearchRequest);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("q=Berlin");
            query.Should().Contain("limit=1");
        }

        /// <summary>
        /// Tests the validation and creation of the search uri fails if the parameters are null.
        /// </summary>
        [Fact]
        public void ValidateAndCraftUriFailsWithException1()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<SearchParameters>(null, sut.BuildSearchRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithMessage("*See the inner exception for more information.")
                .WithInnerException<ArgumentNullException>();
        }

        /// <summary>
        /// Tests the validation and creation of the search uri fails if no query is provided and the exception is wrapped in a nominatim exception.
        /// </summary>
        [Fact]
        public void ValidateAndCraftUriFailsWithException2()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<SearchParameters>(new SearchParameters(), sut.BuildSearchRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithMessage("*See the inner exception for more information.")
                .WithInnerException<ArgumentException>();
        }

        /// <summary>
        /// Tests the search call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task SearchAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new SearchParameters()
            {
                Query = "Berlin",
                Limit = 1,
            };

            var result = await sut.SearchAsync(parameters);
            result.Count.Should().Be(1);
            result[0].PlaceId.Should().Be(297586605);
            result[0].DisplayName.Should().Be("Berlin, Deutschland");
            result[0].Latitude.Should().Be("52.5170365");
            result[0].Longitude.Should().Be("13.3888599");
        }

        /// <summary>
        /// Tests the reverse geocoding call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task ReverseGeocodeAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Latitude = 52.517037,
                Longitude = 13.388860,
                Zoom = 18,
            };

            var result = await sut.ReverseGeocodeAsync(parameters);
            result.PlaceId.Should().Be(297586605);
            result.DisplayName.Should().Be("Berlin, Deutschland");
            result.Address.Should().NotBeNull();
            result.Address.City.Should().Be("Berlin");
            result.Address.Country.Should().Be("Deutschland");
        }

        /// <summary>
        /// Tests the lookup call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task LookupAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new LookupParameters();

            parameters.OsmIds.Add("N240109189");

            var result = await sut.LookupAsync(parameters);
            result.Count.Should().Be(1);
            result[0].PlaceId.Should().Be(297586605);
            result[0].DisplayName.Should().Be("Berlin, Deutschland");
            result[0].OsmType.Should().Be("node");
            result[0].OsmId.Should().Be(240109189);
        }

        /// <summary>
        /// Tests the default public Nominatim instance is used when no server is configured.
        /// </summary>
        [Fact]
        public void BuildEndpointUri_WithNoConfiguredServer_UsesThePublicInstance()
        {
            var sut = BuildService(new NominatimOptions() { Server = null });

            var uri = sut.BuildEndpointUri("search").Uri;

            uri.AbsoluteUri.Should().Be("https://nominatim.openstreetmap.org/search");
        }

        /// <summary>
        /// Tests an alternative publicly hosted Nominatim instance is used when it is configured.
        /// </summary>
        [Fact]
        public void BuildEndpointUri_WithAlternatePublicServer_UsesThatServer()
        {
            var sut = BuildService(new NominatimOptions() { Server = "https://nominatim.qgis.org" });

            var uri = sut.BuildEndpointUri("reverse").Uri;

            uri.AbsoluteUri.Should().Be("https://nominatim.qgis.org/reverse");
        }

        /// <summary>
        /// Tests a self hosted Nominatim instance served under a path keeps that path.
        /// </summary>
        [Fact]
        public void BuildEndpointUri_WithSelfHostedServerUnderAPath_KeepsThePath()
        {
            var sut = BuildService(new NominatimOptions() { Server = "https://my-server.example.com/nominatim/" });

            var uri = sut.BuildEndpointUri("lookup").Uri;

            uri.AbsoluteUri.Should().Be("https://my-server.example.com/nominatim/lookup");
        }

        /// <summary>
        /// Tests a self hosted Nominatim instance on a non standard port is used as configured.
        /// </summary>
        [Fact]
        public void BuildEndpointUri_WithSelfHostedServerOnAPort_UsesThatServer()
        {
            var sut = BuildService(new NominatimOptions() { Server = "http://localhost:8080" });

            var uri = sut.BuildEndpointUri("search").Uri;

            uri.AbsoluteUri.Should().Be("http://localhost:8080/search");
        }

        /// <summary>
        /// Tests the configured server is used when building each of the request types.
        /// </summary>
        [Fact]
        public void BuildRequests_WithConfiguredServer_UseThatServer()
        {
            var sut = BuildService(new NominatimOptions() { Server = "https://my-server.example.com/nominatim" });

            var searchParameters = new SearchParameters() { Query = "Berlin" };
            var reverseParameters = new ReverseGeocodingParameters() { Latitude = 52.517037, Longitude = 13.388860 };
            var lookupParameters = new LookupParameters();
            lookupParameters.OsmIds.Add("N240109189");

            sut.BuildSearchRequest(searchParameters).AbsoluteUri
                .Should().StartWith("https://my-server.example.com/nominatim/search?");
            sut.BuildReverseGeocodeRequest(reverseParameters).AbsoluteUri
                .Should().StartWith("https://my-server.example.com/nominatim/reverse?");
            sut.BuildLookupRequest(lookupParameters).AbsoluteUri
                .Should().StartWith("https://my-server.example.com/nominatim/lookup?");
        }

        /// <summary>
        /// Tests no email parameter is sent when no email has been configured or supplied.
        /// </summary>
        [Fact]
        public void AddEmail_WithNoEmailConfigured_DoesNotAddTheParameter()
        {
            var sut = BuildService(new NominatimOptions() { Email = null });

            var query = QueryString.Empty;
            sut.AddEmail(new SearchParameters(), ref query);

            query.HasValue.Should().BeFalse();
        }

        /// <summary>
        /// Tests no email parameter is present on a built request when no email has been configured.
        /// </summary>
        [Fact]
        public void BuildSearchRequest_WithNoEmailConfigured_DoesNotAddTheParameter()
        {
            var sut = BuildService(new NominatimOptions() { Email = null });

            var uri = sut.BuildSearchRequest(new SearchParameters() { Query = "Berlin" });

            HttpUtility.UrlDecode(uri.PathAndQuery).Should().NotContain("email=");
        }

        /// <summary>
        /// Tests the zoom parameter is omitted when no zoom is supplied.
        /// </summary>
        [Fact]
        public void BuildReverseGeocodeRequest_WithNoZoom_DoesNotAddTheParameter()
        {
            var sut = BuildService();

            var uri = sut.BuildReverseGeocodeRequest(new ReverseGeocodingParameters()
            {
                Latitude = 52.517037,
                Longitude = 13.388860,
                Zoom = null,
            });

            HttpUtility.UrlDecode(uri.PathAndQuery).Should().NotContain("zoom=");
        }

        /// <summary>
        /// Tests the additional parameters are added to each of the request types.
        /// </summary>
        [Fact]
        public void BuildRequests_WithAdditionalParameters_AddThemToTheQueryString()
        {
            var sut = BuildService();

            var searchParameters = new SearchParameters() { Query = "Berlin" };
            searchParameters.AdditionalParameters.Add("customKey1", "customValue1");

            var reverseParameters = new ReverseGeocodingParameters() { Latitude = 52.517037, Longitude = 13.388860 };
            reverseParameters.AdditionalParameters.Add("customKey2", "customValue2");

            var lookupParameters = new LookupParameters();
            lookupParameters.OsmIds.Add("N240109189");
            lookupParameters.AdditionalParameters.Add("customKey3", "customValue3");

            HttpUtility.UrlDecode(sut.BuildSearchRequest(searchParameters).PathAndQuery)
                .Should().Contain("customKey1=customValue1");
            HttpUtility.UrlDecode(sut.BuildReverseGeocodeRequest(reverseParameters).PathAndQuery)
                .Should().Contain("customKey2=customValue2");
            HttpUtility.UrlDecode(sut.BuildLookupRequest(lookupParameters).PathAndQuery)
                .Should().Contain("customKey3=customValue3");
        }

        /// <summary>
        /// Tests the configured user agent is applied to the http client.
        /// </summary>
        [Fact]
        public void AddUserAgent_WithConfiguredUserAgent_SetsTheHeader()
        {
            using (var client = new HttpClient(new Mock<HttpMessageHandler>().Object))
            {
                var options = new Mock<IOptions<NominatimOptions>>();
                options.Setup(x => x.Value).Returns(new NominatimOptions() { UserAgent = "MyApplication/1.0" });

                _ = new NominatimGeocoding(client, options.Object);

                client.DefaultRequestHeaders.UserAgent.ToString().Should().Be("MyApplication/1.0");
            }
        }

        /// <summary>
        /// Tests a user agent already set on the http client is not overwritten.
        /// </summary>
        [Fact]
        public void AddUserAgent_WithUserAgentAlreadyOnTheClient_DoesNotOverwriteIt()
        {
            using (var client = new HttpClient(new Mock<HttpMessageHandler>().Object))
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "ConfiguredElsewhere/2.0");

                var options = new Mock<IOptions<NominatimOptions>>();
                options.Setup(x => x.Value).Returns(new NominatimOptions() { UserAgent = "MyApplication/1.0" });

                _ = new NominatimGeocoding(client, options.Object);

                client.DefaultRequestHeaders.UserAgent.ToString().Should().Be("ConfiguredElsewhere/2.0");
            }
        }

        /// <summary>
        /// Tests no user agent is set on the http client when none is configured.
        /// </summary>
        [Fact]
        public void AddUserAgent_WithNoConfiguredUserAgent_LeavesTheHeaderUnset()
        {
            using (var client = new HttpClient(new Mock<HttpMessageHandler>().Object))
            {
                var options = new Mock<IOptions<NominatimOptions>>();
                options.Setup(x => x.Value).Returns(new NominatimOptions() { UserAgent = null });

                _ = new NominatimGeocoding(client, options.Object);

                client.DefaultRequestHeaders.UserAgent.Count.Should().Be(0);
            }
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
                _httpClient?.Dispose();

                foreach (var message in _responseMessages)
                {
                    message?.Dispose();
                }
            }

            _disposed = true;
        }

        private NominatimGeocoding BuildService()
        {
            return new NominatimGeocoding(_httpClient, _options.Object);
        }

        private NominatimGeocoding BuildService(NominatimOptions options)
        {
            var serviceOptions = new Mock<IOptions<NominatimOptions>>();
            serviceOptions.Setup(x => x.Value).Returns(options);

            return new NominatimGeocoding(_httpClient, serviceOptions.Object);
        }
    }
}
