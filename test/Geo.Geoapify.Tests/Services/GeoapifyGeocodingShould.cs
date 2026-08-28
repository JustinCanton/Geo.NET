// <copyright file="GeoapifyGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Tests.Services
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
    using Geo.Geoapify.Enums;
    using Geo.Geoapify.Models;
    using Geo.Geoapify.Models.Parameters;
    using Geo.Geoapify.Models.Parameters.Biases;
    using Geo.Geoapify.Models.Parameters.Filters;
    using Geo.Geoapify.Services;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="GeoapifyGeocoding"/> class.
    /// </summary>
    public class GeoapifyGeocodingShould : IDisposable
    {
        private const string GeocodeJson =
            "{\"type\":\"FeatureCollection\"," +
            "\"features\":[{\"type\":\"Feature\"," +
            "\"properties\":{" +
            "\"datasource\":{\"sourcename\":\"openstreetmap\",\"attribution\":\"© OpenStreetMap contributors\"," +
            "\"license\":\"Open Database License\",\"url\":\"https://www.openstreetmap.org/copyright\"}," +
            "\"name\":\"Geoapify\",\"country\":\"Germany\",\"country_code\":\"de\",\"state\":\"North Rhine-Westphalia\"," +
            "\"county\":\"Düsseldorf\",\"city\":\"Düsseldorf\",\"postcode\":\"40213\",\"suburb\":\"Altstadt\"," +
            "\"street\":\"Marktplatz\",\"housenumber\":\"2\",\"lon\":6.7189169,\"lat\":51.2170966," +
            "\"formatted\":\"Marktplatz 2, 40213 Düsseldorf, Germany\"," +
            "\"address_line1\":\"Marktplatz 2\",\"address_line2\":\"40213 Düsseldorf, Germany\"," +
            "\"category\":\"building.historic\",\"result_type\":\"building\",\"distance\":12.5," +
            "\"rank\":{\"importance\":0.7,\"popularity\":8.9,\"confidence\":1,\"confidence_city_level\":1," +
            "\"confidence_street_level\":1,\"confidence_building_level\":1,\"match_type\":\"full_match\"}," +
            "\"timezone\":{\"name\":\"Europe/Berlin\",\"offset_STD\":\"+01:00\",\"offset_STD_seconds\":3600," +
            "\"offset_DST\":\"+02:00\",\"offset_DST_seconds\":7200,\"abbreviation_STD\":\"CET\",\"abbreviation_DST\":\"CEST\"}," +
            "\"plus_code\":\"9F38621R+2X\",\"place_id\":\"51f0ad74a3b7c21a40590c72c1a5a35d4640f00101f901e4e2050000000000\"}," +
            "\"geometry\":{\"type\":\"Point\",\"coordinates\":[6.7189169,51.2170966]}," +
            "\"bbox\":[6.718,51.216,6.719,51.218]}]," +
            "\"query\":{\"text\":\"Marktplatz 2, Düsseldorf\"," +
            "\"parsed\":{\"housenumber\":\"2\",\"street\":\"marktplatz\",\"city\":\"düsseldorf\",\"expected_type\":\"building\"}}}";

        private readonly HttpClient _httpClient;
        private readonly Mock<IOptions<KeyOptions<IGeoapifyGeocoding>>> _options = new Mock<IOptions<KeyOptions<IGeoapifyGeocoding>>>();
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoapifyGeocodingShould"/> class.
        /// </summary>
        public GeoapifyGeocodingShould()
        {
            _options
                .Setup(x => x.Value)
                .Returns(new KeyOptions<IGeoapifyGeocoding>()
                {
                    Key = "abc123",
                });

            var mockHandler = new Mock<HttpMessageHandler>();

            foreach (var path in new[] { "/v1/geocode/search", "/v1/geocode/reverse", "/v1/geocode/autocomplete" })
            {
                _responseMessages.Add(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(GeocodeJson),
                });

                var response = _responseMessages[_responseMessages.Count - 1];

                mockHandler
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsolutePath.Equals(path, StringComparison.Ordinal)),
                        ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(response);
            }

            _httpClient = new HttpClient(mockHandler.Object);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void AddGeoapifyKey_WithOptions_SuccessfullyAddsKey()
        {
            var sut = BuildService();
            var query = QueryString.Empty;

            sut.AddGeoapifyKey(new GeocodingParameters(), ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["apiKey"].Should().Be("abc123");
        }

        [Fact]
        public void AddGeoapifyKey_WithParameterOverride_SuccessfullyAddsKey()
        {
            var sut = BuildService();
            var query = QueryString.Empty;

            sut.AddGeoapifyKey(new GeocodingParameters() { Key = "override123" }, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["apiKey"].Should().Be("override123");
        }

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void AddBaseParametersSuccessfully(CultureInfo culture)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();
            var query = QueryString.Empty;

            var parameters = new GeocodingParameters()
            {
                Text = "Marktplatz 2",
                Type = LocationType.Amenity,
                Language = "de",
                Limit = 20,
            };

            parameters.Filters.Add(new CircleFilter()
            {
                Centre = new Coordinate() { Latitude = 51.2170966, Longitude = 6.7189169 },
                Radius = 5000,
            });

            var countryCodeFilter = new CountryCodeFilter();
            countryCodeFilter.CountryCodes.Add("de");
            countryCodeFilter.CountryCodes.Add("at");
            parameters.Filters.Add(countryCodeFilter);

            parameters.Biases.Add(new ProximityBias()
            {
                Coordinate = new Coordinate() { Latitude = 51.2170966, Longitude = 6.7189169 },
            });

            sut.AddBaseParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters["type"].Should().Be("amenity");
            queryParameters["lang"].Should().Be("de");
            queryParameters["limit"].Should().Be("20");
            queryParameters["filter"].Should().Be("circle:6.7189169,51.2170966,5000|countrycode:de,at");
            queryParameters["bias"].Should().Be("proximity:6.7189169,51.2170966");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void AddBaseParameters_WithNoOptionalParameters_AddsNothing()
        {
            var sut = BuildService();
            var query = QueryString.Empty;

            sut.AddBaseParameters(new GeocodingParameters() { Text = "Marktplatz 2" }, ref query);

            query.HasValue.Should().BeFalse();
        }

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildGeocodingRequestSuccessfully(CultureInfo culture)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new GeocodingParameters()
            {
                Text = "Marktplatz 2, Düsseldorf",
                Type = LocationType.Street,
                Language = "de",
                Limit = 5,
            };

            parameters.Biases.Add(new RectangleBias()
            {
                BoundingBox = new BoundingBox()
                {
                    Longitude1 = 6.7,
                    Latitude1 = 51.2,
                    Longitude2 = 6.8,
                    Latitude2 = 51.3,
                },
            });

            var uri = sut.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            uri.AbsolutePath.Should().Be("/v1/geocode/search");
            query.Should().Contain("text=Marktplatz 2, Düsseldorf");
            query.Should().Contain("type=street");
            query.Should().Contain("lang=de");
            query.Should().Contain("limit=5");
            query.Should().Contain("bias=rect:6.7,51.2,6.8,51.3");
            query.Should().Contain("apiKey=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildGeocodingRequest_WithStructuredAddress_AddsAllComponents()
        {
            var sut = BuildService();

            var parameters = new GeocodingParameters()
            {
                Name = "Geoapify",
                HouseNumber = "2",
                Street = "Marktplatz",
                PostCode = "40213",
                City = "Düsseldorf",
                State = "North Rhine-Westphalia",
                Country = "Germany",
            };

            var uri = sut.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("name=Geoapify");
            query.Should().Contain("housenumber=2");
            query.Should().Contain("street=Marktplatz");
            query.Should().Contain("postcode=40213");
            query.Should().Contain("city=Düsseldorf");
            query.Should().Contain("state=North Rhine-Westphalia");
            query.Should().Contain("country=Germany");
            query.Should().NotContain("text=");
        }

        [Fact]
        public void BuildGeocodingRequest_WithAdditionalParameters_AddsThemToQueryString()
        {
            var sut = BuildService();

            var parameters = new GeocodingParameters() { Text = "Marktplatz 2" };
            parameters.AdditionalParameters.Add("customKey1", "customValue1");
            parameters.AdditionalParameters.Add("customKey2", "customValue2");

            var uri = sut.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("customKey1=customValue1");
            query.Should().Contain("customKey2=customValue2");
        }

        [Fact]
        public void BuildGeocodingRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildGeocodingRequest(new GeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Text')");
#else
                .WithMessage("*Parameter name: Text");
#endif
        }

        [Fact]
        public void ValidateAndCraftGeocodingUri_WithNullParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<GeocodingParameters>(null, sut.BuildGeocodingRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithMessage("*See the inner exception for more information.")
                .WithInnerException<ArgumentNullException>();
        }

        [Fact]
        public void ValidateAndCraftGeocodingUri_WithInvalidParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<GeocodingParameters>(new GeocodingParameters(), sut.BuildGeocodingRequest);

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
        public async Task GeocodingAsyncSuccessfully()
        {
            var sut = BuildService();

            var result = await sut.GeocodingAsync(new GeocodingParameters()
            {
                Text = "Marktplatz 2, Düsseldorf",
                Limit = 1,
            });

            result.Type.Should().Be("FeatureCollection");
            result.Features.Count.Should().Be(1);

            var properties = result.Features[0].Properties;
            properties.Name.Should().Be("Geoapify");
            properties.Country.Should().Be("Germany");
            properties.CountryCode.Should().Be("de");
            properties.City.Should().Be("Düsseldorf");
            properties.PostCode.Should().Be("40213");
            properties.Street.Should().Be("Marktplatz");
            properties.HouseNumber.Should().Be("2");
            properties.Formatted.Should().Be("Marktplatz 2, 40213 Düsseldorf, Germany");
            properties.ResultType.Should().Be("building");
            properties.Distance.Should().Be(12.5);
            properties.PlaceId.Should().Be("51f0ad74a3b7c21a40590c72c1a5a35d4640f00101f901e4e2050000000000");
            properties.PlusCode.Should().Be("9F38621R+2X");
            properties.DataSource.SourceName.Should().Be("openstreetmap");
            properties.Rank.Confidence.Should().Be(1);
            properties.Rank.MatchType.Should().Be("full_match");
            properties.TimeZone.Name.Should().Be("Europe/Berlin");
            properties.TimeZone.OffsetStandardSeconds.Should().Be(3600);
            properties.TimeZone.AbbreviationDaylightSaving.Should().Be("CEST");

            result.Features[0].Geometry.Coordinates.Latitude.Should().Be(51.2170966);
            result.Features[0].Geometry.Coordinates.Longitude.Should().Be(6.7189169);
            result.Features[0].BoundingBox.Count.Should().Be(4);

            result.Query.Text.Should().Be("Marktplatz 2, Düsseldorf");
            result.Query.Parsed.HouseNumber.Should().Be("2");
            result.Query.Parsed.Street.Should().Be("marktplatz");
            result.Query.Parsed.ExpectedType.Should().Be("building");
        }

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildReverseGeocodingRequestSuccessfully(CultureInfo culture)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate() { Latitude = 51.2170966, Longitude = 6.7189169 },
            };

            var uri = sut.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            uri.AbsolutePath.Should().Be("/v1/geocode/reverse");
            query.Should().Contain("lat=51.2170966");
            query.Should().Contain("lon=6.7189169");
            query.Should().Contain("apiKey=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildReverseGeocodingRequest_WithOptionalParameters_AddsThemToQueryString()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate() { Latitude = 51.2170966, Longitude = 6.7189169 },
                Type = LocationType.City,
                Language = "fr",
                Limit = 10,
            };

            var uri = sut.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("type=city");
            query.Should().Contain("lang=fr");
            query.Should().Contain("limit=10");
        }

        [Fact]
        public void BuildReverseGeocodingRequest_WithAdditionalParameters_AddsThemToQueryString()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate() { Latitude = 51.2170966, Longitude = 6.7189169 },
            };

            parameters.AdditionalParameters.Add("customKey1", "customValue1");

            var uri = sut.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("customKey1=customValue1");
        }

        [Fact]
        public void BuildReverseGeocodingRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildReverseGeocodingRequest(new ReverseGeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Coordinate')");
#else
                .WithMessage("*Parameter name: Coordinate");
#endif
        }

        [Fact]
        public void ValidateAndCraftReverseGeocodingUri_WithNullParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<ReverseGeocodingParameters>(null, sut.BuildReverseGeocodingRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithInnerException<ArgumentNullException>();
        }

        [Fact]
        public void ValidateAndCraftReverseGeocodingUri_WithInvalidParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<ReverseGeocodingParameters>(new ReverseGeocodingParameters(), sut.BuildReverseGeocodingRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithInnerException<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Coordinate')");
#else
                .WithMessage("*Parameter name: Coordinate");
#endif
        }

        [Fact]
        public async Task ReverseGeocodingAsyncSuccessfully()
        {
            var sut = BuildService();

            var result = await sut.ReverseGeocodingAsync(new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate() { Latitude = 51.2170966, Longitude = 6.7189169 },
            });

            result.Features.Count.Should().Be(1);
            result.Features[0].Properties.Formatted.Should().Be("Marktplatz 2, 40213 Düsseldorf, Germany");
            result.Features[0].Geometry.Coordinates.Latitude.Should().Be(51.2170966);
            result.Features[0].Geometry.Coordinates.Longitude.Should().Be(6.7189169);
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
                Text = "Marktpl",
                Type = LocationType.Street,
                Limit = 8,
            };

            parameters.Filters.Add(new RectangleFilter()
            {
                BoundingBox = new BoundingBox()
                {
                    Longitude1 = 6.7,
                    Latitude1 = 51.2,
                    Longitude2 = 6.8,
                    Latitude2 = 51.3,
                },
            });

            parameters.Biases.Add(new CircleBias()
            {
                Centre = new Coordinate() { Latitude = 51.2170966, Longitude = 6.7189169 },
                Radius = 1500,
            });

            var uri = sut.BuildAutocompleteRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            uri.AbsolutePath.Should().Be("/v1/geocode/autocomplete");
            query.Should().Contain("text=Marktpl");
            query.Should().Contain("type=street");
            query.Should().Contain("limit=8");
            query.Should().Contain("filter=rect:6.7,51.2,6.8,51.3");
            query.Should().Contain("bias=circle:6.7189169,51.2170966,1500");
            query.Should().Contain("apiKey=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildAutocompleteRequest_WithAdditionalParameters_AddsThemToQueryString()
        {
            var sut = BuildService();

            var parameters = new AutocompleteParameters() { Text = "Marktpl" };
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

            var result = await sut.AutocompleteAsync(new AutocompleteParameters() { Text = "Marktpl" });

            result.Features.Count.Should().Be(1);
            result.Features[0].Properties.Street.Should().Be("Marktplatz");
        }

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void FiltersAreCultureInvariant(CultureInfo culture)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var circle = new CircleFilter()
            {
                Centre = new Coordinate() { Latitude = 41.878968, Longitude = -87.770231 },
                Radius = 5000.5,
            };

            var rectangle = new RectangleFilter()
            {
                BoundingBox = new BoundingBox()
                {
                    Longitude1 = -89.09754,
                    Latitude1 = 39.668983,
                    Longitude2 = -88.399274,
                    Latitude2 = 40.383412,
                },
            };

            var place = new PlaceFilter() { PlaceId = "51f07665660fc4024059dc0a96dfac6c" };
            var geometry = new GeometryFilter() { GeometryId = "d3046cf911d4d8bb8dd99394ef0921fb" };

            circle.ToString().Should().Be("circle:-87.770231,41.878968,5000.5");
            rectangle.ToString().Should().Be("rect:-89.09754,39.668983,-88.399274,40.383412");
            place.ToString().Should().Be("place:51f07665660fc4024059dc0a96dfac6c");
            geometry.ToString().Should().Be("geometry:d3046cf911d4d8bb8dd99394ef0921fb");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BiasesAreCultureInvariant(CultureInfo culture)
        {
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var proximity = new ProximityBias()
            {
                Coordinate = new Coordinate() { Latitude = 52.971411, Longitude = 41.2257145 },
            };

            var circle = new CircleBias()
            {
                Centre = new Coordinate() { Latitude = 40.77, Longitude = -73.99 },
                Radius = 1500,
            };

            var countryCode = new CountryCodeBias();
            countryCode.CountryCodes.Add("us");
            countryCode.CountryCodes.Add("ca");

            proximity.ToString().Should().Be("proximity:41.2257145,52.971411");
            circle.ToString().Should().Be("circle:-73.99,40.77,1500");
            countryCode.ToString().Should().Be("countrycode:us,ca");

            Thread.CurrentThread.CurrentCulture = oldCulture;
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

        private GeoapifyGeocoding BuildService()
        {
            return new GeoapifyGeocoding(_httpClient, _options.Object);
        }
    }
}
