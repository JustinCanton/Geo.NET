// <copyright file="ArcGISGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Tests.Services
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
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.Enums;
    using Geo.ArcGIS.Models;
    using Geo.ArcGIS.Models.Parameters;
    using Geo.ArcGIS.Models.Responses;
    using Geo.ArcGIS.Services;
    using Geo.Core;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ArcGISGeocoding"/> class.
    /// </summary>
    public class ArcGISGeocodingShould : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Mock<IArcGISTokenContainer> _mockTokenContainer;
        private readonly IGeoNETExceptionProvider _exceptionProvider;
        private readonly IGeoNETResourceStringProviderFactory _resourceStringProviderFactory;
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISGeocodingShould"/> class.
        /// </summary>
        public ArcGISGeocodingShould()
        {
            _mockTokenContainer = new Mock<IArcGISTokenContainer>();
            _mockTokenContainer
                .Setup(x => x.GetTokenAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync("token123");

            var mockHandler = new Mock<HttpMessageHandler>();

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                        "{\"spatialReference\":{\"wkid\":4326,\"latestWkid\":4326},\"locations\":[{\"address\":\"123 East\",\"location\":{\"x\":-85.837039999999945,\"y\":37.710620000000063}," +
                        "\"score\":100,\"attributes\":{\"ResultID\":0,\"Loc_name\":\"World\",\"Status\":\"M\",\"Score\":100,\"Match_addr\":\"123 East\",\"LongLabel\":\"123 East, 3046 Dolphin Dr, Elizabethtown, KY, 42701, USA\"," +
                        "\"ShortLabel\":\"123 East\",\"Addr_type\":\"POI\",\"Type\":\"American Food\",\"PlaceName\":\"123 East\",\"Place_addr\":\"3046 Dolphin Dr, Elizabethtown, Kentucky, 42701\",\"Phone\":\"(270) 982 - 5311\"," +
                        "\"URL\":\"\",\"Rank\":19,\"AddBldg\":\"\",\"AddNum\":\"3046\",\"AddNumFrom\":\"\",\"AddNumTo\":\"\",\"AddRange\":\"\",\"Side\":\"R\",\"StPreDir\":\"\",\"StPreType\":\"\",\"StName\":\"Dolphin\"," +
                        "\"StType\":\"Dr\",\"StDir\":\"\",\"BldgType\":\"\",\"BldgName\":\"\",\"LevelType\":\"\",\"LevelName\":\"\",\"UnitType\":\"\",\"UnitName\":\"\",\"SubAddr\":\"\",\"StAddr\":\"3046 Dolphin Dr\"," +
                        "\"Block\":\"\",\"Sector\":\"\",\"Nbrhd\":\"\",\"District\":\"\",\"City\":\"Elizabethtown\",\"MetroArea\":\"\",\"Subregion\":\"Hardin County\",\"Region\":\"Kentucky\",\"RegionAbbr\":\"KY\"," +
                        "\"Territory\":\"\",\"Zone\":\"\",\"Postal\":\"42701\",\"PostalExt\":\"\",\"Country\":\"USA\",\"LangCode\":\"ENG\",\"Distance\":0,\"X\":-85.837509973310887,\"Y\":37.710570043857146," +
                        "\"DisplayX\":-85.837039999999945,\"DisplayY\":37.710620000000063,\"Xmin\":-85.84203999999994,\"Xmax\":-85.832039999999949,\"Ymin\":37.70562000000006,\"Ymax\":37.715620000000065,\"ExInfo\":\"\"}}]}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/arcgis/rest/services/World/GeocodeServer/geocodeAddresses")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                        "{ \"address\":{ \"Match_addr\":\"Cali's California Style Burritos\", \"LongLabel\":\"Cali's California Style Burritos, 3046 Dolphin Dr, Elizabethtown, KY, 42701, USA\"," +
                        "\"ShortLabel\":\"Cali's California Style Burritos\", \"Addr_type\":\"POI\", \"Type\":\"Mexican Food\", \"PlaceName\":\"Cali's California Style Burritos\"," +
                        "\"AddNum\":\"3046\", \"Address\":\"3046 Dolphin Dr\", \"Block\":\"\", \"Sector\":\"\", \"Neighborhood\":\"\", \"District\":\"\", \"City\":\"Elizabethtown\", \"MetroArea\":\"\"," +
                        "\"Subregion\":\"Hardin County\", \"Region\":\"Kentucky\", \"Territory\":\"\", \"Postal\":\"42701\", \"PostalExt\":\"\", \"CountryCode\":\"USA\" }," +
                        "\"location\":{ \"x\":-85.837039999999945,\"y\":37.710620000000063,\"spatialReference\":{ \"wkid\":4326,\"latestWkid\":4326} }}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/arcgis/rest/services/World/GeocodeServer/reverseGeocode")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"suggestions\":[" +
                        "{ \"text\":\"123 East, 3046 Dolphin Dr, Elizabethtown, KY, 42701, USA\", \"magicKey\":\"dHA9MCNsb2M9OTk2MzI1I2xuZz0zMyNwbD0yOTIyODAjbGJzPTE0OjU2ODczNg==\", \"isCollection\":false }," +
                        "{ \"text\":\"123 East Rd, Martinsburg, WV, 25404, USA\", \"magicKey\":\"dHA9MCNsb2M9NDkwODAjbG5nPTMzI2huPTEyMyNsYnM9MTA5OjQ1MTcxMzQ3\", \"isCollection\":false }," +
                        "{ \"text\":\"123 East Ave NE, Coeburn, VA, 24230, USA\", \"magicKey\":\"dHA9MCNsb2M9MTQ0OTYzI2xuZz0zMyNobj0xMjMjbGJzPTEwOTo0NTE3MTE4Nw==\", \"isCollection\":false }," +
                        "{ \"text\":\"123 East Ave, Hampton, VA, 23661, USA\", \"magicKey\":\"dHA9MCNsb2M9MjUwMjE3I2xuZz0zMyNobj0xMjMjbGJzPTEwOTo0NTE3MTE4NQ==\", \"isCollection\":false }," +
                        "{ \"text\":\"123 East Ave, Middlebourne, WV, 26149, USA\", \"magicKey\":\"dHA9MCNsb2M9NTM3NDQjbG5nPTMzI2huPTEyMyNsYnM9MTA5OjQ1MTcxMTg1\", \"isCollection\":false }]}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/arcgis/rest/services/World/GeocodeServer/suggest")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"spatialReference\":{\"wkid\":4326,\"latestWkid\":4326},\"candidates\":[{\"address\":\"123 East\",\"location\":{\"x\":-85.837039999999945,\"y\":37.710620000000063}," +
                        "\"score\":100,\"attributes\":{\"Match_addr\":\"123 East\",\"Addr_type\":\"POI\"},\"extent\":{\"xmin\":-85.84203999999994,\"ymin\":37.70562000000006,\"xmax\":-85.832039999999949,\"ymax\":37.715620000000065}}]}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/arcgis/rest/services/World/GeocodeServer/findAddressCandidates") && x.RequestUri.PathAndQuery.Contains("singleLine")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"spatialReference\":{\"wkid\":4326,\"latestWkid\":4326},\"candidates\":[]}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/arcgis/rest/services/World/GeocodeServer/findAddressCandidates") && x.RequestUri.PathAndQuery.Contains("category")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[^1]);

            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            _resourceStringProviderFactory = new GeoNETResourceStringProviderFactory();
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
        /// Tests the token is properly set into the query string.
        /// </summary>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Fact]
        public async Task AddArcGISTokenSuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;

            query = await sut.AddArcGISToken(query, CancellationToken.None).ConfigureAwait(false);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["token"].Should().Be("token123");
        }

        /// <summary>
        /// Tests the storage parameter query information is properly set into the query string.
        /// </summary>
        [Fact]
        public void AddStorageParameterSuccessfully()
        {
            var query = QueryString.Empty;
            var parameters = new StorageParameters()
            {
                ForStorage = false,
            };

            ArcGISGeocoding.AddStorageParameter(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["forStorage"].Should().Be("false");

            query = QueryString.Empty;
            parameters = new StorageParameters()
            {
                ForStorage = true,
            };

            ArcGISGeocoding.AddStorageParameter(parameters, ref query);

            queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["forStorage"].Should().Be("true");
        }

        /// <summary>
        /// Tests the address candidate uri is built properly.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public async Task BuildAddressCandidateRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new AddressCandidateParameters()
            {
                SingleLineAddress = "123 East",
                ForStorage = true,
            };

            // Act
            var uri = await sut.BuildAddressCandidateRequest(parameters, CancellationToken.None).ConfigureAwait(false);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("singleLine=123 East");
            query.Should().Contain("forStorage=true");
            query.Should().Contain("f=json");
            query.Should().Contain("outFields=Match_addr,Addr_type");
            query.Should().Contain("token=token123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the address candidate uri isn't built if an single line address isn't passed in.
        /// </summary>
        [Fact]
        public void BuildAddressCandidateRequestWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildAddressCandidateRequest(new AddressCandidateParameters(), CancellationToken.None).GetAwaiter().GetResult();

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'SingleLineAddress')");
        }

        /// <summary>
        /// Tests the place candidate uri is built properly.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public async Task BuildPlaceCandidateRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new PlaceCandidateParameters()
            {
                Category = "restaurant",
                Location = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                MaximumLocations = 4,
                ForStorage = false,
            };

            // Act
            var uri = await sut.BuildPlaceCandidateRequest(parameters, CancellationToken.None).ConfigureAwait(false);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("f=json");
            query.Should().Contain("outFields=Place_addr,PlaceName");
            query.Should().Contain("category=restaurant");
            query.Should().Contain("location=123.456,56.789");
            query.Should().Contain("maxLocations=4");
            query.Should().Contain("forStorage=false");
            query.Should().Contain("token=token123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the suggest uri is built properly.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public async Task BuildSuggestRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new SuggestParameters()
            {
                Text = "123 East",
                Category = "restaurant",
                Location = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                SearchExtent = new BoundingBox()
                {
                    EastLongitude = 123.456,
                    WestLongitude = 121.323,
                    NorthLatitude = 67.89,
                    SouthLatitude = 65.432,
                },
                MaximumLocations = 2,
            };

            // Act
            var uri = await sut.BuildSuggestRequest(parameters, CancellationToken.None).ConfigureAwait(false);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("text=123 East");
            query.Should().Contain("category=restaurant");
            query.Should().Contain("location=123.456,56.789");
            query.Should().Contain("searchExtent=121.323,67.89,123.456,65.432");
            query.Should().Contain("maxLocations=2");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the suggest uri isn't built if the text isn't passed in.
        /// </summary>
        [Fact]
        public void BuildSuggestRequestWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildSuggestRequest(new SuggestParameters(), CancellationToken.None).GetAwaiter().GetResult();

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Text')");
        }

        /// <summary>
        /// Tests the reverse geocoding uri is built properly.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public async Task BuildReverseGeocodingRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new ReverseGeocodingParameters()
            {
                Location = new Coordinate()
                {
                    Latitude = 80.012,
                    Longitude = 123.456,
                },
                OutSpatialReference = 12345,
                LanguageCode = new CultureInfo("en-CA"),
                ForStorage = false,
            };

            parameters.FeatureTypes.Add(FeatureType.DistanceMarker);
            parameters.FeatureTypes.Add(FeatureType.POI);
            parameters.FeatureTypes.Add(FeatureType.Postal);
            parameters.FeatureTypes.Add(FeatureType.StreetName);

            // Act
            var uri = await sut.BuildReverseGeocodingRequest(parameters, CancellationToken.None).ConfigureAwait(false);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("location=123.456,80.012");
            query.Should().Contain("outSR=12345");
            query.Should().Contain("langCode=en");
            query.Should().Contain("featureTypes=DistanceMarker,POI,Postal,StreetName");
            query.Should().Contain("locationType=rooftop");
            query.Should().Contain("preferredLabelValues=postalCity");
            query.Should().Contain("forStorage=false");
            query.Should().Contain("token=token123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the reverse geocoding uri isn't built if the location isn't passed in.
        /// </summary>
        [Fact]
        public void BuildReverseGeocodingRequestWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildReverseGeocodingRequest(new ReverseGeocodingParameters(), CancellationToken.None).GetAwaiter().GetResult();

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'Location')");
        }

        /// <summary>
        /// Tests the geocoding uri is built properly.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public async Task BuildGeocodingRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new GeocodingParameters()
            {
                Category = "restaurant",
                SearchExtent = new BoundingBox()
                {
                    EastLongitude = 123.456,
                    WestLongitude = 121.323,
                    NorthLatitude = 67.89,
                    SouthLatitude = 65.432,
                },
                OutSpatialReference = 12345,
                LanguageCode = new CultureInfo("ES"),
            };

            parameters.SourceCountry.Add(new RegionInfo("FR"));
            parameters.SourceCountry.Add(new RegionInfo("DE"));

            parameters.AddressAttributes.Add(
                new AddressAttributeParameter()
                {
                    ObjectId = 1,
                    SingleLine = "123 East",
                    Address = "Same As Above",
                    Neighbourhood = "East Hood",
                    City = "East City",
                    Subregion = "East Subregion",
                    Region = "East Region",
                });

            // Act
            var uri = await sut.BuildGeocodingRequest(parameters, CancellationToken.None).ConfigureAwait(false);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("addresses={\"records\":[{\"attributes\":{\"ObjectId\":1,\"SingleLine\":\"123 East\",\"Address\":\"Same As Above\",\"Neighbourhood\":\"East Hood\",\"City\":\"East City\",\"Subregion\":\"East Subregion\",\"Region\":\"East Region\"}}]}");
            query.Should().Contain("category=restaurant");
            query.Should().Contain("sourceCountry=FRA,DEU");
            query.Should().Contain("matchOutOfRange=true");
            query.Should().Contain("outSR=12345");
            query.Should().Contain("langCode=es");
            query.Should().Contain("locationType=rooftop");
            query.Should().Contain("preferredLabelValues=postalCity");
            query.Should().Contain("token=token123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the geocoding uri isn't built if the address attributes aren't passed in.
        /// </summary>
        [Fact]
        public void BuildGeocodingRequestWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildGeocodingRequest(new GeocodingParameters(), CancellationToken.None).GetAwaiter().GetResult();

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("*(Parameter 'AddressAttributes')");
        }

        /// <summary>
        /// Tests the geocoding returns a response successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task GeocodingAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new GeocodingParameters();

            parameters.AddressAttributes.Add(
                new AddressAttributeParameter()
                {
                    ObjectId = 1,
                    SingleLine = "123 East",
                });

            var response = await sut.GeocodingAsync(parameters).ConfigureAwait(false);
            response.Locations.Count.Should().Be(1);
            response.SpatialReference.WellKnownID.Should().Be(4326);
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
                Location = new Coordinate()
                {
                    Latitude = 37.710620000000063,
                    Longitude = -85.837039999999945,
                },
            };

            var response = await sut.ReverseGeocodingAsync(parameters).ConfigureAwait(false);
            response.Address.MatchAddress.Should().Be("Cali's California Style Burritos");
            response.Location.Longitude.Should().Be(-85.837039999999945);
        }

        /// <summary>
        /// Tests the suggest returns a response successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task SuggestAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new SuggestParameters()
            {
                Text = "123 East",
            };

            var response = await sut.SuggestAsync(parameters).ConfigureAwait(false);
            response.Suggestions.Count.Should().Be(5);
        }

        /// <summary>
        /// Tests the place candidate returns a response successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task PlaceCandidateAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new PlaceCandidateParameters()
            {
                Category = "restaurants",
            };

            var response = await sut.PlaceCandidateAsync(parameters).ConfigureAwait(false);
            response.Candidates.Count.Should().Be(0);
            response.SpatialReference.WellKnownID.Should().Be(4326);
        }

        /// <summary>
        /// Tests the address candidate returns a response successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task AddressCandidateAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new AddressCandidateParameters()
            {
                SingleLineAddress = "123 East",
            };

            var response = await sut.AddressCandidateAsync(parameters).ConfigureAwait(false);
            response.Candidates.Count.Should().Be(1);
            response.SpatialReference.WellKnownID.Should().Be(4326);
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

        private ArcGISGeocoding BuildService()
        {
            return new ArcGISGeocoding(_httpClient, _mockTokenContainer.Object, _exceptionProvider, _resourceStringProviderFactory);
        }
    }
}