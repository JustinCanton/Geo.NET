// <copyright file="OpenRouteServiceGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Tests.Services
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
    using Geo.OpenRouteService.Enums;
    using Geo.OpenRouteService.Models;
    using Geo.OpenRouteService.Models.Parameters;
    using Geo.OpenRouteService.Services;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="OpenRouteServiceGeocoding"/> class.
    /// </summary>
    public class OpenRouteServiceGeocodingShould : IDisposable
    {
        private const string SearchJson =
            "{\"type\":\"FeatureCollection\"," +
            "\"features\":[{\"type\":\"Feature\"," +
            "\"geometry\":{\"type\":\"Point\",\"coordinates\":[8.68417,49.41461]}," +
            "\"properties\":{\"id\":\"node:6863027853\",\"gid\":\"osm:venue:node:6863027853\"," +
            "\"layer\":\"venue\",\"source\":\"osm\",\"source_id\":\"node:6863027853\"," +
            "\"name\":\"Heidelberg\",\"label\":\"Heidelberg, Baden-Württemberg, Germany\"," +
            "\"confidence\":1.0,\"distance\":0.0,\"match_type\":\"exact\",\"accuracy\":\"point\"," +
            "\"country\":\"Germany\",\"country_code\":\"DE\",\"region\":\"Baden-Württemberg\"," +
            "\"locality\":\"Heidelberg\"}}]," +
            "\"bbox\":[8.572,49.351,8.797,49.470]," +
            "\"geocoding\":{\"version\":\"0.2\",\"attribution\":\"https://openrouteservice.org/\",\"warnings\":[]}}";

        private readonly HttpClient _httpClient;
        private readonly Mock<IOptions<KeyOptions<IOpenRouteServiceGeocoding>>> _options = new Mock<IOptions<KeyOptions<IOpenRouteServiceGeocoding>>>();
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenRouteServiceGeocodingShould"/> class.
        /// </summary>
        public OpenRouteServiceGeocodingShould()
        {
            _options
                .Setup(x => x.Value)
                .Returns(new KeyOptions<IOpenRouteServiceGeocoding>()
                {
                    Key = "abc123",
                });

            var mockHandler = new Mock<HttpMessageHandler>();

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(SearchJson),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsolutePath.Contains("/geocode/search/structured")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(SearchJson),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsolutePath.Contains("/geocode/autocomplete")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(SearchJson),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsolutePath.Contains("/geocode/reverse")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(SearchJson),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsolutePath.Contains("/geocode/search") && !x.RequestUri.AbsolutePath.Contains("structured")),
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
        public void AddOpenRouteServiceKey_WithOptions_SuccessfullyAddsKey()
        {
            var sut = BuildService();
            var query = QueryString.Empty;

            sut.AddOpenRouteServiceKey(new SearchParameters(), ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["api_key"].Should().Be("abc123");
        }

        [Fact]
        public void AddOpenRouteServiceKey_WithParameterOverride_SuccessfullyAddsKey()
        {
            var sut = BuildService();
            var query = QueryString.Empty;

            sut.AddOpenRouteServiceKey(new SearchParameters() { Key = "override123" }, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["api_key"].Should().Be("override123");
        }

        [Fact]
        public void AddBaseParametersSuccessfully()
        {
            var sut = BuildService();
            var query = QueryString.Empty;

            var parameters = new SearchParameters()
            {
                Text = "Heidelberg",
                Size = 5,
                FocusPoint = new Coordinate() { Latitude = 49.41, Longitude = 8.68 },
                BoundaryRect = new BoundingBox()
                {
                    MinLongitude = 7.0,
                    MaxLongitude = 10.0,
                    MinLatitude = 48.0,
                    MaxLatitude = 51.0,
                },
                BoundaryCircle = new BoundingCircle()
                {
                    Latitude = 49.41,
                    Longitude = 8.68,
                    Radius = 50.0,
                },
                BoundaryGid = "whosonfirst:region:85682571",
            };

            parameters.BoundaryCountries.Add("DE");
            parameters.BoundaryCountries.Add("AT");
            parameters.Sources.Add(SourceType.Osm);
            parameters.Sources.Add(SourceType.Gn);
            parameters.Layers.Add(LayerType.Locality);
            parameters.Layers.Add(LayerType.Address);

            sut.AddBaseParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters["size"].Should().Be("5");
            queryParameters["focus.point.lat"].Should().Be("49.41");
            queryParameters["focus.point.lon"].Should().Be("8.68");
            queryParameters["boundary.rect.min_lon"].Should().Be("7");
            queryParameters["boundary.rect.max_lon"].Should().Be("10");
            queryParameters["boundary.rect.min_lat"].Should().Be("48");
            queryParameters["boundary.rect.max_lat"].Should().Be("51");
            queryParameters["boundary.circle.lat"].Should().Be("49.41");
            queryParameters["boundary.circle.lon"].Should().Be("8.68");
            queryParameters["boundary.circle.radius"].Should().Be("50");
            queryParameters["boundary.country"].Should().Be("DE,AT");
            queryParameters["boundary.gid"].Should().Be("whosonfirst:region:85682571");
            queryParameters["sources"].Should().Be("osm,gn");
            queryParameters["layers"].Should().Be("locality,address");
        }

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildSearchRequestSuccessfully(CultureInfo culture)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new SearchParameters()
            {
                Text = "Heidelberg",
                Size = 10,
                FocusPoint = new Coordinate() { Latitude = 49.41461, Longitude = 8.68417 },
                BoundaryRect = new BoundingBox()
                {
                    MinLongitude = 7.5,
                    MaxLongitude = 9.5,
                    MinLatitude = 48.5,
                    MaxLatitude = 50.5,
                },
                BoundaryGid = "whosonfirst:region:85682571",
            };

            parameters.BoundaryCountries.Add("DE");
            parameters.Sources.Add(SourceType.Osm);
            parameters.Layers.Add(LayerType.Locality);

            var uri = sut.BuildSearchRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("text=Heidelberg");
            query.Should().Contain("size=10");
            query.Should().Contain("focus.point.lat=49.41461");
            query.Should().Contain("focus.point.lon=8.68417");
            query.Should().Contain("boundary.rect.min_lon=7.5");
            query.Should().Contain("boundary.rect.max_lon=9.5");
            query.Should().Contain("boundary.rect.min_lat=48.5");
            query.Should().Contain("boundary.rect.max_lat=50.5");
            query.Should().Contain("boundary.country=DE");
            query.Should().Contain("boundary.gid=whosonfirst:region:85682571");
            query.Should().Contain("sources=osm");
            query.Should().Contain("layers=locality");
            query.Should().Contain("api_key=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildSearchRequest_WithAdditionalParameters_AddsThemToQueryString()
        {
            var sut = BuildService();

            var parameters = new SearchParameters() { Text = "Heidelberg" };
            parameters.AdditionalParameters.Add("customKey1", "customValue1");
            parameters.AdditionalParameters.Add("customKey2", "customValue2");

            var uri = sut.BuildSearchRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("customKey1=customValue1");
            query.Should().Contain("customKey2=customValue2");
        }

        [Fact]
        public void BuildSearchRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildSearchRequest(new SearchParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Text')");
#else
                .WithMessage("*Parameter name: Text");
#endif
        }

        [Fact]
        public void ValidateAndCraftSearchUri_WithNullParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<SearchParameters>(null, sut.BuildSearchRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithMessage("*See the inner exception for more information.")
                .WithInnerException<ArgumentNullException>();
        }

        [Fact]
        public void ValidateAndCraftSearchUri_WithInvalidParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<SearchParameters>(new SearchParameters(), sut.BuildSearchRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithMessage("*See the inner exception for more information.")
                .WithInnerException<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Text')");
#else
                .WithMessage("*Parameter name: Text");
#endif
        }

        [Fact]
        public async Task SearchAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new SearchParameters()
            {
                Text = "Heidelberg",
                Size = 5,
            };

            parameters.BoundaryCountries.Add("DE");

            var result = await sut.SearchAsync(parameters);

            result.Features.Count.Should().Be(1);
            result.Features[0].Properties.Name.Should().Be("Heidelberg");
            result.Features[0].Properties.Country.Should().Be("Germany");
            result.Features[0].Geometry.Coordinates.Latitude.Should().Be(49.41461);
            result.Features[0].Geometry.Coordinates.Longitude.Should().Be(8.68417);
        }

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildAutocompleteRequestSuccessfully(CultureInfo culture)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new AutocompleteParameters()
            {
                Text = "Heidel",
                Size = 5,
                FocusPoint = new Coordinate() { Latitude = 49.41461, Longitude = 8.68417 },
            };

            parameters.BoundaryCountries.Add("DE");
            parameters.Layers.Add(LayerType.Locality);

            var uri = sut.BuildAutocompleteRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("text=Heidel");
            query.Should().Contain("size=5");
            query.Should().Contain("focus.point.lat=49.41461");
            query.Should().Contain("focus.point.lon=8.68417");
            query.Should().Contain("boundary.country=DE");
            query.Should().Contain("layers=locality");
            query.Should().Contain("api_key=abc123");

            var path = uri.AbsolutePath;
            path.Should().Contain("/geocode/autocomplete");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildAutocompleteRequest_WithAdditionalParameters_AddsThemToQueryString()
        {
            var sut = BuildService();

            var parameters = new AutocompleteParameters() { Text = "Heidel" };
            parameters.AdditionalParameters.Add("customKey1", "customValue1");

            var uri = sut.BuildAutocompleteRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("customKey1=customValue1");
        }

        [Fact]
        public void BuildAutocompleteRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildAutocompleteRequest(new AutocompleteParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Text')");
#else
                .WithMessage("*Parameter name: Text");
#endif
        }

        [Fact]
        public void ValidateAndCraftAutocompleteUri_WithNullParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<AutocompleteParameters>(null, sut.BuildAutocompleteRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithInnerException<ArgumentNullException>();
        }

        [Fact]
        public void ValidateAndCraftAutocompleteUri_WithInvalidParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<AutocompleteParameters>(new AutocompleteParameters(), sut.BuildAutocompleteRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithInnerException<ArgumentException>();
        }

        [Fact]
        public async Task AutocompleteAsyncSuccessfully()
        {
            var sut = BuildService();

            var result = await sut.AutocompleteAsync(new AutocompleteParameters() { Text = "Heidel" });

            result.Features.Count.Should().Be(1);
            result.Features[0].Properties.Name.Should().Be("Heidelberg");
        }

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildStructuredSearchRequestSuccessfully(CultureInfo culture)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new StructuredSearchParameters()
            {
                Address = "Hauptstraße 1",
                Locality = "Heidelberg",
                Region = "Baden-Württemberg",
                PostalCode = "69117",
                Country = "DE",
                Size = 3,
                FocusPoint = new Coordinate() { Latitude = 49.41461, Longitude = 8.68417 },
            };

            var uri = sut.BuildStructuredSearchRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("address=Hauptstraße 1");
            query.Should().Contain("locality=Heidelberg");
            query.Should().Contain("region=Baden-Württemberg");
            query.Should().Contain("postalcode=69117");
            query.Should().Contain("country=DE");
            query.Should().Contain("size=3");
            query.Should().Contain("focus.point.lat=49.41461");
            query.Should().Contain("focus.point.lon=8.68417");
            query.Should().Contain("api_key=abc123");

            var path = uri.AbsolutePath;
            path.Should().Contain("/geocode/search/structured");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildStructuredSearchRequest_WithAdditionalParameters_AddsThemToQueryString()
        {
            var sut = BuildService();

            var parameters = new StructuredSearchParameters() { Locality = "Heidelberg" };
            parameters.AdditionalParameters.Add("customKey1", "customValue1");

            var uri = sut.BuildStructuredSearchRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("customKey1=customValue1");
        }

        [Fact]
        public void BuildStructuredSearchRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildStructuredSearchRequest(new StructuredSearchParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Address')");
#else
                .WithMessage("*Parameter name: Address");
#endif
        }

        [Fact]
        public void ValidateAndCraftStructuredSearchUri_WithNullParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<StructuredSearchParameters>(null, sut.BuildStructuredSearchRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithInnerException<ArgumentNullException>();
        }

        [Fact]
        public void ValidateAndCraftStructuredSearchUri_WithInvalidParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<StructuredSearchParameters>(new StructuredSearchParameters(), sut.BuildStructuredSearchRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithInnerException<ArgumentException>();
        }

        [Fact]
        public async Task StructuredSearchAsyncSuccessfully()
        {
            var sut = BuildService();

            var result = await sut.StructuredSearchAsync(new StructuredSearchParameters() { Locality = "Heidelberg", Country = "DE" });

            result.Features.Count.Should().Be(1);
            result.Features[0].Properties.Locality.Should().Be("Heidelberg");
        }

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildReverseRequestSuccessfully(CultureInfo culture)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new ReverseSearchParameters()
            {
                Point = new Coordinate() { Latitude = 49.41461, Longitude = 8.68417 },
                Size = 5,
                BoundaryCircleRadius = 1.5,
                BoundaryGid = "whosonfirst:region:85682571",
            };

            parameters.BoundaryCountries.Add("DE");
            parameters.Sources.Add(SourceType.Osm);
            parameters.Layers.Add(LayerType.Address);

            var uri = sut.BuildReverseRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("point.lat=49.41461");
            query.Should().Contain("point.lon=8.68417");
            query.Should().Contain("size=5");
            query.Should().Contain("boundary.circle.radius=1.5");
            query.Should().Contain("boundary.country=DE");
            query.Should().Contain("boundary.gid=whosonfirst:region:85682571");
            query.Should().Contain("sources=osm");
            query.Should().Contain("layers=address");
            query.Should().Contain("api_key=abc123");

            var path = uri.AbsolutePath;
            path.Should().Contain("/geocode/reverse");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildReverseRequest_WithAdditionalParameters_AddsThemToQueryString()
        {
            var sut = BuildService();

            var parameters = new ReverseSearchParameters()
            {
                Point = new Coordinate() { Latitude = 49.41461, Longitude = 8.68417 },
            };

            parameters.AdditionalParameters.Add("customKey1", "customValue1");
            parameters.AdditionalParameters.Add("customKey2", "customValue2");

            var uri = sut.BuildReverseRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("customKey1=customValue1");
            query.Should().Contain("customKey2=customValue2");
        }

        [Fact]
        public void BuildReverseRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildReverseRequest(new ReverseSearchParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Point')");
#else
                .WithMessage("*Parameter name: Point");
#endif
        }

        [Fact]
        public void ValidateAndCraftReverseUri_WithNullParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<ReverseSearchParameters>(null, sut.BuildReverseRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithInnerException<ArgumentNullException>();
        }

        [Fact]
        public void ValidateAndCraftReverseUri_WithInvalidParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<ReverseSearchParameters>(new ReverseSearchParameters(), sut.BuildReverseRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithInnerException<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Point')");
#else
                .WithMessage("*Parameter name: Point");
#endif
        }

        [Fact]
        public async Task ReverseAsyncSuccessfully()
        {
            var sut = BuildService();

            var result = await sut.ReverseAsync(new ReverseSearchParameters()
            {
                Point = new Coordinate() { Latitude = 49.41461, Longitude = 8.68417 },
            });

            result.Features.Count.Should().Be(1);
            result.Features[0].Properties.Name.Should().Be("Heidelberg");
            result.Features[0].Geometry.Coordinates.Latitude.Should().Be(49.41461);
            result.Features[0].Geometry.Coordinates.Longitude.Should().Be(8.68417);
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

        private OpenRouteServiceGeocoding BuildService()
        {
            return new OpenRouteServiceGeocoding(_httpClient, _options.Object);
        }
    }
}
