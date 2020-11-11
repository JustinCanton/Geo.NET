// <copyright file="HereGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Tests.Services
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
    using Geo.Here.Abstractions;
    using Geo.Here.Enums;
    using Geo.Here.Models;
    using Geo.Here.Models.Parameters;
    using Geo.Here.Models.Responses;
    using Geo.Here.Services;
    using Moq;
    using Moq.Protected;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="HereGeocoding"/> class.
    /// </summary>
    [TestFixture]
    public class HereGeocodingShould
    {
        private Mock<HttpMessageHandler> _mockHandler;
        private HereKeyContainer _keyContainer;

        /// <summary>
        /// One time setup information.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _keyContainer = new HereKeyContainer("abc123");

            _mockHandler = new Mock<HttpMessageHandler>();

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/geocode")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
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

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/arcgis/rest/services/World/GeocodeServer/reverseGeocode")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        "{ \"address\":{ \"Match_addr\":\"Cali's California Style Burritos\", \"LongLabel\":\"Cali's California Style Burritos, 3046 Dolphin Dr, Elizabethtown, KY, 42701, USA\"," +
                        "\"ShortLabel\":\"Cali's California Style Burritos\", \"Addr_type\":\"POI\", \"Type\":\"Mexican Food\", \"PlaceName\":\"Cali's California Style Burritos\"," +
                        "\"AddNum\":\"3046\", \"Address\":\"3046 Dolphin Dr\", \"Block\":\"\", \"Sector\":\"\", \"Neighborhood\":\"\", \"District\":\"\", \"City\":\"Elizabethtown\", \"MetroArea\":\"\"," +
                        "\"Subregion\":\"Hardin County\", \"Region\":\"Kentucky\", \"Territory\":\"\", \"Postal\":\"42701\", \"PostalExt\":\"\", \"CountryCode\":\"USA\" }," +
                        "\"location\":{ \"x\":-85.837039999999945,\"y\":37.710620000000063,\"spatialReference\":{ \"wkid\":4326,\"latestWkid\":4326} }}"),
                });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/arcgis/rest/services/World/GeocodeServer/suggest")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"suggestions\":[" +
                        "{ \"text\":\"123 East, 3046 Dolphin Dr, Elizabethtown, KY, 42701, USA\", \"magicKey\":\"dHA9MCNsb2M9OTk2MzI1I2xuZz0zMyNwbD0yOTIyODAjbGJzPTE0OjU2ODczNg==\", \"isCollection\":false }," +
                        "{ \"text\":\"123 East Rd, Martinsburg, WV, 25404, USA\", \"magicKey\":\"dHA9MCNsb2M9NDkwODAjbG5nPTMzI2huPTEyMyNsYnM9MTA5OjQ1MTcxMzQ3\", \"isCollection\":false }," +
                        "{ \"text\":\"123 East Ave NE, Coeburn, VA, 24230, USA\", \"magicKey\":\"dHA9MCNsb2M9MTQ0OTYzI2xuZz0zMyNobj0xMjMjbGJzPTEwOTo0NTE3MTE4Nw==\", \"isCollection\":false }," +
                        "{ \"text\":\"123 East Ave, Hampton, VA, 23661, USA\", \"magicKey\":\"dHA9MCNsb2M9MjUwMjE3I2xuZz0zMyNobj0xMjMjbGJzPTEwOTo0NTE3MTE4NQ==\", \"isCollection\":false }," +
                        "{ \"text\":\"123 East Ave, Middlebourne, WV, 26149, USA\", \"magicKey\":\"dHA9MCNsb2M9NTM3NDQjbG5nPTMzI2huPTEyMyNsYnM9MTA5OjQ1MTcxMTg1\", \"isCollection\":false }]}"),
                });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/arcgis/rest/services/World/GeocodeServer/findAddressCandidates") && x.RequestUri.PathAndQuery.Contains("singleLine")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"spatialReference\":{\"wkid\":4326,\"latestWkid\":4326},\"candidates\":[{\"address\":\"123 East\",\"location\":{\"x\":-85.837039999999945,\"y\":37.710620000000063}," +
                        "\"score\":100,\"attributes\":{\"Match_addr\":\"123 East\",\"Addr_type\":\"POI\"},\"extent\":{\"xmin\":-85.84203999999994,\"ymin\":37.70562000000006,\"xmax\":-85.832039999999949,\"ymax\":37.715620000000065}}]}"),
                });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/arcgis/rest/services/World/GeocodeServer/findAddressCandidates") && x.RequestUri.PathAndQuery.Contains("category")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"spatialReference\":{\"wkid\":4326,\"latestWkid\":4326},\"candidates\":[]}"),
                });
        }

        /// <summary>
        /// Tests the key is properly set into the query string.
        /// </summary>
        [Test]
        public void AddHereKeySuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer);
            var query = new NameValueCollection();

            service.AddHereKey(query);
            query.Count.Should().Be(1);
            query["apiKey"].Should().Be("abc123");
        }

        /// <summary>
        /// Tests the base parameters is properly set into the query string.
        /// </summary>
        [Test]
        public void AddBaseParametersSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer);
            var query = new NameValueCollection();
            var parameters = new BaseParameters()
            {
                Language = "es",
            };

            service.AddBaseParameters(parameters, query);
            query.Count.Should().Be(1);
            query["lang"].Should().Be("es");
        }

        /// <summary>
        /// Tests the base filter parameters is properly set into the query string.
        /// </summary>
        [Test]
        public void AddLimitingParametersSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer);
            var query = new NameValueCollection();
            var parameters = new BaseFilterParameters()
            {
                Limit = 17,
                Language = "de",
            };

            service.AddLimitingParameters(parameters, query);
            query.Count.Should().Be(2);
            query["limit"].Should().Be("17");
            query["lang"].Should().Be("de");
        }

        /// <summary>
        /// Tests the locating parameters is properly set into the query string.
        /// </summary>
        [Test]
        public void AddLocatingParametersSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer);
            var query = new NameValueCollection();
            var parameters = new BaseFilterParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = "fr",
            };

            service.AddLocatingParameters(parameters, query);
            query.Count.Should().Be(3);
            query["at"].Should().Be("56.789,123.456");
            query["limit"].Should().Be("91");
            query["lang"].Should().Be("fr");
        }

        /// <summary>
        /// Tests the bounding parameters is properly set into the query string.
        /// </summary>
        [Test]
        public void AddBoundingParametersSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer);
            var query = new NameValueCollection();
            var parameters = new AreaParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = "fr",
            };

            service.AddBoundingParameters(parameters, query);
            query.Count.Should().Be(3);
            query["at"].Should().Be("56.789,123.456");
            query["limit"].Should().Be("91");
            query["lang"].Should().Be("fr");

            query.Clear();
            parameters = new AreaParameters()
            {
                InCountry = "BEL",
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = "nl",
            };

            service.AddBoundingParameters(parameters, query);
            query.Count.Should().Be(4);
            query["in"].Should().Be("countryCode:BEL");
            query["at"].Should().Be("56.789,123.456");
            query["limit"].Should().Be("91");
            query["lang"].Should().Be("nl");

            query.Clear();
            parameters = new AreaParameters()
            {
                InCircle = new Circle()
                {
                    Centre = new Coordinate()
                    {
                        Latitude = 78.9,
                        Longitude = 45.32,
                    },
                    Radius = 50000,
                },
                Limit = 83,
                Language = "ga",
            };

            service.AddBoundingParameters(parameters, query);
            query.Count.Should().Be(3);
            query["in"].Should().Be("circle:78.9,45.32;r=50000");
            query["limit"].Should().Be("83");
            query["lang"].Should().Be("ga");

            query.Clear();
            parameters = new AreaParameters()
            {
                InCountry = "DNK",
                InCircle = new Circle()
                {
                    Centre = new Coordinate()
                    {
                        Latitude = 48.9,
                        Longitude = 15.32,
                    },
                    Radius = 6000,
                },
                Limit = 48,
                Language = "da",
            };

            service.AddBoundingParameters(parameters, query);
            query.Count.Should().Be(3);
            query["in"].Should().Be("countryCode:DNK,circle:48.9,15.32;r=6000");
            query["limit"].Should().Be("48");
            query["lang"].Should().Be("da");

            query.Clear();
            parameters = new AreaParameters()
            {
                InBoundingBox = new BoundingBox()
                {
                    North = 89.99,
                    South = 87.99,
                    West = -1.5,
                    East = 1.5,
                },
                Limit = 65,
                Language = "cs",
            };

            service.AddBoundingParameters(parameters, query);
            query.Count.Should().Be(3);
            query["in"].Should().Be("bbox:-1.5,87.99,1.5,89.99");
            query["limit"].Should().Be("65");
            query["lang"].Should().Be("cs");

            query.Clear();
            parameters = new AreaParameters()
            {
                InCountry = "POL",
                InBoundingBox = new BoundingBox()
                {
                    North = 54.99,
                    South = 45.99,
                    West = -43.5,
                    East = -39.5,
                },
                Limit = 33,
                Language = "pl",
            };

            service.AddBoundingParameters(parameters, query);
            query.Count.Should().Be(3);
            query["in"].Should().Be("countryCode:POL,bbox:-43.5,45.99,-39.5,54.99");
            query["limit"].Should().Be("33");
            query["lang"].Should().Be("pl");
        }

        /// <summary>
        /// Tests the bounding parameter are not set into the query properly.
        /// </summary>
        [Test]
        public void AddBoundingParametersWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer);
            var query = new NameValueCollection();
            Action act = () => service.AddBoundingParameters(new AreaParameters(), query);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The combination of bounding parameters is not valid.");

            var parameters = new AreaParameters()
            {
                InCountry = "POL",
                InBoundingBox = new BoundingBox()
                {
                    North = 54.99,
                    South = 45.99,
                    West = -43.5,
                    East = -39.5,
                },
                InCircle = new Circle()
                {
                    Centre = new Coordinate()
                    {
                        Latitude = 48.9,
                        Longitude = 15.32,
                    },
                    Radius = 6000,
                },
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 33,
                Language = "pl",
            };

            act = () => service.AddBoundingParameters(new AreaParameters(), query);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The combination of bounding parameters is not valid.");
        }
    }
}
