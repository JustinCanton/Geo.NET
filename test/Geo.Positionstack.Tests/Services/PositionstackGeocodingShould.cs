// <copyright file="PositionstackGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Tests.Services
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
    using Geo.Positionstack.Models.Parameters;
    using Geo.Positionstack.Services;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="PositionstackGeocoding"/> class.
    /// </summary>
    public class PositionstackGeocodingShould : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Mock<IOptions<KeyOptions<IPositionstackGeocoding>>> _options = new Mock<IOptions<KeyOptions<IPositionstackGeocoding>>>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionstackGeocodingShould"/> class.
        /// </summary>
        public PositionstackGeocodingShould()
        {
            _options
                .Setup(x => x.Value)
                .Returns(new KeyOptions<IPositionstackGeocoding>()
                {
                    Key = "abc123",
                });

            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            _httpClient = new HttpClient(new Mock<HttpMessageHandler>().Object);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        [Fact]
        public void AddPositionstackKey_WithOptions_SuccessfullyAddsKey()
        {
            var sut = BuildService();

            var query = QueryString.Empty;

            sut.AddPositionstackKey(new GeocodingParameters(), ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["access_key"].Should().Be("abc123");
        }

        [Fact]
        public void AddPositionstackKey_WithParameterOverride_SuccessfullyAddsKey()
        {
            var sut = BuildService();

            var query = QueryString.Empty;

            sut.AddPositionstackKey(new GeocodingParameters() { Key = "123abc" }, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["access_key"].Should().Be("123abc");
        }

        [Fact]
        public void AddFilterParameters_WithValidData_AddsSuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new GeocodingParameters()
            {
                Language = "en",
                CountryModule = true,
                SunModule = true,
                TimezoneModule = true,
                BoundingBoxModule = true,
                Limit = 7,
            };

            parameters.Fields.Add("country.flag");
            parameters.Fields.Add("results.map_url");
            parameters.Fields.Add(string.Empty);

            sut.AddFilterParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(7);
            queryParameters["language"].Should().Be("en");
            queryParameters["country_module"].Should().Be("1");
            queryParameters["sun_module"].Should().Be("1");
            queryParameters["timezone_module"].Should().Be("1");
            queryParameters["bbox_module"].Should().Be("1");
            queryParameters["limit"].Should().Be("7");
            queryParameters["fields"].Should().Be("country.flag,results.map_url");
        }

        [Fact]
        public void AddFilterParameters_WithLessData_AddsSuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new GeocodingParameters()
            {
                CountryModule = false,
                SunModule = false,
                TimezoneModule = false,
                BoundingBoxModule = false,
            };

            sut.AddFilterParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["limit"].Should().Be("10");
        }

        [Fact]
        public void AddLocationParameters_WithValidLocationInformation_AddsSuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new GeocodingParameters()
            {
                Region = "Paris",
            };

            parameters.Countries.Add("CA");
            parameters.Countries.Add("FR");

            sut.AddLocationParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(2);
            queryParameters["country"].Should().Be("CA,FR");
            queryParameters["region"].Should().Be("Paris");
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

            parameters.Countries.Add("CA");

            // Act
            var uri = sut.BuildGeocodingRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("query=123 East");
            query.Should().Contain("country=CA");
            query.Should().Contain("access_key=123abc");

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

            // Act
            var uri = sut.BuildReverseGeocodingRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("query=56.78,78.91");
            query.Should().Contain("access_key=123abc");

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

            var uri = sut.ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, sut.BuildReverseGeocodingRequest);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("query=56.78,78.91");
            query.Should().Contain("access_key=123abc");
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
            }

            _disposed = true;
        }

        private PositionstackGeocoding BuildService()
        {
            return new PositionstackGeocoding(_httpClient, _options.Object);
        }
    }
}