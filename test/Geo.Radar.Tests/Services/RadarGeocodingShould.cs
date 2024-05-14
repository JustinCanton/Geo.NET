// <copyright file="RadarGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar.Tests.Services
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
    using Geo.Radar.Models;
    using Geo.Radar.Models.Parameters;
    using Geo.Radar.Services;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="RadarGeocoding"/> class.
    /// </summary>
    public class RadarGeocodingShould : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Mock<IOptions<KeyOptions<IRadarGeocoding>>> _options = new Mock<IOptions<KeyOptions<IRadarGeocoding>>>();
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="RadarGeocodingShould"/> class.
        /// </summary>
        public RadarGeocodingShould()
        {
            _options
                .Setup(x => x.Value)
                .Returns(new KeyOptions<IRadarGeocoding>()
                {
                    Key = "abc123",
                });

            var mockHandler = new Mock<HttpMessageHandler>();

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                        "{\"meta\":{\"code\":200},\"addresses\":[{\"latitude\":47.576575,\"longitude\":16.430711,\"geometry\":{\"type\":\"Point\",\"coordinates\":[16.430711,47.576575]},\"country\":\"Austria\",\"countryCode\":\"AT\",\"countryFlag\":\"🇦🇹\",\"county\":\"Oberpullendorf\",\"distance\":6962472,\"confidence\":\"exact\",\"city\":\"Weppersdorf\",\"number\":\"123\",\"postalCode\":\"7331\",\"stateCode\":\"BU\",\"state\":\"Burgenland\",\"street\":\"Hauptstraße\",\"layer\":\"address\",\"formattedAddress\":\"123 Hauptstraße, BU 7331 AUT\",\"addressLabel\":\"Hauptstraße 123\"}]}"),
            });

            // For reverse geocoding, use the places endpoint type
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("geocode/forward")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                        "{\"meta\":{\"code\":200},\"addresses\":[{\"latitude\":47.576174,\"longitude\":16.43042,\"geometry\":{\"type\":\"Point\",\"coordinates\":[16.43042,47.576174]},\"country\":\"Austria\",\"countryCode\":\"AT\",\"countryFlag\":\"🇦🇹\",\"county\":\"Oberpullendorf\",\"distance\":50,\"city\":\"Weppersdorf\",\"number\":\"123\",\"postalCode\":\"7331\",\"stateCode\":\"BU\",\"state\":\"Burgenland\",\"street\":\"Hauptstraße\",\"layer\":\"address\",\"formattedAddress\":\"123 Hauptstraße, BU 7331 AUT\",\"addressLabel\":\"Hauptstraße 123\"}]}"),
            });

            // For reverse geocoding, use the permanent endpoint type
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("geocode/reverse")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                        "{\"meta\":{\"code\":200},\"addresses\":[{\"latitude\":43.76866,\"longitude\":-79.40616,\"geometry\":{\"type\":\"Point\",\"coordinates\":[-79.40616,43.76866]},\"country\":\"Canada\",\"countryCode\":\"CA\",\"countryFlag\":\"🇨🇦\",\"county\":\"Toronto\",\"distance\":381,\"borough\":\"North York\",\"city\":\"Toronto\",\"number\":\"123\",\"neighborhood\":\"Willowdale East\",\"postalCode\":\"M2N 3N8\",\"stateCode\":\"ON\",\"state\":\"Ontario\",\"street\":\"Hillcrest Ave\",\"layer\":\"address\",\"formattedAddress\":\"123 Hillcrest Ave, North York, Toronto, ON M2N 3N8 CAN\",\"addressLabel\":\"123 Hillcrest Ave\"},{\"latitude\":43.7678,\"longitude\":-79.40562,\"geometry\":{\"type\":\"Point\",\"coordinates\":[-79.40562,43.7678]},\"country\":\"Canada\",\"countryCode\":\"CA\",\"countryFlag\":\"🇨🇦\",\"county\":\"Toronto\",\"distance\":436,\"borough\":\"North York\",\"city\":\"Toronto\",\"number\":\"123\",\"neighborhood\":\"Willowdale East\",\"postalCode\":\"M2N 3M1\",\"stateCode\":\"ON\",\"state\":\"Ontario\",\"street\":\"Elmwood Ave\",\"layer\":\"address\",\"formattedAddress\":\"123 Elmwood Ave, North York, Toronto, ON M2N 3M1 CAN\",\"addressLabel\":\"123 Elmwood Ave\"},{\"latitude\":43.764905,\"longitude\":-79.408365,\"geometry\":{\"type\":\"Point\",\"coordinates\":[-79.408365,43.764905]},\"country\":\"Canada\",\"countryCode\":\"CA\",\"countryFlag\":\"🇨🇦\",\"county\":\"Toronto\",\"distance\":469,\"borough\":\"North York\",\"city\":\"Toronto\",\"number\":\"123\",\"neighborhood\":\"Willowdale East\",\"postalCode\":\"M2N 4T2\",\"stateCode\":\"ON\",\"state\":\"Ontario\",\"street\":\"Doris Avenue\",\"layer\":\"address\",\"formattedAddress\":\"123 Doris Avenue, North York, Toronto, ON M2N 4T2 CAN\",\"addressLabel\":\"123 Doris Avenue\"},{\"latitude\":43.766997,\"longitude\":-79.405112,\"geometry\":{\"type\":\"Point\",\"coordinates\":[-79.405112,43.766997]},\"country\":\"Canada\",\"countryCode\":\"CA\",\"countryFlag\":\"🇨🇦\",\"county\":\"Toronto\",\"distance\":502,\"borough\":\"North York\",\"city\":\"Toronto\",\"number\":\"123\",\"neighborhood\":\"Willowdale East\",\"postalCode\":\"M2N 3K2\",\"stateCode\":\"ON\",\"state\":\"Ontario\",\"street\":\"Hollywood Avenue\",\"layer\":\"address\",\"formattedAddress\":\"123 Hollywood Avenue, North York, Toronto, ON M2N 3K2 CAN\",\"addressLabel\":\"123 Hollywood Avenue\"},{\"latitude\":43.773507,\"longitude\":-79.405844,\"geometry\":{\"type\":\"Point\",\"coordinates\":[-79.405844,43.773507]},\"country\":\"Canada\",\"countryCode\":\"CA\",\"countryFlag\":\"🇨🇦\",\"county\":\"Toronto\",\"distance\":672,\"borough\":\"North York\",\"city\":\"Toronto\",\"number\":\"123\",\"neighborhood\":\"Willowdale East\",\"postalCode\":\"M2N 4A7\",\"stateCode\":\"ON\",\"state\":\"Ontario\",\"street\":\"Norton Avenue\",\"layer\":\"address\",\"formattedAddress\":\"123 Norton Avenue, North York, Toronto, ON M2N 4A7 CAN\",\"addressLabel\":\"123 Norton Avenue\"},{\"latitude\":43.76654,\"longitude\":-79.40308,\"geometry\":{\"type\":\"Point\",\"coordinates\":[-79.40308,43.76654]},\"country\":\"Canada\",\"countryCode\":\"CA\",\"countryFlag\":\"🇨🇦\",\"county\":\"Toronto\",\"distance\":673,\"borough\":\"North York\",\"city\":\"Toronto\",\"number\":\"123\",\"neighborhood\":\"Willowdale East\",\"postalCode\":\"M2N 3J1\",\"stateCode\":\"ON\",\"state\":\"Ontario\",\"street\":\"Alfred Ave\",\"layer\":\"address\",\"formattedAddress\":\"123 Alfred Ave, North York, Toronto, ON M2N 3J1 CAN\",\"addressLabel\":\"123 Alfred Ave\"},{\"latitude\":43.7752,\"longitude\":-79.41155,\"geometry\":{\"type\":\"Point\",\"coordinates\":[-79.41155,43.7752]},\"country\":\"Canada\",\"countryCode\":\"CA\",\"countryFlag\":\"🇨🇦\",\"county\":\"Toronto\",\"distance\":725,\"borough\":\"North York\",\"city\":\"Toronto\",\"number\":\"123\",\"neighborhood\":\"Willowdale East\",\"postalCode\":\"M2N 6V2\",\"stateCode\":\"ON\",\"state\":\"Ontario\",\"street\":\"Grandview Way\",\"layer\":\"address\",\"formattedAddress\":\"123 Grandview Way, North York, Toronto, ON M2N 6V2 CAN\",\"addressLabel\":\"123 Grandview Way\"},{\"latitude\":43.7753,\"longitude\":-79.40628,\"geometry\":{\"type\":\"Point\",\"coordinates\":[-79.40628,43.7753]},\"country\":\"Canada\",\"countryCode\":\"CA\",\"countryFlag\":\"🇨🇦\",\"county\":\"Toronto\",\"distance\":823,\"borough\":\"North York\",\"city\":\"Toronto\",\"number\":\"123\",\"neighborhood\":\"Willowdale East\",\"postalCode\":\"M2N 4G3\",\"stateCode\":\"ON\",\"state\":\"Ontario\",\"street\":\"Church Ave\",\"layer\":\"address\",\"formattedAddress\":\"123 Church Ave, North York, Toronto, ON M2N 4G3 CAN\",\"addressLabel\":\"123 Church Ave\"},{\"latitude\":43.76244,\"longitude\":-79.41773,\"geometry\":{\"type\":\"Point\",\"coordinates\":[-79.41773,43.76244]},\"country\":\"Canada\",\"countryCode\":\"CA\",\"countryFlag\":\"🇨🇦\",\"county\":\"Toronto\",\"distance\":887,\"borough\":\"North York\",\"city\":\"Toronto\",\"number\":\"123\",\"neighborhood\":\"Lansing-Westgate\",\"postalCode\":\"M2N 1S8\",\"stateCode\":\"ON\",\"state\":\"Ontario\",\"street\":\"Burndale Ave\",\"layer\":\"address\",\"formattedAddress\":\"123 Burndale Ave, North York, Toronto, ON M2N 1S8 CAN\",\"addressLabel\":\"123 Burndale Ave\"},{\"latitude\":43.773987,\"longitude\":-79.420541,\"geometry\":{\"type\":\"Point\",\"coordinates\":[-79.420541,43.773987]},\"country\":\"Canada\",\"countryCode\":\"CA\",\"countryFlag\":\"🇨🇦\",\"county\":\"Toronto\",\"distance\":973,\"borough\":\"North York\",\"city\":\"Toronto\",\"number\":\"123\",\"neighborhood\":\"Willowdale West\",\"postalCode\":\"M2N 2B1\",\"stateCode\":\"ON\",\"state\":\"Ontario\",\"street\":\"Hounslow Avenue\",\"layer\":\"address\",\"formattedAddress\":\"123 Hounslow Avenue, North York, Toronto, ON M2N 2B1 CAN\",\"addressLabel\":\"123 Hounslow Avenue\"}]}"),
            });

            // For reverse geocoding, use the places endpoint type
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("search/autocomplete")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            _httpClient = new HttpClient(mockHandler.Object);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void AddRadarKey_WithOptions_SuccessfullyAddsKey()
        {
            var sut = BuildService();

            sut.AddRadarKey(new GeocodingParameters());

            _httpClient.DefaultRequestHeaders.Authorization.Scheme.Should().Be("abc123");
        }

        [Fact]
        public void AddHereKey_WithParameterOverride_SuccessfullyAddsKey()
        {
            var sut = BuildService();

            sut.AddRadarKey(new GeocodingParameters() { Key = "123abc" });

            _httpClient.DefaultRequestHeaders.Authorization.Scheme.Should().Be("123abc");
        }

        [Fact]
        public void AddCountry_WithValidCountries_AddsSuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new GeocodingParameters();

            parameters.Countries.Add("CA");
            parameters.Countries.Add("FR");

            sut.AddCountry(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["country"].Should().Be("CA,FR");
        }

        [Fact]
        public void AddLayers_WithValidLayers_AddsSuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new GeocodingParameters();

            parameters.Layers.Add(Layer.PostalCode);
            parameters.Layers.Add(Layer.Country);
            parameters.Layers.Add(Layer.Coarse);

            sut.AddLayers(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["layers"].Should().Be("postalCode,country,coarse");
        }

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildGeocodingRequest_WithValidParameters_SuccessfullyBuildsUrl(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new GeocodingParameters()
            {
                Query = "123 East",
                Key = "123abc",
            };

            parameters.Layers.Add(Layer.PostalCode);
            parameters.Layers.Add(Layer.Country);

            parameters.Countries.Add("CA");

            // Act
            var uri = sut.BuildGeocodingRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("query=123 East");
            query.Should().Contain("country=CA");
            query.Should().Contain("layers=postalCode,country");

            _httpClient.DefaultRequestHeaders.Authorization.Scheme.Should().Be("123abc");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildGeocodingRequest_WithCharacterNeedingEncoding_SuccessfullyBuildsAnEncodedUrl()
        {
            var sut = BuildService();

            var parameters = new GeocodingParameters()
            {
                Query = "123 East #425",
            };

            var uri = sut.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("query=123 East #425");
            uri.PathAndQuery.Should().Contain("query=123%20East%20%23425");
        }

        [Fact]
        public void BuildGeocodingRequest_WithInvalidParameters_FailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildGeocodingRequest(new GeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Query')");
#else
                .WithMessage("*Parameter name: Query");
#endif
        }

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildReverseGeocodingRequest_WithValidParameters_SuccessfullyBuildsUrl(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                Key = "123abc",
            };

            parameters.Layers.Add(Layer.Locality);
            parameters.Layers.Add(Layer.Fine);

            // Act
            var uri = sut.BuildReverseGeocodingRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("coordinates=56.78,78.91");
            query.Should().Contain("layers=locality,fine");

            _httpClient.DefaultRequestHeaders.Authorization.Scheme.Should().Be("123abc");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildReverseGeocodingRequest_WithInvalidParameters_FailsWithException()
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

        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildAutocompleteRequest_WithValidParameters_SuccessfullyBuildsUrl(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new AutocompleteParameters()
            {
                Query = "123 East",
                Near = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                Limit = 14,
                Mailable = true,
                Key = "123abc",
            };

            parameters.Layers.Add(Layer.PostalCode);
            parameters.Layers.Add(Layer.Country);

            parameters.Countries.Add("CA");

            // Act
            var uri = sut.BuildAutocompleteRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("query=123 East");
            query.Should().Contain("near=56.78,78.91");
            query.Should().Contain("limit=14");
            query.Should().Contain("mailable=true");
            query.Should().Contain("country=CA");
            query.Should().Contain("layers=postalCode,country");

            _httpClient.DefaultRequestHeaders.Authorization.Scheme.Should().Be("123abc");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildAutocompleteRequest_WithInvalidParameters_FailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildAutocompleteRequest(new AutocompleteParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Query')");
#else
                .WithMessage("*Parameter name: Query");
#endif
        }

        [Fact]
        public void ValidateAndBuildUri_WithValidParameters_SuccessfullyBuildsUri()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                Key = "123abc",
            };

            parameters.Layers.Add(Layer.Locality);
            parameters.Layers.Add(Layer.Fine);

            var uri = sut.ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, sut.BuildReverseGeocodingRequest);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("coordinates=56.78,78.91");
            query.Should().Contain("layers=locality,fine");

            _httpClient.DefaultRequestHeaders.Authorization.Scheme.Should().Be("123abc");
        }

        [Fact]
        public void ValidateAndBuildUri_WithNullParameters_FailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<ReverseGeocodingParameters>(null, sut.BuildReverseGeocodingRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithMessage("*See the inner exception for more information.")
                .WithInnerException<ArgumentNullException>();
        }

        [Fact]
        public void ValidateAndBuildUri_WithInvalidParameters_FailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<ReverseGeocodingParameters>(new ReverseGeocodingParameters(), sut.BuildReverseGeocodingRequest);

            act.Should()
                .Throw<GeoNETException>()
                .WithMessage("*See the inner exception for more information.")
                .WithInnerException<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Coordinate')");
#else
                .WithMessage("*Parameter name: Coordinate");
#endif
        }

        [Fact]
        public async Task GeocodingAsync_WithValidParameters_ReturnsSuccessfully()
        {
            var sut = BuildService();

            var parameters = new GeocodingParameters()
            {
                Query = "123 East",
            };

            var result = await sut.GeocodingAsync(parameters);
            result.Addresses.Count.Should().Be(1);
        }

        [Fact]
        public async Task ReverseGeocodingAsync_WithValidParameters_ReturnsSuccessfully()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
            };

            var result = await sut.ReverseGeocodingAsync(parameters);
            result.Addresses.Count.Should().Be(1);
        }

        [Fact]
        public async Task AutocompleteAsync_WithValidParameters_ReturnsSuccessfully()
        {
            var sut = BuildService();

            var parameters = new AutocompleteParameters()
            {
                Query = "123 East",
            };

            var result = await sut.AutocompleteAsync(parameters);
            result.Addresses.Count.Should().Be(10);
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

        private RadarGeocoding BuildService()
        {
            return new RadarGeocoding(_httpClient, _options.Object);
        }
    }
}