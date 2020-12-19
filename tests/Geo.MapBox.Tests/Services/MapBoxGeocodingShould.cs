﻿// <copyright file="MapBoxGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using FluentAssertions;
    using Geo.MapBox.Enums;
    using Geo.MapBox.Models;
    using Geo.MapBox.Models.Exceptions;
    using Geo.MapBox.Models.Parameters;
    using Geo.MapBox.Models.Responses;
    using Geo.MapBox.Services;
    using Moq;
    using Moq.Protected;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="MapBoxGeocoding"/> class.
    /// </summary>
    [TestFixture]
    public class MapBoxGeocodingShould
    {
        private Mock<HttpMessageHandler> _mockHandler;
        private MapBoxKeyContainer _keyContainer;

        /// <summary>
        /// One time setup information.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _keyContainer = new MapBoxKeyContainer("abc123");

            _mockHandler = new Mock<HttpMessageHandler>();

            // For reverse geocoding, use the places endpoint type
            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("mapbox.places")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
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

            // For reverse geocoding, use the permanent endpoint type
            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("mapbox.places-permanent")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
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
        }

        /// <summary>
        /// Tests the key is properly set into the query string.
        /// </summary>
        [Test]
        public void AddMapBoxKeySuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapBoxGeocoding(httpClient, _keyContainer);
            var query = new NameValueCollection();

            service.AddMapBoxKey(query);
            query.Count.Should().Be(1);
            query["access_token"].Should().Be("abc123");
        }

        /// <summary>
        /// Tests the base parameters are properly set into the query string.
        /// </summary>
        [Test]
        public void AddBaseParametersSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapBoxGeocoding(httpClient, _keyContainer);
            var query = new NameValueCollection();
            var parameters = new BaseParameters()
            {
                Countries = new List<string>()
                {
                    "CAN",
                    "FRA",
                },
                Languages = new List<string>()
                {
                    "en",
                    "fr",
                },
                Limit = 5,
                Routing = true,
                Types = new List<FeatureType>()
                {
                    FeatureType.Address,
                    FeatureType.Place,
                },
            };

            service.AddBaseParameters(parameters, query);
            query.Count.Should().Be(5);
            query["country"].Should().Be("CAN,FRA");
            query["language"].Should().Be("en,fr");
            query["limit"].Should().Be("5");
            query["routing"].Should().Be("true");
            query["types"].Should().Be("address,place");
        }

        /// <summary>
        /// Tests the building of the geocoding parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildGeocodingRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapBoxGeocoding(httpClient, _keyContainer);
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
                Countries = new List<string>()
                {
                    "CAN",
                    "FRA",
                },
                Languages = new List<string>()
                {
                    "en",
                    "fr",
                },
                Limit = 5,
                Routing = true,
                Types = new List<FeatureType>()
                {
                    FeatureType.Address,
                    FeatureType.Place,
                },
            };

            var uri = service.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("autocomplete=true");
            query.Should().Contain("bbox=123.45,45.67,165.43,87.65");
            query.Should().Contain("fuzzyMatch=false");
            query.Should().Contain("proximity=140.25,64.12");
            query.Should().Contain("country=CAN,FRA");
            query.Should().Contain("language=en,fr");
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
        [Test]
        public void BuildGeocodingRequestFailsWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapBoxGeocoding(httpClient, _keyContainer);
            Action act = () => service.BuildGeocodingRequest(new GeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The query cannot be null or empty. (Parameter 'Query')");
        }

        /// <summary>
        /// Tests the building of the reverse geocoding parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildReverseGeocodingRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapBoxGeocoding(httpClient, _keyContainer);
            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                EndpointType = EndpointType.Permenant,
                ReverseMode = ReverseMode.Score,
                Countries = new List<string>()
                {
                    "BGR",
                    "SWE",
                },
                Languages = new List<string>()
                {
                    "bg",
                    "sv",
                },
                Limit = 3,
                Routing = false,
                Types = new List<FeatureType>()
                {
                    FeatureType.Country,
                    FeatureType.District,
                    FeatureType.Neighborhood,
                },
            };

            var uri = service.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("reverseMode=score");
            query.Should().Contain("country=BGR,SWE");
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
        [Test]
        public void BuildReverseGeocodingRequestFailsWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapBoxGeocoding(httpClient, _keyContainer);
            Action act = () => service.BuildReverseGeocodingRequest(new ReverseGeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The coordinates cannot be null. (Parameter 'Coordinate')");
        }

        /// <summary>
        /// Tests the validation and creation of the reverse geocoding uri is done successfully.
        /// </summary>
        [Test]
        public void ValidateAndCraftUriSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapBoxGeocoding(httpClient, _keyContainer);
            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                EndpointType = EndpointType.Permenant,
                ReverseMode = ReverseMode.Score,
                Countries = new List<string>()
                {
                    "BGR",
                    "SWE",
                },
                Languages = new List<string>()
                {
                    "bg",
                    "sv",
                },
                Limit = 3,
                Routing = false,
                Types = new List<FeatureType>()
                {
                    FeatureType.Country,
                    FeatureType.District,
                    FeatureType.Neighborhood,
                },
            };

            var uri = service.ValidateAndCraftUri<ReverseGeocodingParameters>(parameters, service.BuildReverseGeocodingRequest);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("reverseMode=score");
            query.Should().Contain("country=BGR,SWE");
            query.Should().Contain("language=bg,sv");
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
        [Test]
        public void ValidateAndCraftUriFailsWithException1()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapBoxGeocoding(httpClient, _keyContainer);
            Action act = () => service.ValidateAndCraftUri<ReverseGeocodingParameters>(null, service.BuildReverseGeocodingRequest);

            act.Should()
                .Throw<MapBoxException>()
                .WithMessage("The MapBox parameters are null. Please see the inner exception for more information.")
                .WithInnerException<ArgumentNullException>();
        }

        /// <summary>
        /// Tests the validation and creation of the reverse geocoding uri fails if no id is provided and the exception is wrapped in a here exception.
        /// </summary>
        [Test]
        public void ValidateAndCraftUriFailsWithException2()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapBoxGeocoding(httpClient, _keyContainer);
            Action act = () => service.ValidateAndCraftUri<ReverseGeocodingParameters>(new ReverseGeocodingParameters(), service.BuildReverseGeocodingRequest);

            act.Should()
                .Throw<MapBoxException>()
                .WithMessage("Failed to create the MapBox uri. Please see the inner exception for more information.")
                .WithInnerException<ArgumentException>()
                .WithMessage("The coordinates cannot be null. (Parameter 'Coordinate')");
        }

        /// <summary>
        /// Tests the geocoding call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task GeocodingAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapBoxGeocoding(httpClient, _keyContainer);
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
                Countries = new List<string>()
                {
                    "CAN",
                    "FRA",
                },
                Languages = new List<string>()
                {
                    "en",
                    "fr",
                },
                Limit = 5,
                Routing = true,
                Types = new List<FeatureType>()
                {
                    FeatureType.Address,
                    FeatureType.Place,
                },
            };

            var result = await service.GeocodingAsync(parameters).ConfigureAwait(false);
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
        [Test]
        public async Task ReverseGeocodingAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new MapBoxGeocoding(httpClient, _keyContainer);
            var parameters = new ReverseGeocodingParameters()
            {
                Coordinate = new Coordinate()
                {
                    Latitude = 56.78,
                    Longitude = 78.91,
                },
                EndpointType = EndpointType.Permenant,
                ReverseMode = ReverseMode.Score,
                Countries = new List<string>()
                {
                    "BGR",
                    "SWE",
                },
                Languages = new List<string>()
                {
                    "bg",
                    "sv",
                },
                Limit = 3,
                Routing = false,
                Types = new List<FeatureType>()
                {
                    FeatureType.Country,
                    FeatureType.District,
                    FeatureType.Neighborhood,
                },
            };

            var result = await service.ReverseGeocodingAsync(parameters).ConfigureAwait(false);
            result.Query.ToString().Should().Be(new Coordinate()
            {
                Latitude = -28.081626,
                Longitude = 153.379627,
            }.ToString());
            result.Features.Count.Should().Be(1);
            result.Features[0].Id.Should().Be("address.4562086697174018");
        }
    }
}