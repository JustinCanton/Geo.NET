// <copyright file="BingGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Tests.Services
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
    using Geo.Bing.Models;
    using Geo.Bing.Models.Parameters;
    using Geo.Bing.Services;
    using Geo.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="BingGeocoding"/> class.
    /// </summary>
    public class BingGeocodingShould : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly BingKeyContainer _keyContainer;
        private readonly IGeoNETExceptionProvider _exceptionProvider;
        private readonly IStringLocalizerFactory _localizerFactory;
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BingGeocodingShould"/> class.
        /// </summary>
        public BingGeocodingShould()
        {
            _keyContainer = new BingKeyContainer("123abc");

            var mockHandler = new Mock<HttpMessageHandler>();

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'authenticationResultCode':'ValidCredentials','brandLogoUri':'http://dev.virtualearth.net/Branding/logo_powered_by.png'," +
                    "'copyright':'Copyright © 2020 Microsoft and its suppliers. All rights reserved. This API cannot be accessed and the content and any results may not be used," +
                    "reproduced or transmitted in any manner without express written permission from Microsoft Corporation.'," +
                    "'resourceSets':[{'estimatedTotal':1,'resources':[{'__type':'Location:http://schemas.microsoft.com/search/local/ws/rest/v1'," +
                    "'bbox':[47.643502452429324,-122.14677698166385,47.65122788757068,-122.13148835833614],'name':'1 Microsoft Way, Redmond, WA 98052'," +
                    "'point':{'type':'Point','coordinates':[47.64736517,-122.13913267]},'address':{'addressLine':'1 Microsoft Way','adminDistrict':'WA'," +
                    "'adminDistrict2':'King County','countryRegion':'United States','formattedAddress':'1 Microsoft Way, Redmond, WA 98052','locality':'Redmond'," +
                    "'postalCode':'98052','countryRegionIso2':'US'},'confidence':'High','entityType':'Address','geocodePoints':[{'type':'Point'," +
                    "'coordinates':[47.64736517,-122.13913267],'calculationMethod':'Rooftop','usageTypes':['Display']},{'type':'Point'," +
                    "'coordinates':[47.644459999999,-122.130462999999],'calculationMethod':'Rooftop','usageTypes':['Route']}],'matchCodes':['Good']," +
                    "'queryParseValues':[{'property':'AddressLine','value':'1 microsoft way'},{'property':'Locality','value':'redmond'},{'property':'AdminDistrict','value':'wa'}]}]}]," +
                    "'statusCode':200,'statusDescription':'OK','traceId':'03a0308fc21d4984873f095704aaa59e|CH000010A2|0.0.0.1|Ref A: AA58A734408F404DA92D6F4BC96F8B40 Ref B: CH1EDGE0809 Ref C: 2020-07-24T00:29:16Z'}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("REST/v1/Locations?query")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'authenticationResultCode':'ValidCredentials','brandLogoUri':'http://dev.virtualearth.net/Branding/logo_powered_by.png'," +
                    "'copyright':'Copyright © 2020 Microsoft and its suppliers. All rights reserved. This API cannot be accessed and the content and any results may not be used, " +
                    "reproduced or transmitted in any manner without express written permission from Microsoft Corporation.'," +
                    "'resourceSets':[{'estimatedTotal':1,'resources':[{'__type':'Location:http://schemas.microsoft.com/search/local/ws/rest/v1'," +
                    "'bbox':[40.752777282429321,-73.996387763584124,40.760502717570674,-73.982790236415866],'name':'640 8th Ave, New York, NY 10036'," +
                    "'point':{'type':'Point','coordinates':[40.75664,-73.989589]},'address':{'addressLine':'640 8th Ave','adminDistrict':'NY','adminDistrict2':'New York Co.'," +
                    "'countryRegion':'United States','formattedAddress':'640 8th Ave, New York, NY 10036'," +
                    "'intersection':{'baseStreet':'8th Ave','secondaryStreet1':'W 41st St','secondaryStreet2':'W 42nd St','intersectionType':'Between','displayName':'8th Ave, between W 41st St and W 42nd St'}," +
                    "'locality':'Garment District','postalCode':'10036'},'confidence':'High','entityType':'Address'," +
                    "'geocodePoints':[{'type':'Point','coordinates':[40.75664,-73.989589],'calculationMethod':'Parcel','usageTypes':['Display']}],'matchCodes':['Good']}]}]," +
                    "'statusCode':200,'statusDescription':'OK','traceId':'e511dfd0857b41f1aee1bc60510c92dd|CH000010AD|0.0.0.1|CH01EAP00000CE7'}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("REST/v1/Locations/")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'authenticationResultCode':'ValidCredentials','brandLogoUri':'http://dev.virtualearth.net/Branding/logo_powered_by.png'," +
                    "'copyright':'Copyright © 2020 Microsoft and its suppliers. All rights reserved. This API cannot be accessed and the content and any results may not be used, " +
                    "reproduced or transmitted in any manner without express written permission from Microsoft Corporation.'," +
                    "'resourceSets':[{'estimatedTotal':2,'resources':[{'__type':'Location:http://schemas.microsoft.com/search/local/ws/rest/v1'," +
                    "'bbox':[43.643867082429324,-79.387334063313247,43.651592517570677,-79.37309973668674],'name':'222 Bay St, Downtown Toronto, Toronto, ON M5J, Canada'," +
                    "'point':{'type':'Point','coordinates':[43.6477298,-79.3802169]},'address':{'addressLine':'222 Bay St','adminDistrict':'ON'," +
                    "'adminDistrict2':'Toronto','countryRegion':'Canada','formattedAddress':'222 Bay St, Downtown Toronto, Toronto, ON M5J, Canada'," +
                    "'locality':'Toronto','postalCode':'M5J'},'confidence':'Medium','entityType':'Address'," +
                    "'geocodePoints':[{'type':'Point','coordinates':[43.6477298,-79.3802169],'calculationMethod':'Rooftop','usageTypes':['Display']}," +
                    "{'type':'Point','coordinates':[43.647807064497,-79.3798914855925],'calculationMethod':'Rooftop','usageTypes':['Route']}]," +
                    "'matchCodes':['Ambiguous']},{'__type':'Location:http://schemas.microsoft.com/search/local/ws/rest/v1'," +
                    "'bbox':[43.643967351394977,-79.3870843772517,43.65169278653633,-79.372850026866857],'name':'222 Bay St, Downtown Toronto, Toronto, ON M5L, Canada'," +
                    "'point':{'type':'Point','coordinates':[43.647830068965654,-79.379967202059277]},'address':{'addressLine':'222 Bay St'," +
                    "'adminDistrict':'ON','adminDistrict2':'Toronto','countryRegion':'Canada','formattedAddress':'222 Bay St, Downtown Toronto, Toronto, ON M5L, Canada'," +
                    "'locality':'Toronto','postalCode':'M5L'},'confidence':'Medium','entityType':'Address'," +
                    "'geocodePoints':[{'type':'Point','coordinates':[43.647830068965654,-79.379967202059277],'calculationMethod':'InterpolationOffset','usageTypes':['Display']}," +
                    "{'type':'Point','coordinates':[43.647843999251016,-79.379908116806476],'calculationMethod':'Interpolation','usageTypes':['Route']}],'matchCodes':['Ambiguous']}]}]," +
                    "'statusCode':200,'statusDescription':'OK','traceId':'6ae6e5559034467397975a98e8058202|CH0000108C|0.0.0.1|Ref A: AADC32DAE8A14CA3BF8B7C11893FE2DC Ref B: CH1EDGE1106 Ref C: 2020-07-30T02:28:48Z'}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("REST/v1/Locations?adminDistrict")),
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
        public void AddBingKeySuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;

            sut.AddBingKey(ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["key"].Should().Be("123abc");
        }

        /// <summary>
        /// Tests the base query information is properly set into the query string.
        /// </summary>
        [Fact]
        public void BuildBaseQuerySuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new BaseParameters()
            {
                IncludeNeighbourhood = true,
                IncludeQueryParse = true,
                IncludeCiso2 = true,
            };

            sut.BuildBaseQuery(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(2);
            queryParameters["includeNeighborhood"].Should().Be("1");
            queryParameters["include"].Should().Contain("queryParse").And.Contain("ciso2");
        }

        /// <summary>
        /// Tests the base query information is properly set into the query string.
        /// </summary>
        [Fact]
        public void BuildLimitedResultQuerySuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new ResultParameters()
            {
                MaximumResults = 7,
                IncludeNeighbourhood = true,
                IncludeQueryParse = true,
                IncludeCiso2 = true,
            };

            sut.BuildLimitedResultQuery(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(3);
            queryParameters["maxResults"].Should().Be("7");
            queryParameters["includeNeighborhood"].Should().Be("1");
            queryParameters["include"].Should().Contain("queryParse").And.Contain("ciso2");
        }

        /// <summary>
        /// Tests the geocoding uri isn't built if a query isn't passed in.
        /// </summary>
        [Fact]
        public void BuildGeocodingRequestWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildGeocodingRequest(new GeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Query')");
        }

        /// <summary>
        /// Tests the geocoding uri is built properly.
        /// </summary>
        [Fact]
        public void BuildGeocodingRequestSuccessfully()
        {
            var sut = BuildService();

            var parameters = new GeocodingParameters()
            {
                Query = "1 Microsoft Way Redmond WA",
                MaximumResults = 7,
                IncludeNeighbourhood = true,
                IncludeQueryParse = true,
                IncludeCiso2 = true,
            };

            var uri = sut.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("query=1 Microsoft Way Redmond WA");
            query.Should().Contain("maxResults=7");
            query.Should().Contain("includeNeighborhood=1");
            query.Should().Contain("include=queryParse,ciso2");
            query.Should().Contain("key=123abc");
        }

        /// <summary>
        /// Tests the reverse geocoding uri isn't built if a point isn't passed in.
        /// </summary>
        [Fact]
        public void BuildReverseGeocodingRequestWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildReverseGeocodingRequest(new ReverseGeocodingParameters() { Point = null });

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Point')");
        }

        /// <summary>
        /// Tests the reverse geocoding uri is built properly.
        /// </summary>
        [Fact]
        public void BuildReverseGeocodingRequestSuccessfully()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Point = new Coordinate()
                {
                    Latitude = 40.7567,
                    Longitude = -73.9897,
                },
                IncludeAddress = true,
                IncludeAddressNeighbourhood = true,
                IncludePopulatedPlace = true,
                IncludePostcode = true,
                IncludeAdministrationDivision1 = true,
                IncludeAdministrationDivision2 = true,
                IncludeCountryRegion = true,
                IncludeNeighbourhood = true,
                IncludeQueryParse = true,
                IncludeCiso2 = true,
            };

            var uri = sut.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("/40.7567,-73.9897");
            query.Should().Contain("includeEntityTypes=Address,Neighborhood,PopulatedPlace,Postcode1,AdminDivision1,AdminDivision2,CountryRegion");
            query.Should().Contain("includeNeighborhood=1");
            query.Should().Contain("include=queryParse,ciso2");
            query.Should().Contain("key=123abc");
        }

        /// <summary>
        /// Tests the address geocoding uri isn't built if not enought information is passed in.
        /// </summary>
        [Fact]
        public void BuildAddressGeocodingRequestWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildAddressGeocodingRequest(new AddressGeocodingParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'parameters')");
        }

        /// <summary>
        /// Tests the address geocoding uri is built properly.
        /// </summary>
        [Fact]
        public void BuildAddressGeocodingRequestSuccessfully()
        {
            var sut = BuildService();

            var parameters = new AddressGeocodingParameters()
            {
                AdministrationDistrict = "Ontario",
                Locality = "Toronto",
                PostalCode = "M5J",
                AddressLine = "222 Bay Street",
                CountryRegion = new RegionInfo("en-CA"),
                MaximumResults = 8,
                IncludeNeighbourhood = true,
                IncludeQueryParse = true,
                IncludeCiso2 = true,
            };

            var uri = sut.BuildAddressGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("adminDistrict=Ontario");
            query.Should().Contain("locality=Toronto");
            query.Should().Contain("postalCode=M5J");
            query.Should().Contain("addressLine=222 Bay Street");
            query.Should().Contain("countryRegion=CA");
            query.Should().Contain("maxResults=8");
            query.Should().Contain("includeNeighborhood=1");
            query.Should().Contain("include=queryParse,ciso2");
            query.Should().Contain("key=123abc");
        }

        /// <summary>
        /// Tests the geocoding returns a response successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task GeocodingAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new GeocodingParameters()
            {
                Query = "1 Microsoft Way Redmond WA",
            };

            var response = await sut.GeocodingAsync(parameters).ConfigureAwait(false);
            response.StatusCode.Should().Be(200);
            response.ResourceSets.Count.Should().Be(1);
        }

        /// <summary>
        /// Tests the reverse geocoding returns a response successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task ReverseGeocodingAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Point = new Coordinate()
                {
                    Latitude = 40.7567,
                    Longitude = -73.9897,
                },
            };

            var response = await sut.ReverseGeocodingAsync(parameters).ConfigureAwait(false);
            response.StatusCode.Should().Be(200);
            response.ResourceSets.Count.Should().Be(1);
        }

        /// <summary>
        /// Tests the address geocoding returns a response successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task AddressGeocodingAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new AddressGeocodingParameters()
            {
                AdministrationDistrict = "Ontario",
                Locality = "Toronto",
                PostalCode = "M5J",
                AddressLine = "222 Bay Street",
                CountryRegion = new RegionInfo("en-CA"),
            };

            var response = await sut.AddressGeocodingAsync(parameters).ConfigureAwait(false);
            response.StatusCode.Should().Be(200);
            response.ResourceSets.Count.Should().Be(1);
            response.ResourceSets[0].EstimatedTotal.Should().Be(2);
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

        private BingGeocoding BuildService()
        {
            return new BingGeocoding(_httpClient, _keyContainer, _exceptionProvider, _localizerFactory);
        }
    }
}