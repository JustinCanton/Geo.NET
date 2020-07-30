// <copyright file="GoogleGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using FluentAssertions;
    using Geo.Google.Enums;
    using Geo.Google.Extensions;
    using Geo.Google.Models;
    using Geo.Google.Services;
    using Moq;
    using Moq.Protected;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="GoogleGeocoding"/> class.
    /// </summary>
    [TestFixture]
    public class GoogleGeocodingShould
    {
        private HttpClient _httpClient;
        private GoogleKeyContainer _keyContainer;

        /// <summary>
        /// One time setup information.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _keyContainer = new GoogleKeyContainer("abc123");

            var handlerMock = new Mock<HttpMessageHandler>();

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("maps/api/geocode/json?address")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
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

            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("maps/api/geocode/json?latlng")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
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

            _httpClient = new HttpClient(handlerMock.Object);
        }

        /// <summary>
        /// Tests the key is properly set into the query string.
        /// </summary>
        [Test]
        public void AddGoogleKeySuccessfully()
        {
            var service = new GoogleGeocoding(_httpClient, _keyContainer);
            var query = new NameValueCollection();

            service.AddGoogleKey(query);
            query.Count.Should().Be(1);
            query["key"].Should().Be("abc123");
        }

        /// <summary>
        /// Tests the geocoding uri isn't built if an address isn't passed in.
        /// </summary>
        [Test]
        public void BuildGeocodingRequestWithException()
        {
            var service = new GoogleGeocoding(_httpClient, _keyContainer);
            Action act = () => service.BuildGeocodingRequest(new GeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The address cannot be null or empty. (Parameter 'Address')");
        }

        /// <summary>
        /// Tests the geocoding uri is built properly.
        /// </summary>
        [Test]
        public void BuildGeocodingRequestSuccessfully()
        {
            var service = new GoogleGeocoding(_httpClient, _keyContainer);
            var parameters = new GeocodingParameters()
            {
                Address = "123 East",
                Components = "test",
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
                Region = "us",
                Language = "en",
            };

            var uri = service.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("address=123 East");
            query.Should().Contain("components=test");
            query.Should().Contain("bounds=43.219,87.654|80.012,123.456");
            query.Should().Contain("region=us");
            query.Should().Contain("language=en");
            query.Should().Contain("key=abc123");
        }

        /// <summary>
        /// Tests the reverse geocoding uri isn't built if an address isn't passed in.
        /// </summary>
        [Test]
        public void BuildReverseGeocodingRequestWithException()
        {
            var service = new GoogleGeocoding(_httpClient, _keyContainer);
            Action act = () => service.BuildReverseGeocodingRequest(new ReverseGeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The coordinates cannot be null. (Parameter 'Coordinate')");
        }

        /// <summary>
        /// Tests the reverse geocoding uri is built properly.
        /// </summary>
        [Test]
        public void BuildReverseGeocodingRequestSuccessfully()
        {
            var service = new GoogleGeocoding(_httpClient, _keyContainer);
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
                Language = "en",
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
        [Test]
        public async Task GeocodingAsyncSuccessfully()
        {
            var service = new GoogleGeocoding(_httpClient, _keyContainer);
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
        [Test]
        public async Task ReverseGeocodingAsyncSuccessfully()
        {
            var service = new GoogleGeocoding(_httpClient, _keyContainer);
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
    }
}