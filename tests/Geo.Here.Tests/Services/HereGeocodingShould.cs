// <copyright file="HereGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Tests.Services
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using FluentAssertions;
    using Geo.Core;
    using Geo.Here.Models;
    using Geo.Here.Models.Exceptions;
    using Geo.Here.Models.Parameters;
    using Geo.Here.Services;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.Extensions.Options;
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
        private IStringLocalizer<HereGeocoding> _localizer;
        private IStringLocalizer<ClientExecutor> _coreLocalizer;

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
                        "{\"items\":" +
                        "[{\"title\":\"123 East Hill, London, SW18 2QB, England\",\"id\":\"here: af:streetsection: Op7KzT7e2gH8G99rgRWJUB:EAIaAzEyMyhk\",\"resultType\":\"houseNumber\",\"houseNumberType\":\"interpolated\"," +
                        "\"address\":{\"label\":\"123 East Hill, London, SW18 2QB, England\",\"countryCode\":\"GBR\",\"countryName\":\"England\",\"countyCode\":\"LDN\",\"county\":\"London\",\"city\":\"London\"," +
                        "\"district\":\"Wandsworth\",\"street\":\"East Hill\",\"postalCode\":\"SW18 2QB\",\"houseNumber\":\"123\"},\"position\":{\"lat\":51.45697,\"lng\":-0.18835},\"access\":[{\"lat\":51.4571,\"lng\":-0.18842}]," +
                        "\"mapView\":{\"west\":-0.18979,\"south\":51.45607,\"east\":-0.18691,\"north\":51.45787},\"scoring\":{\"queryScore\":0.99,\"fieldScore\":{\"streets\":[0.9],\"houseNumber\":1.0}}}]}"),
                });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/revgeocode")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        "{\"items\":" +
                        "[{\"title\":\"Royal Oak\",\"id\":\"here: pds:place: 826gcpue - d78485b762734169a8d1b4ac2311fd8f\",\"resultType\":\"place\"," +
                        "\"address\":{\"label\":\"Royal Oak, 135 East Hill, London, SW18 2, England\",\"countryCode\":\"GBR\",\"countryName\":\"England\",\"countyCode\":\"LDN\",\"county\":\"London\",\"city\":\"London\"," +
                        "\"district\":\"Wandsworth\",\"street\":\"East Hill\",\"postalCode\":\"SW18 2\",\"houseNumber\":\"135\"},\"position\":{\"lat\":51.45696,\"lng\":-0.18844},\"access\":[{\"lat\":51.45709,\"lng\":-0.18847}]," +
                        "\"distance\":6,\"categories\":[{\"id\":\"200 - 2000 - 0011\",\"name\":\"Bar or Pub\",\"primary\":true},{\"id\":\"100 - 1000 - 0000\",\"name\":\"Restaurant\"}]}]}"),
                });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/discover")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        "{\"items\":" +
                        "[{\"title\":\"123 East India Dock Road, London, E14 0, England\",\"id\":\"here: af:streetsection: ku2Yqc - Y2Nto7kjX9fjdPC:CggIBCD9_YPsAhABGgMxMjMoZA\",\"resultType\":\"houseNumber\"," +
                        "\"houseNumberType\":\"PA\",\"address\":{\"label\":\"123 East India Dock Road, London, E14 0, England\",\"countryCode\":\"GBR\",\"countryName\":\"England\",\"countyCode\":\"LDN\"," +
                        "\"county\":\"London\",\"city\":\"London\",\"district\":\"Poplar\",\"street\":\"East India Dock Road\",\"postalCode\":\"E14 0\",\"houseNumber\":\"123\"},\"position\":{\"lat\":51.51144," +
                        "\"lng\":-0.01137},\"access\":[{\"lat\":51.51129,\"lng\":-0.01135}],\"distance\":3452941,\"mapView\":{\"west\":-0.02104,\"south\":51.51082,\"east\":0.00436,\"north\":51.51477}}]}"),
                });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/autosuggest")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        "{\"items\":" +
                        "[{\"title\":\"123 East Coast Rd, Singapore 428808, Singapore\",\"id\":\"here: af:streetsection: e0X59kKoq7enolh0iTsyvB:CggIBCDU9MSlARABGgMxMjMoZA\",\"resultType\":\"houseNumber\"," +
                        "\"houseNumberType\":\"PA\",\"address\":{\"label\":\"123 East Coast Rd, Singapore 428808, Singapore\"},\"position\":{\"lat\":1.306,\"lng\":103.90468},\"access\":[{\"lat\":1.30587,\"lng\":103.90473}]," +
                        "\"distance\":7413232,\"mapView\":{\"west\":103.90204,\"south\":1.30201,\"east\":103.91773,\"north\":1.31018},\"highlights\":{\"title\":[{\"start\":0,\"end\":3},{\"start\":4,\"end\":8}]," +
                        "\"address\":{\"label\":[{\"start\":0,\"end\":3},{\"start\":4,\"end\":8}]}}},{\"title\":\"123 East Hill, London, SW18 2QB, England\",\"id\":\"here: af:streetsection: Op7KzT7e2gH8G99rgRWJUB:EAIaAzEyMyhk\"," +
                        "\"resultType\":\"houseNumber\",\"houseNumberType\":\"interpolated\",\"address\":{\"label\":\"123 East Hill, London, SW18 2QB, England\"},\"position\":{\"lat\":51.45697,\"lng\":-0.18835}," +
                        "\"access\":[{\"lat\":51.4571,\"lng\":-0.18842}],\"distance\":3466446,\"mapView\":{\"west\":-0.18966,\"south\":51.45683,\"east\":-0.17991,\"north\":51.45962}," +
                        "\"highlights\":{\"title\":[{\"start\":0,\"end\":3},{\"start\":4,\"end\":8}],\"address\":{\"label\":[{\"start\":0,\"end\":3},{\"start\":4,\"end\":8}]}}}],\"queryTerms\":[]}"),
                });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/browse")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        "{\"items\":" +
                        "[{\"title\":\"Саида Әсем\",\"id\":\"here: pds:place: 398jx7ps - 9d1139f0b66a0fc43cdb4f0aacf1c8ee\",\"resultType\":\"place\",\"address\":{\"label\":\"Саида Әсем, Жаңақала ауданы, Қазақстан\"," +
                        "\"countryCode\":\"KAZ\",\"countryName\":\"Қазақстан\",\"county\":\"Батыс Қазақстан облысы\",\"city\":\"Жаңақала ауданы\",\"district\":\"Қызылоба\"},\"position\":{\"lat\":49.6511,\"lng\":50.64354}," +
                        "\"access\":[{\"lat\":49.65112,\"lng\":50.64352}],\"distance\":60301,\"categories\":[{\"id\":\"600 - 6000 - 0061\",\"name\":\"Азық - түлік дүкені\",\"primary\":true},{\"id\":\"100 - 1000 - 0000\"," +
                        "\"name\":\"Мейрамхана\"}],\"references\":[{\"supplier\":{\"id\":\"core\"},\"id\":\"1175170365\"},{\"supplier\":{\"id\":\"core\"},\"id\":\"1175170366\"}],\"foodTypes\":[{\"id\":\"800 - 064\"," +
                        "\"name\":\"Халықаралық\",\"primary\":true}]},{\"title\":\"Кафе\",\"id\":\"here: pds:place: 398jx7ps - 01389f86bfca0514c45361a326c22cad\",\"resultType\":\"place\"," +
                        "\"address\":{\"label\":\"Кафе, Ақжайық ауданы, Қазақстан\",\"countryCode\":\"KAZ\",\"countryName\":\"Қазақстан\",\"county\":\"Батыс Қазақстан облысы\",\"city\":\"Ақжайық ауданы\"}," +
                        "\"position\":{\"lat\":50.21434,\"lng\":51.12206},\"access\":[{\"lat\":50.21433,\"lng\":51.12205}],\"distance\":83493,\"categories\":[{\"id\":\"100 - 1000 - 0000\",\"name\":\"Мейрамхана\",\"primary\":true}]," +
                        "\"references\":[{\"supplier\":{\"id\":\"core\"},\"id\":\"1175964311\"}],\"foodTypes\":[{\"id\":\"800 - 064\",\"name\":\"Халықаралық\",\"primary\":true}]}]}"),
                });

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/lookup")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(
                        "{\"title\":\"Royal Oak\",\"id\":\"here: pds:place: 826gcpue - d78485b762734169a8d1b4ac2311fd8f\",\"resultType\":\"place\",\"address\":{\"label\":\"Royal Oak, 135 East Hill, London, SW18 2, England\"," +
                        "\"countryCode\":\"GBR\",\"countryName\":\"England\",\"countyCode\":\"LDN\",\"county\":\"London\",\"city\":\"London\",\"district\":\"Wandsworth\",\"street\":\"East Hill\",\"postalCode\":\"SW18 2\"," +
                        "\"houseNumber\":\"135\"},\"position\":{\"lat\":51.45696,\"lng\":-0.18844},\"access\":[{\"lat\":51.45709,\"lng\":-0.18847}],\"categories\":[{\"id\":\"200 - 2000 - 0011\",\"name\":\"Bar or Pub\",\"primary\":true}," +
                        "{\"id\":\"100 - 1000 - 0000\",\"name\":\"Restaurant\"}],\"references\":[{\"supplier\":{\"id\":\"yelp\"},\"id\":\"3cGjx7b4Kcwmaz5HNLEEHA\"},{\"supplier\":{\"id\":\"yelp\"},\"id\":\"BoDwT7dYuxxxWp0XnfloqQ\"}]," +
                        "\"contacts\":[{\"phone\":[{\"value\":\" + 442088744892\"}]}],\"openingHours\":[{\"text\":[\"Mon: 12:00 - 15:00\",\"Tue - Thu, Sat: 12:00 - 15:00, 18:00 - 23:00\",\"Fri: 12:00 - 15:00, 23:00 - 23:00\",\"Sun: 12:00 - 23:00\"]," +
                        "\"isOpen\":false,\"structured\":[{\"start\":\"T120000\",\"duration\":\"PT03H00M\",\"recurrence\":\"FREQ: DAILY; BYDAY: MO,TU,WE,TH,FR,SA\"},{\"start\":\"T180000\",\"duration\":\"PT05H00M\"," +
                        "\"recurrence\":\"FREQ: DAILY; BYDAY: TU,WE,TH,SA\"},{\"start\":\"T230000\",\"duration\":\"PT24H00M\",\"recurrence\":\"FREQ: DAILY; BYDAY: FR\"},{\"start\":\"T120000\",\"duration\":\"PT11H00M\",\"recurrence\":\"FREQ: DAILY; BYDAY: SU\"}]}]}"),
                });

            var options = Options.Create(new LocalizationOptions { ResourcesPath = "Resources" });
            var factory = new ResourceManagerStringLocalizerFactory(options, NullLoggerFactory.Instance);
            _localizer = new StringLocalizer<HereGeocoding>(factory);
            _coreLocalizer = new StringLocalizer<ClientExecutor>(factory);
        }

        /// <summary>
        /// Tests the key is properly set into the query string.
        /// </summary>
        [Test]
        public void AddHereKeySuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
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
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
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
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
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
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
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
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
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
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
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

        /// <summary>
        /// Tests the building of the geocoding parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildGeocodingRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new GeocodeParameters()
            {
                Query = "123 East",
                QualifiedQuery = "123 West",
                InCountry = "DEN",
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = "dl",
            };

            var uri = service.BuildGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("q=123 East");
            query.Should().Contain("qq=123 West");
            query.Should().Contain("in=DEN");
            query.Should().Contain("at=56.789,123.456");
            query.Should().Contain("limit=91");
            query.Should().Contain("lang=dl");
            query.Should().Contain("apiKey=abc123");
        }

        /// <summary>
        /// Tests the building of the geocoding parameters fails if no query is provided.
        /// </summary>
        [Test]
        public void BuildGeocodingRequestFailsWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildGeocodingRequest(new GeocodeParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("Both query items (Query, QualifiedQuery) cannot be null or empty.");
        }

        /// <summary>
        /// Tests the building of the reverse geocoding parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildReverseGeocodingRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new ReverseGeocodeParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 76.789,
                    Longitude = -12.456,
                },
                Limit = 1,
                Language = "en",
            };

            var uri = service.BuildReverseGeocodingRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("at=76.789,-12.456");
            query.Should().Contain("limit=1");
            query.Should().Contain("lang=en");
            query.Should().Contain("apiKey=abc123");
        }

        /// <summary>
        /// Tests the building of the reverse geocoding parameters fails if no query is provided.
        /// </summary>
        [Test]
        public void BuildReverseGeocodingRequestFailsWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildReverseGeocodingRequest(new ReverseGeocodeParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The at coordinates cannot be null. (Parameter 'At')");
        }

        /// <summary>
        /// Tests the building of the discover parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildDiscoverRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new DiscoverParameters()
            {
                Query = "123 East",
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

            var uri = service.BuildDiscoverRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("q=123 East");
            query.Should().Contain("in=countryCode:POL");
            query.Should().Contain("in=bbox:-43.5,45.99,-39.5,54.99");
            query.Should().Contain("limit=33");
            query.Should().Contain("lang=pl");
            query.Should().Contain("apiKey=abc123");
        }

        /// <summary>
        /// Tests the building of the discover parameters fails if no query is provided.
        /// </summary>
        [Test]
        public void BuildDiscoverRequestFailsWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildDiscoverRequest(new DiscoverParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The query cannot be null. (Parameter 'Query')");
        }

        /// <summary>
        /// Tests the building of the autosuggest parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildAutosuggestRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new AutosuggestParameters()
            {
                Query = "123 Weast",
                TermsLimit = 7,
                InCountry = "CAD",
                InBoundingBox = new BoundingBox()
                {
                    North = 54.2,
                    South = 45.2,
                    West = -43.1,
                    East = -39.1,
                },
                Limit = 44,
                Language = "en",
            };

            var uri = service.BuildAutosuggestRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("q=123 Weast");
            query.Should().Contain("termsLimit=7");
            query.Should().Contain("in=countryCode:CAD");
            query.Should().Contain("in=bbox:-43.1,45.2,-39.1,54.2");
            query.Should().Contain("limit=44");
            query.Should().Contain("lang=en");
            query.Should().Contain("apiKey=abc123");
        }

        /// <summary>
        /// Tests the building of the autosuggest parameters fails if no query is provided.
        /// </summary>
        [Test]
        public void BuildAutosuggestRequestFailsWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildAutosuggestRequest(new AutosuggestParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The query cannot be null. (Parameter 'Query')");
        }

        /// <summary>
        /// Tests the building of the browse parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildBrowseRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new BrowseParameters()
            {
                Categories = "Resturants",
                Name = "Place",
                InCountry = "CAD",
                At = new Coordinate()
                {
                    Latitude = 54.2,
                    Longitude = 45.2,
                },
                Limit = 44,
                Language = "en",
            };

            var uri = service.BuildBrowseRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("categories=Resturants");
            query.Should().Contain("name=Place");
            query.Should().Contain("at=54.2,45.2");
            query.Should().Contain("in=countryCode:CAD");
            query.Should().Contain("limit=44");
            query.Should().Contain("lang=en");
            query.Should().Contain("apiKey=abc123");
        }

        /// <summary>
        /// Tests the building of the browse parameters fails if no at is provided.
        /// </summary>
        [Test]
        public void BuildBrowseRequestFailsWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildBrowseRequest(new BrowseParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The at coordinates cannot be null. (Parameter 'At')");
        }

        /// <summary>
        /// Tests the building of the lookup parameters is done successfully.
        /// </summary>
        [Test]
        public void BuildLookupRequestSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new LookupParameters()
            {
                Id = "12345sudfinm",
                Language = "jp",
            };

            var uri = service.BuildLookupRequest(parameters);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("id=12345sudfinm");
            query.Should().Contain("lang=jp");
            query.Should().Contain("apiKey=abc123");
        }

        /// <summary>
        /// Tests the building of the lookup parameters fails if no id is provided.
        /// </summary>
        [Test]
        public void BuildLookupRequestFailsWithException()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.BuildLookupRequest(new LookupParameters());

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The id cannot be null. (Parameter 'Id')");
        }

        /// <summary>
        /// Tests the validation and creation of the lookup uri is done successfully.
        /// </summary>
        [Test]
        public void ValidateAndCraftUriSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new LookupParameters()
            {
                Id = "12345sudfinm",
                Language = "jp",
            };

            var uri = service.ValidateAndBuildUri<LookupParameters>(parameters, service.BuildLookupRequest);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("id=12345sudfinm");
            query.Should().Contain("lang=jp");
            query.Should().Contain("apiKey=abc123");
        }

        /// <summary>
        /// Tests the validation and creation of the lookup uri fails if the parameters are null.
        /// </summary>
        [Test]
        public void ValidateAndCraftUriFailsWithException1()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.ValidateAndBuildUri<LookupParameters>(null, service.BuildLookupRequest);

            act.Should()
                .Throw<HereException>()
                .WithMessage("The here parameters are null. See the inner exception for more information.")
                .WithInnerException<ArgumentNullException>();
        }

        /// <summary>
        /// Tests the validation and creation of the lookup uri fails if no id is provided and the exception is wrapped in a here exception.
        /// </summary>
        [Test]
        public void ValidateAndCraftUriFailsWithException2()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            Action act = () => service.ValidateAndBuildUri<LookupParameters>(new LookupParameters(), service.BuildLookupRequest);

            act.Should()
                .Throw<HereException>()
                .WithMessage("Failed to create the here uri. See the inner exception for more information.")
                .WithInnerException<ArgumentException>()
                .WithMessage("The id cannot be null. (Parameter 'Id')");
        }

        /// <summary>
        /// Tests the geocoding call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task GeocodingAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new GeocodeParameters()
            {
                Query = "123 East",
                QualifiedQuery = "123 West",
                InCountry = "DEN",
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = "dl",
            };

            var result = await service.GeocodingAsync(parameters).ConfigureAwait(false);
            result.Items.Count.Should().Be(1);
        }

        /// <summary>
        /// Tests the reverse geocoding call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task ReverseGeocodingAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new ReverseGeocodeParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 76.789,
                    Longitude = -12.456,
                },
                Limit = 1,
                Language = "en",
            };

            var result = await service.ReverseGeocodingAsync(parameters).ConfigureAwait(false);
            result.Items.Count.Should().Be(1);
        }

        /// <summary>
        /// Tests the discover call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task DiscoverAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new DiscoverParameters()
            {
                Query = "123 East",
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

            var result = await service.DiscoverAsync(parameters).ConfigureAwait(false);
            result.Items.Count.Should().Be(1);
        }

        /// <summary>
        /// Tests the autosuggest call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task AutosuggestAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new AutosuggestParameters()
            {
                Query = "123 Weast",
                TermsLimit = 7,
                InCountry = "CAD",
                InBoundingBox = new BoundingBox()
                {
                    North = 54.2,
                    South = 45.2,
                    West = -43.1,
                    East = -39.1,
                },
                Limit = 44,
                Language = "en",
            };

            var result = await service.AutosuggestAsync(parameters).ConfigureAwait(false);
            result.Items.Count.Should().Be(2);
        }

        /// <summary>
        /// Tests the lookup call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task LookupAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new LookupParameters()
            {
                Id = "12345sudfinm",
                Language = "jp",
            };

            var result = await service.LookupAsync(parameters).ConfigureAwait(false);
            result.Title.Should().Be("Royal Oak");
            result.Id.Should().Be("here: pds:place: 826gcpue - d78485b762734169a8d1b4ac2311fd8f");
        }

        /// <summary>
        /// Tests the browse call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Test]
        public async Task BrowseAsyncSuccessfully()
        {
            using var httpClient = new HttpClient(_mockHandler.Object);
            var service = new HereGeocoding(httpClient, _keyContainer, _localizer, _coreLocalizer);
            var parameters = new BrowseParameters()
            {
                Categories = "Resturants",
                Name = "Place",
                InCountry = "CAD",
                At = new Coordinate()
                {
                    Latitude = 54.2,
                    Longitude = 45.2,
                },
                Limit = 44,
                Language = "en",
            };

            var result = await service.BrowseAsync(parameters).ConfigureAwait(false);
            result.Items.Count.Should().Be(2);
        }
    }
}
