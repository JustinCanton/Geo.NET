// <copyright file="TrimbleGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Tests.Services
{
    using System;
    using System.Globalization;
    using System.Net.Http;
    using System.Threading;
    using System.Web;
    using FluentAssertions;
    using Geo.Core;
    using Geo.Core.Models.Exceptions;
    using Geo.Trimble.Models.Enums;
    using Geo.Trimble.Models.Parameters;
    using Geo.Trimble.Services;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="TrimbleGeocoding"/> class.
    /// </summary>
    public class TrimbleGeocodingShould : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Mock<IOptions<KeyOptions<ITrimbleGeocoding>>> _options = new Mock<IOptions<KeyOptions<ITrimbleGeocoding>>>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrimbleGeocodingShould"/> class.
        /// </summary>
        public TrimbleGeocodingShould()
        {
            _options
                .Setup(x => x.Value)
                .Returns(new KeyOptions<ITrimbleGeocoding>()
                {
                    Key = "abc123",
                });

            _httpClient = new HttpClient(new Mock<HttpMessageHandler>().Object);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void AddTrimbleKey_WithOptions_SuccessfullyAddsKey()
        {
            var sut = BuildService();

            var query = QueryString.Empty;

            sut.AddTrimbleKey(new GeocodingParameters(), ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["authToken"].Should().Be("abc123");
        }

        [Fact]
        public void AddTrimbleKey_WithParameterOverride_SuccessfullyAddsKey()
        {
            var sut = BuildService();

            var query = QueryString.Empty;

            sut.AddTrimbleKey(new GeocodingParameters() { Key = "override123" }, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["authToken"].Should().Be("override123");
        }

        [Fact]
        public void AddAddressParameters_WithAllFields_AddsAllParameters()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new GeocodingParameters()
            {
                Street = "100 Main St",
                City = "Springfield",
                State = "IL",
                Zip = "62701",
                County = "Sangamon",
                Country = "US",
                Region = Region.NorthAmerica,
                Dataset = "Current",
                MaxResults = 5,
                MatchNamedRoadsOnly = true,
            };

            sut.AddAddressParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters["Street"].Should().Be("100 Main St");
            queryParameters["City"].Should().Be("Springfield");
            queryParameters["State"].Should().Be("IL");
            queryParameters["Zip"].Should().Be("62701");
            queryParameters["County"].Should().Be("Sangamon");
            queryParameters["Country"].Should().Be("US");
            queryParameters["Region"].Should().Be("0");
            queryParameters["Dataset"].Should().Be("Current");
            queryParameters["MaxResults"].Should().Be("5");
            queryParameters["MatchNamedRoadsOnly"].Should().Be("true");
        }

        [Fact]
        public void AddAddressParameters_WithMinimalFields_OnlyAddsPresentFields()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new GeocodingParameters()
            {
                City = "Chicago",
                State = "IL",
            };

            sut.AddAddressParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters["City"].Should().Be("Chicago");
            queryParameters["State"].Should().Be("IL");
            queryParameters["Street"].Should().BeNull();
            queryParameters["Zip"].Should().BeNull();
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
                Street = "100 Main St",
                City = "Springfield",
                State = "IL",
                Key = "abc123",
            };

            // Act
            var uri = sut.BuildGeocodingRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("Street=100 Main St");
            query.Should().Contain("City=Springfield");
            query.Should().Contain("State=IL");
            query.Should().Contain("authToken=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildGeocodingRequest_WithNoAddressFields_ThrowsArgumentException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildGeocodingRequest(new GeocodingParameters());

            act.Should()
                .Throw<ArgumentException>();
        }

        [Fact]
        public void BuildGeocodingRequest_WithAdditionalParameters_AddsThemToQueryString()
        {
            var sut = BuildService();

            var parameters = new GeocodingParameters()
            {
                City = "Chicago",
            };

            parameters.AdditionalParameters.Add("customKey1", "customValue1");
            parameters.AdditionalParameters.Add("customKey2", "customValue2");

            var uri = sut.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("customKey1=customValue1");
            query.Should().Contain("customKey2=customValue2");
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
                    Latitude = 40.7128,
                    Longitude = -74.0060,
                },
                Key = "abc123",
            };

            // Act
            var uri = sut.BuildReverseGeocodingRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("Coords=-74.006,40.7128");
            query.Should().Contain("authToken=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildReverseGeocodingRequest_WithNullCoordinate_ThrowsArgumentException()
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
        public void BuildReverseGeocodingRequest_WithAllOptionalParameters_SuccessfullyBuildsUrl()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate() { Latitude = 40.7128, Longitude = -74.0060 },
                Region = Region.Europe,
                Dataset = "Current",
                Lang = "en",
                MatchNamedRoadsOnly = true,
                MaxCleanupMiles = 0.5,
                IncludePostedSpeedLimit = true,
                VehicleType = "Truck",
                Heading = 90.0,
                IncludeLinkInfo = true,
                CountryAbbrevType = CountryAbbrevType.ISO2,
                IncludeTrimblePlaceIds = true,
            };

            var uri = sut.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);

            query.Should().Contain("Region=3");
            query.Should().Contain("Dataset=Current");
            query.Should().Contain("lang=en");
            query.Should().Contain("matchNamedRoadsOnly=true");
            query.Should().Contain("maxCleanupMiles=0.5");
            query.Should().Contain("includePostedSpeedLimit=true");
            query.Should().Contain("vehicleType=Truck");
            query.Should().Contain("heading=90");
            query.Should().Contain("includeLinkInfo=true");
            query.Should().Contain("countryAbbrevType=ISO2");
            query.Should().Contain("includeTrimblePlaceIds=true");
        }

        [Fact]
        public void ValidateAndBuildUri_WithNullParameters_ThrowsGeoNETException()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<GeocodingParameters>(null, p => new Uri("https://example.com"));

            act.Should().Throw<GeoNETException>();
        }

        /// <summary>
        /// Releases resources.
        /// </summary>
        /// <param name="disposing">A flag indicating whether managed resources should be disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                }

                _disposed = true;
            }
        }

        private TrimbleGeocoding BuildService()
        {
            return new TrimbleGeocoding(_httpClient, _options.Object);
        }
    }
}
