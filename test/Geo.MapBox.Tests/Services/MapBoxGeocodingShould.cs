// <copyright file="MapBoxGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Tests.Services
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
    using Geo.MapBox.Enums;
    using Geo.MapBox.Models;
    using Geo.MapBox.Models.Exceptions;
    using Geo.MapBox.Models.Parameters;
    using Geo.MapBox.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="MapBoxGeocoding"/> class.
    /// </summary>
    public class MapBoxGeocodingShould : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly MapBoxKeyContainer _keyContainer;
        private readonly IGeoNETExceptionProvider _exceptionProvider;
        private readonly IStringLocalizerFactory _localizerFactory;
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapBoxGeocodingShould"/> class.
        /// </summary>
        public MapBoxGeocodingShould()
        {
            _keyContainer = new MapBoxKeyContainer("abc123");

            var mockHandler = new Mock<HttpMessageHandler>();

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                        "{\"type\":\"FeatureCollection\",\"query\":[\"123\",\"east\"],\"features\":[{\"id\":\"address.4562086697174018\",\"type\":\"Feature\",\"place_type\":[\"address\"],\"relevance\":1," +
                        "\"properties\":{\"accuracy\":\"point\"},\"text\":\"Easthill Drive\",\"place_name\":\"123 Easthill Drive, Robina Queensland 4226, Australia\",\"center\":[153.379627,-28.081626]," +
                        "\"geometry\":{\"type\":\"Point\",\"coordinates\":[153.379627,-28.081626]},\"address\":\"123\"," +
                        "\"context\":[{\"id\":\"postcode.7266040401534490\",\"text\":\"4226\"},{\"id\":\"locality.3059244982453840\",\"wikidata\":\"Q7352919\",\"text\":\"Robina\"}," +
                        "{\"id\":\"place.12294497843533720\",\"wikidata\":\"Q140075\",\"text\":\"Gold Coast\"},{\"id\":\"region.19496380243439240\",\"wikidata\":\"Q36074\",\"short_code\":\"AU - QLD\",\"text\":\"Queensland\"}," +
                        "{\"id\":\"country.9968792518346070\",\"wikidata\":\"Q408\",\"short_code\":\"au\",\"text\":\"Australia\"}]}]," +
                        "\"attribution\":\"NOTICE: © 2020 Mapbox and its suppliers.All rights reserved.Use of this data is subject to the Mapbox Terms of Service(https://www.mapbox.com/about/maps/). This response and the information it contains may not be retained. POI(s) provided by Foursquare.\"}"),
            });

            // For reverse geocoding, use the places endpoint type
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("mapbox.places")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                        "{\"type\":\"FeatureCollection\",\"query\":[153.379627,-28.081626],\"features\":[{\"id\":\"address.4562086697174018\",\"type\":\"Feature\",\"place_type\":[\"address\"],\"relevance\":1," +
                        "\"properties\":{\"accuracy\":\"point\"},\"text\":\"Easthill Drive\",\"place_name\":\"123 Easthill Drive, Robina Queensland 4226, Australia\",\"center\":[153.379627,-28.0816261]," +
                        "\"geometry\":{\"type\":\"Point\",\"coordinates\":[153.379627,-28.0816261]},\"address\":\"123\",\"context\":[{\"id\":\"postcode.7266040401534490\",\"text\":\"4226\"}," +
                        "{\"id\":\"locality.3059244982453840\",\"wikidata\":\"Q7352919\",\"text\":\"Robina\"},{\"id\":\"place.12294497843533720\",\"wikidata\":\"Q140075\",\"text\":\"Gold Coast\"}," +
                        "{\"id\":\"region.19496380243439240\",\"wikidata\":\"Q36074\",\"short_code\":\"AU - QLD\",\"text\":\"Queensland\"},{\"id\":\"country.9968792518346070\",\"wikidata\":\"Q408\",\"short_code\":\"au\",\"text\":\"Australia\"}]}]," +
                        "\"attribution\":\"NOTICE: © 2020 Mapbox and its suppliers.All rights reserved.Use of this data is subject to the Mapbox Terms of Service(https://www.mapbox.com/about/maps/). This response and the information it contains may not be retained. POI(s) provided by Foursquare.\"}"),
            });

            // For reverse geocoding, use the permanent endpoint type
            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("mapbox.places-permanent")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            _localizerFactory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            _httpClient = new HttpClient(mockHandler.Object);
            _exceptionProvider = new GeoNETExceptionProvider();
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
        public void AddMapBoxKeySuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;

            sut.AddMapBoxKey(ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["access_token"].Should().Be("abc123");
        }

        /// <summary>
        /// Tests the base parameters are properly set into the query string.
        /// </summary>
        [Fact]
        public void AddBaseParametersSuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new BaseParameters()
            {
                Limit = 5,
                Routing = true,
            };

            parameters.Countries.Add(new RegionInfo("CA"));
            parameters.Countries.Add(new RegionInfo("FR"));

            parameters.Languages.Add(new CultureInfo("en"));
            parameters.Languages.Add(new CultureInfo("fr-FR"));

            parameters.Types.Add(FeatureType.Address);
            parameters.Types.Add(FeatureType.Place);

            sut.AddBaseParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(5);
            queryParameters["country"].Should().Be("CA,FR");
            queryParameters["language"].Should().Be("en,fr-FR");
            queryParameters["limit"].Should().Be("5");
            queryParameters["routing"].Should().Be("true");
            queryParameters["types"].Should().Be("address,place");
        }

        /// <summary>
        /// Tests the building of the geocoding parameters is done successfully.
        /// </summary>
        [Fact]
        public void BuildGeocodingRequestSuccessfully()
        {
            var sut = BuildService();

            var parameters = new GeocodingParameters()
            {
                Query = "123 East",
                EndpointType = EndpointType.Places,
                ReturnAutocomplete = true,
                BoundingBox = new BoundingBox()
                {
                    West = 123.45,
                    North = 87.65,
                    East = 165.43,
                    South = 45.67,
                },
                FuzzyMatch = false,
                Proximity = new Coordinate()
                {
                    Latitude = 64.12,
                    Longitude = 140.25,
                },
                Limit = 5,
                Routing = true,
            };

            parameters.Countries.Add(new RegionInfo("CA"));
            parameters.Countries.Add(new RegionInfo("FR"));

            parameters.Languages.Add(new CultureInfo("en-CA"));
            parameters.Languages.Add(new CultureInfo("fr"));

            parameters.Types.Add(FeatureType.Address);
            parameters.Types.Add(FeatureType.Place);

            var uri = sut.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("autocomplete=true");
            query.Should().Contain("bbox=123.45,45.67,165.43,87.65");
            query.Should().Contain("fuzzyMatch=false");
            query.Should().Contain("proximity=140.25,64.12");
            query.Should().Contain("CA,FR");
            query.Should().Contain("en-CA,fr");
            query.Should().Contain("limit=5");
            query.Should().Contain("routing=true");
            query.Should().Contain("types=address,place");
            query.Should().Contain("access_token=abc123");

            var path = HttpUtility.UrlDecode(uri.AbsolutePath);
            path.Should().Contain("mapbox.places/123 East");
        }

        /// <summary>
        /// Tests the building of the geocoding parameters fails if no query is provided.
        /// </summary>
        [Fact]
        public void BuildGeocodingRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildGeocodingRequest(new GeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Query')");
        }

        /// <summary>
        /// Tests the building of the reverse geocoding parameters is done successfully.
        /// </summary>
        [Fact]
        public void BuildReverseGeocodingRequestSuccessfully()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                EndpointType = EndpointType.Permanent,
                ReverseMode = ReverseMode.Score,
                Limit = 3,
                Routing = false,
            };

            parameters.Countries.Add(new RegionInfo("BG"));
            parameters.Countries.Add(new RegionInfo("SE"));

            parameters.Languages.Add(new CultureInfo("bg"));
            parameters.Languages.Add(new CultureInfo("sv"));

            parameters.Types.Add(FeatureType.Country);
            parameters.Types.Add(FeatureType.District);
            parameters.Types.Add(FeatureType.Neighborhood);

            var uri = sut.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("reverseMode=score");
            query.Should().Contain("country=BG,SE");
            query.Should().Contain("language=bg,sv");
            query.Should().Contain("limit=3");
            query.Should().Contain("routing=false");
            query.Should().Contain("types=country,district,neighborhood");
            query.Should().Contain("access_token=abc123");

            var path = HttpUtility.UrlDecode(uri.AbsolutePath);
            path.Should().Contain("mapbox.places-permanent/78.91,56.78");
        }

        /// <summary>
        /// Tests the building of the reverse geocoding parameters fails if no coordinate is provided.
        /// </summary>
        [Fact]
        public void BuildReverseGeocodingRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildReverseGeocodingRequest(new ReverseGeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Coordinate')");
        }

        /// <summary>
        /// Tests the validation and creation of the reverse geocoding uri is done successfully.
        /// </summary>
        [Fact]
        public void ValidateAndCraftUriSuccessfully()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                EndpointType = EndpointType.Permanent,
                ReverseMode = ReverseMode.Score,
                Limit = 3,
                Routing = false,
            };

            parameters.Countries.Add(new RegionInfo("BG"));
            parameters.Countries.Add(new RegionInfo("SE"));

            parameters.Languages.Add(new CultureInfo("bg"));
            parameters.Languages.Add(new CultureInfo("sv-FI"));

            parameters.Types.Add(FeatureType.Country);
            parameters.Types.Add(FeatureType.District);
            parameters.Types.Add(FeatureType.Neighborhood);

            var uri = sut.ValidateAndBuildUri<ReverseGeocodingParameters>(parameters, sut.BuildReverseGeocodingRequest);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("reverseMode=score");
            query.Should().Contain("country=BG,SE");
            query.Should().Contain("language=bg,sv-FI");
            query.Should().Contain("limit=3");
            query.Should().Contain("routing=false");
            query.Should().Contain("types=country,district,neighborhood");
            query.Should().Contain("access_token=abc123");

            var path = HttpUtility.UrlDecode(uri.AbsolutePath);
            path.Should().Contain("mapbox.places-permanent/78.91,56.78");
        }

        /// <summary>
        /// Tests the validation and creation of the reverse geocoding uri fails if the parameters are null.
        /// </summary>
        [Fact]
        public void ValidateAndCraftUriFailsWithException1()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<ReverseGeocodingParameters>(null, sut.BuildReverseGeocodingRequest);

            act.Should()
                .Throw<MapBoxException>()
                .WithMessage("*See the inner exception for more information.")
                .WithInnerException<ArgumentNullException>();
        }

        /// <summary>
        /// Tests the validation and creation of the reverse geocoding uri fails if no id is provided and the exception is wrapped in a here exception.
        /// </summary>
        [Fact]
        public void ValidateAndCraftUriFailsWithException2()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<ReverseGeocodingParameters>(new ReverseGeocodingParameters(), sut.BuildReverseGeocodingRequest);

            act.Should()
                .Throw<MapBoxException>()
                .WithMessage("*See the inner exception for more information.")
                .WithInnerException<ArgumentException>()
                .WithMessage("*(Parameter 'Coordinate')");
        }

        /// <summary>
        /// Tests the geocoding call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task GeocodingAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new GeocodingParameters()
            {
                Query = "123 East",
                EndpointType = EndpointType.Places,
                ReturnAutocomplete = true,
                BoundingBox = new BoundingBox()
                {
                    West = 123.45,
                    North = 87.65,
                    East = 165.43,
                    South = 45.67,
                },
                FuzzyMatch = false,
                Proximity = new Coordinate()
                {
                    Latitude = 64.12,
                    Longitude = 140.25,
                },
                Limit = 5,
                Routing = true,
            };

            parameters.Countries.Add(new RegionInfo("CA"));
            parameters.Countries.Add(new RegionInfo("FR"));

            parameters.Languages.Add(new CultureInfo("en"));
            parameters.Languages.Add(new CultureInfo("fr"));

            parameters.Types.Add(FeatureType.Address);
            parameters.Types.Add(FeatureType.Place);

            var result = await sut.GeocodingAsync(parameters).ConfigureAwait(false);
            result.Query.Count.Should().Be(2);
            result.Query.Should().ContainInOrder(new List<string>()
            {
                "123",
                "east",
            });
            result.Features.Count.Should().Be(1);
            result.Features[0].Id.Should().Be("address.4562086697174018");
        }

        /// <summary>
        /// Tests the reverse geocoding call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task ReverseGeocodingAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                EndpointType = EndpointType.Permanent,
                ReverseMode = ReverseMode.Score,
                Limit = 3,
                Routing = false,
            };

            parameters.Countries.Add(new RegionInfo("BG"));
            parameters.Countries.Add(new RegionInfo("SE"));

            parameters.Languages.Add(new CultureInfo("bg"));
            parameters.Languages.Add(new CultureInfo("sv"));

            parameters.Types.Add(FeatureType.Country);
            parameters.Types.Add(FeatureType.District);
            parameters.Types.Add(FeatureType.Neighborhood);

            var result = await sut.ReverseGeocodingAsync(parameters).ConfigureAwait(false);
            result.Query.ToString().Should().Be(new Coordinate()
            {
                Latitude = -28.081626,
                Longitude = 153.379627,
            }.ToString());
            result.Features.Count.Should().Be(1);
            result.Features[0].Id.Should().Be("address.4562086697174018");
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

        private MapBoxGeocoding BuildService()
        {
            return new MapBoxGeocoding(_httpClient, _keyContainer, _exceptionProvider, _localizerFactory);
        }
    }
}