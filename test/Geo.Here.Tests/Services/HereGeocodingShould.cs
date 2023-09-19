// <copyright file="HereGeocodingShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Tests.Services
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
    using Geo.Here.Models;
    using Geo.Here.Models.Exceptions;
    using Geo.Here.Models.Parameters;
    using Geo.Here.Services;
    using Microsoft.Extensions.Localization;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="HereGeocoding"/> class.
    /// </summary>
    public class HereGeocodingShould : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly HereKeyContainer _keyContainer;
        private readonly IGeoNETExceptionProvider _exceptionProvider;
        private readonly IGeoNETResourceStringProviderFactory _resourceStringProviderFactory;
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="HereGeocodingShould"/> class.
        /// </summary>
        public HereGeocodingShould()
        {
            _keyContainer = new HereKeyContainer("abc123");

            var mockHandler = new Mock<HttpMessageHandler>();

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                        "{\"items\":" +
                        "[{\"title\":\"123 East Hill, London, SW18 2QB, England\",\"id\":\"here: af:streetsection: Op7KzT7e2gH8G99rgRWJUB:EAIaAzEyMyhk\",\"resultType\":\"houseNumber\",\"houseNumberType\":\"interpolated\"," +
                        "\"address\":{\"label\":\"123 East Hill, London, SW18 2QB, England\",\"countryCode\":\"GBR\",\"countryName\":\"England\",\"countyCode\":\"LDN\",\"county\":\"London\",\"city\":\"London\"," +
                        "\"district\":\"Wandsworth\",\"street\":\"East Hill\",\"postalCode\":\"SW18 2QB\",\"houseNumber\":\"123\"},\"position\":{\"lat\":51.45697,\"lng\":-0.18835},\"access\":[{\"lat\":51.4571,\"lng\":-0.18842}]," +
                        "\"mapView\":{\"west\":-0.18979,\"south\":51.45607,\"east\":-0.18691,\"north\":51.45787},\"scoring\":{\"queryScore\":0.99,\"fieldScore\":{\"streets\":[0.9],\"houseNumber\":1.0}}}]}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/geocode")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                        "{\"items\":" +
                        "[{\"title\":\"Royal Oak\",\"id\":\"here: pds:place: 826gcpue - d78485b762734169a8d1b4ac2311fd8f\",\"resultType\":\"place\"," +
                        "\"address\":{\"label\":\"Royal Oak, 135 East Hill, London, SW18 2, England\",\"countryCode\":\"GBR\",\"countryName\":\"England\",\"countyCode\":\"LDN\",\"county\":\"London\",\"city\":\"London\"," +
                        "\"district\":\"Wandsworth\",\"street\":\"East Hill\",\"postalCode\":\"SW18 2\",\"houseNumber\":\"135\"},\"position\":{\"lat\":51.45696,\"lng\":-0.18844},\"access\":[{\"lat\":51.45709,\"lng\":-0.18847}]," +
                        "\"distance\":6,\"categories\":[{\"id\":\"200 - 2000 - 0011\",\"name\":\"Bar or Pub\",\"primary\":true},{\"id\":\"100 - 1000 - 0000\",\"name\":\"Restaurant\"}]}]}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/revgeocode")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                        "{\"items\":" +
                        "[{\"title\":\"123 East India Dock Road, London, E14 0, England\",\"id\":\"here: af:streetsection: ku2Yqc - Y2Nto7kjX9fjdPC:CggIBCD9_YPsAhABGgMxMjMoZA\",\"resultType\":\"houseNumber\"," +
                        "\"houseNumberType\":\"PA\",\"address\":{\"label\":\"123 East India Dock Road, London, E14 0, England\",\"countryCode\":\"GBR\",\"countryName\":\"England\",\"countyCode\":\"LDN\"," +
                        "\"county\":\"London\",\"city\":\"London\",\"district\":\"Poplar\",\"street\":\"East India Dock Road\",\"postalCode\":\"E14 0\",\"houseNumber\":\"123\"},\"position\":{\"lat\":51.51144," +
                        "\"lng\":-0.01137},\"access\":[{\"lat\":51.51129,\"lng\":-0.01135}],\"distance\":3452941,\"mapView\":{\"west\":-0.02104,\"south\":51.51082,\"east\":0.00436,\"north\":51.51477}}]}"),
            });

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/discover")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _responseMessages.Add(new HttpResponseMessage()
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

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/autosuggest")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _responseMessages.Add(new HttpResponseMessage()
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

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/browse")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

            _responseMessages.Add(new HttpResponseMessage()
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

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.PathAndQuery.Contains("/v1/lookup")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);

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
        /// Tests the key is properly set into the query string.
        /// </summary>
        [Fact]
        public void AddHereKeySuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;

            sut.AddHereKey(ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(1);
            queryParameters["apiKey"].Should().Be("abc123");
        }

        /// <summary>
        /// Tests the base parameters is properly set into the query string.
        /// </summary>
        [Fact]
        public void AddBaseParametersSuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new BaseParameters()
            {
                Language = new CultureInfo("es"),
                PoliticalView = "IND",
            };
            parameters.Show.Add("countryInfo");
            parameters.Show.Add("streetInfo");

            sut.AddBaseParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(3);
            queryParameters["lang"].Should().Be("es");
            queryParameters["politicalView"].Should().Be("IND");
            queryParameters["show"].Should().Be("countryInfo,streetInfo");
        }

        [Fact]
        public void AddBaseParameters_WithInvariantCulture_IgnoresLanguage()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new BaseParameters()
            {
                Language = CultureInfo.InvariantCulture,
                PoliticalView = "IND",
            };
            parameters.Show.Add("countryInfo");
            parameters.Show.Add("streetInfo");

            sut.AddBaseParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(2);
            queryParameters["politicalView"].Should().Be("IND");
            queryParameters["show"].Should().Be("countryInfo,streetInfo");
        }

        /// <summary>
        /// Tests the base filter parameters is properly set into the query string.
        /// </summary>
        [Fact]
        public void AddLimitingParametersSuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new BaseFilterParameters()
            {
                Limit = 17,
                Language = new CultureInfo("da"),
            };

            sut.AddLimitingParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(2);
            queryParameters["limit"].Should().Be("17");
            queryParameters["lang"].Should().Be("da");
        }

        /// <summary>
        /// Tests the locating parameters is properly set into the query string.
        /// </summary>
        [Fact]
        public void AddLocatingParametersSuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new BaseFilterParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = new CultureInfo("fr-FR"),
            };

            sut.AddLocatingParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(3);
            queryParameters["at"].Should().Be("56.789,123.456");
            queryParameters["limit"].Should().Be("91");
            queryParameters["lang"].Should().Be("fr-FR");
        }

        /// <summary>
        /// Tests the bounding parameters is properly set into the query string.
        /// </summary>
        [Fact]
        public void AddBoundingParametersSuccessfully()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new AreaParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = new CultureInfo("fr"),
            };

            sut.AddBoundingParameters(parameters, ref query);

            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(3);
            queryParameters["at"].Should().Be("56.789,123.456");
            queryParameters["limit"].Should().Be("91");
            queryParameters["lang"].Should().Be("fr");

            query = QueryString.Empty;
            parameters = new AreaParameters()
            {
                InCountry = "BEL",
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = new CultureInfo("nl"),
            };

            sut.AddBoundingParameters(parameters, ref query);

            queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(4);
            queryParameters["in"].Should().Be("countryCode:BEL");
            queryParameters["at"].Should().Be("56.789,123.456");
            queryParameters["limit"].Should().Be("91");
            queryParameters["lang"].Should().Be("nl");

            query = QueryString.Empty;
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
                Language = new CultureInfo("gl"),
            };

            sut.AddBoundingParameters(parameters, ref query);

            queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(3);
            queryParameters["in"].Should().Be("circle:78.9,45.32;r=50000");
            queryParameters["limit"].Should().Be("83");
            queryParameters["lang"].Should().Be("gl");

            query = QueryString.Empty;
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
                Language = new CultureInfo("da"),
            };

            sut.AddBoundingParameters(parameters, ref query);

            queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(3);
            queryParameters["in"].Should().Be("countryCode:DNK,circle:48.9,15.32;r=6000");
            queryParameters["limit"].Should().Be("48");
            queryParameters["lang"].Should().Be("da");

            query = QueryString.Empty;
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
                Language = new CultureInfo("ca"),
            };

            sut.AddBoundingParameters(parameters, ref query);

            queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(3);
            queryParameters["in"].Should().Be("bbox:-1.5,87.99,1.5,89.99");
            queryParameters["limit"].Should().Be("65");
            queryParameters["lang"].Should().Be("ca");

            query = QueryString.Empty;
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
                Language = new CultureInfo("pl"),
            };

            sut.AddBoundingParameters(parameters, ref query);

            queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(3);
            queryParameters["in"].Should().Be("countryCode:POL,bbox:-43.5,45.99,-39.5,54.99");
            queryParameters["limit"].Should().Be("33");
            queryParameters["lang"].Should().Be("pl");
        }

        [Fact]
        public void AddBoundingParameters_WithFlexiblePolyline_CorrectlyAddsPolyline()
        {
            // Arrange
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new AreaParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = new CultureInfo("fr"),
                FlexiblePolyline = new FlexiblePolyline()
                {
                    Coordinates = new List<LatLngZ>()
                    {
                        new LatLngZ(52.5199356, 13.3866272),
                        new LatLngZ(52.5100899, 13.2816896),
                        new LatLngZ(52.4351807, 13.1935196),
                        new LatLngZ(52.4107285, 13.1964502),
                        new LatLngZ(52.38871, 13.1557798),
                        new LatLngZ(52.3727798, 13.1491003),
                        new LatLngZ(52.3737488, 13.1154604),
                        new LatLngZ(52.3875198, 13.0872202),
                        new LatLngZ(52.4029388, 13.0706196),
                        new LatLngZ(52.4105797, 13.0755529),
                    },
                    Precision = 5,
                    ThirdDimension = ThirdDimension.Absent,
                    ThirdDimensionPrecision = 0,
                },
            };

            // Act
            sut.AddBoundingParameters(parameters, ref query);

            // Assert
            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(4);
            queryParameters["at"].Should().Be("56.789,123.456");
            queryParameters["limit"].Should().Be("91");
            queryParameters["lang"].Should().Be("fr");
            queryParameters["route"].Should().Be("BF05xgKuy2xCx9B7vUl0OhnR54EqSzpEl-HxjD3pBiGnyGi2CvwFsgD3nD4vB6e");
        }

        [Fact]
        public void AddBoundingParameters_WithRoute_CorrectlyAddsRoute()
        {
            // Arrange
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new AreaParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = new CultureInfo("fr"),
                Route = "BlBoz5xJ67i1BU1B7PUzIhaUxL7YU",
            };

            // Act
            sut.AddBoundingParameters(parameters, ref query);

            // Assert
            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(4);
            queryParameters["at"].Should().Be("56.789,123.456");
            queryParameters["limit"].Should().Be("91");
            queryParameters["lang"].Should().Be("fr");
            queryParameters["route"].Should().Be("BlBoz5xJ67i1BU1B7PUzIhaUxL7YU");
        }

        [Fact]
        public void AddBoundingParameters_WithFlexiblePolylineAndRoute_CorrectlyAddsPolyline()
        {
            // Arrange
            var sut = BuildService();

            var query = QueryString.Empty;
            var parameters = new AreaParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = new CultureInfo("fr"),
                FlexiblePolyline = new FlexiblePolyline()
                {
                    Coordinates = new List<LatLngZ>()
                    {
                        new LatLngZ(52.5199356, 13.3866272),
                        new LatLngZ(52.5100899, 13.2816896),
                        new LatLngZ(52.4351807, 13.1935196),
                        new LatLngZ(52.4107285, 13.1964502),
                        new LatLngZ(52.38871, 13.1557798),
                        new LatLngZ(52.3727798, 13.1491003),
                        new LatLngZ(52.3737488, 13.1154604),
                        new LatLngZ(52.3875198, 13.0872202),
                        new LatLngZ(52.4029388, 13.0706196),
                        new LatLngZ(52.4105797, 13.0755529),
                    },
                    Precision = 5,
                    ThirdDimension = ThirdDimension.Absent,
                    ThirdDimensionPrecision = 0,
                },
                Route = "BlBoz5xJ67i1BU1B7PUzIhaUxL7YU",
            };

            // Act
            sut.AddBoundingParameters(parameters, ref query);

            // Assert
            var queryParameters = HttpUtility.ParseQueryString(query.ToString());
            queryParameters.Count.Should().Be(4);
            queryParameters["at"].Should().Be("56.789,123.456");
            queryParameters["limit"].Should().Be("91");
            queryParameters["lang"].Should().Be("fr");
            queryParameters["route"].Should().Be("BF05xgKuy2xCx9B7vUl0OhnR54EqSzpEl-HxjD3pBiGnyGi2CvwFsgD3nD4vB6e");
        }

        /// <summary>
        /// Tests the bounding parameter are not set into the query properly.
        /// </summary>
        [Fact]
        public void AddBoundingParametersWithException()
        {
            var sut = BuildService();

            var query = QueryString.Empty;
            Action act = () => sut.AddBoundingParameters(new AreaParameters(), ref query);

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'parameters')");
#else
                .WithMessage("*Parameter name: parameters");
#endif

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
                Language = new CultureInfo("pl"),
            };

            act = () => sut.AddBoundingParameters(new AreaParameters(), ref query);

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'parameters')");
#else
                .WithMessage("*Parameter name: parameters");
#endif
        }

        /// <summary>
        /// Tests the building of the geocoding parameters is done successfully.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildGeocodingRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new GeocodeParameters()
            {
                Query = "123 East",
                QualifiedQuery = "123 West",
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = new CultureInfo("da"),
            };

            parameters.InCountry.Add(new RegionInfo("DK"));
            parameters.InCountry.Add(new RegionInfo("JP"));
            parameters.InCountry.Add(new RegionInfo("RS"));

            parameters.Types.Add("address");
            parameters.Types.Add("area");

            // Act
            var uri = sut.BuildGeocodingRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("q=123 East");
            query.Should().Contain("qq=123 West");
            query.Should().Contain("in=countryCode:DNK,JPN,SRB");
            query.Should().Contain("at=56.789,123.456");
            query.Should().Contain("types=address,area");
            query.Should().Contain("limit=91");
            query.Should().Contain("lang=da");
            query.Should().Contain("apiKey=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the building of the geocoding parameters fails if no query is provided.
        /// </summary>
        [Fact]
        public void BuildGeocodingRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildGeocodingRequest(new GeocodeParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'parameters')");
#else
                .WithMessage("*Parameter name: parameters");
#endif
        }

        /// <summary>
        /// Tests the building of the reverse geocoding parameters is done successfully.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildReverseGeocodingRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new ReverseGeocodeParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 76.789,
                    Longitude = -12.456,
                },
                Limit = 1,
                Language = new CultureInfo("en"),
            };

            parameters.Types.Add("city");
            parameters.Types.Add("area");

            // Act
            var uri = sut.BuildReverseGeocodingRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("at=76.789,-12.456");
            query.Should().Contain("types=city,area");
            query.Should().Contain("limit=1");
            query.Should().Contain("lang=en");
            query.Should().Contain("apiKey=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        [Fact]
        public void BuildReverseGeocodingRequest_WithInCircleParameter_BuildsSuccessfully()
        {
            // Arrange
            var sut = BuildService();

            var parameters = new ReverseGeocodeParameters()
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
                Limit = 1,
                Language = new CultureInfo("en"),
            };

            parameters.Types.Add("city");
            parameters.Types.Add("area");

            // Act
            var uri = sut.BuildReverseGeocodingRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("in=circle:78.9,45.32;r=50000");
            query.Should().Contain("types=city,area");
            query.Should().Contain("limit=1");
            query.Should().Contain("lang=en");
            query.Should().Contain("apiKey=abc123");
        }

        /// <summary>
        /// Tests the building of the reverse geocoding parameters fails if no query is provided.
        /// </summary>
        [Fact]
        public void BuildReverseGeocodingRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildReverseGeocodingRequest(new ReverseGeocodeParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'parameters')");
#else
                .WithMessage("*Parameter name: parameters");
#endif
        }

        /// <summary>
        /// Tests the building of the reverse geocoding parameters fails if no query is provided.
        /// </summary>
        [Fact]
        public void BuildReverseGeocodingRequest_WithBothInAndAt_FailsWithException()
        {
            var sut = BuildService();
            var parameters = new ReverseGeocodeParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 76.789,
                    Longitude = -12.456,
                },
                InCircle = new Circle()
                {
                    Centre = new Coordinate()
                    {
                        Latitude = 78.9,
                        Longitude = 45.32,
                    },
                    Radius = 50000,
                },
            };

            Action act = () => sut.BuildReverseGeocodingRequest(parameters);

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'parameters')");
#else
                .WithMessage("*Parameter name: parameters");
#endif
        }

        /// <summary>
        /// Tests the building of the discover parameters is done successfully.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildDiscoverRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

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
                Language = new CultureInfo("pl"),
            };

            // Act
            var uri = sut.BuildDiscoverRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("q=123 East");
            query.Should().Contain("in=countryCode:POL");
            query.Should().Contain("in=bbox:-43.5,45.99,-39.5,54.99");
            query.Should().Contain("limit=33");
            query.Should().Contain("lang=pl");
            query.Should().Contain("apiKey=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the building of the discover parameters fails if no query is provided.
        /// </summary>
        [Fact]
        public void BuildDiscoverRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildDiscoverRequest(new DiscoverParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Query')");
#else
                .WithMessage("*Parameter name: Query");
#endif
        }

        /// <summary>
        /// Tests the building of the autosuggest parameters is done successfully.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildAutosuggestRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

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
                Language = new CultureInfo("en"),
            };

            // Act
            var uri = sut.BuildAutosuggestRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("q=123 Weast");
            query.Should().Contain("termsLimit=7");
            query.Should().Contain("in=countryCode:CAD");
            query.Should().Contain("in=bbox:-43.1,45.2,-39.1,54.2");
            query.Should().Contain("limit=44");
            query.Should().Contain("lang=en");
            query.Should().Contain("apiKey=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the building of the autosuggest parameters fails if no query is provided.
        /// </summary>
        [Fact]
        public void BuildAutosuggestRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildAutosuggestRequest(new AutosuggestParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Query')");
#else
                .WithMessage("*Parameter name: Query");
#endif
        }

        /// <summary>
        /// Tests the building of the browse parameters is done successfully.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildBrowseRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

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
                Language = new CultureInfo("en"),
            };

            // Act
            var uri = sut.BuildBrowseRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("categories=Resturants");
            query.Should().Contain("name=Place");
            query.Should().Contain("at=54.2,45.2");
            query.Should().Contain("in=countryCode:CAD");
            query.Should().Contain("limit=44");
            query.Should().Contain("lang=en");
            query.Should().Contain("apiKey=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the building of the browse parameters fails if no at is provided.
        /// </summary>
        [Fact]
        public void BuildBrowseRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildBrowseRequest(new BrowseParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'At')");
#else
                .WithMessage("*Parameter name: At");
#endif
        }

        /// <summary>
        /// Tests the building of the lookup parameters is done successfully.
        /// </summary>
        /// <param name="culture">The culture to set the current running thread to.</param>
        [Theory]
        [ClassData(typeof(CultureTestData))]
        public void BuildLookupRequestSuccessfully(CultureInfo culture)
        {
            // Arrange
            var oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = culture;

            var sut = BuildService();

            var parameters = new LookupParameters()
            {
                Id = "12345sudfinm",
                Language = new CultureInfo("ja"),
            };

            // Act
            var uri = sut.BuildLookupRequest(parameters);

            // Assert
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("id=12345sudfinm");
            query.Should().Contain("lang=ja");
            query.Should().Contain("apiKey=abc123");

            Thread.CurrentThread.CurrentCulture = oldCulture;
        }

        /// <summary>
        /// Tests the building of the lookup parameters fails if no id is provided.
        /// </summary>
        [Fact]
        public void BuildLookupRequestFailsWithException()
        {
            var sut = BuildService();

            Action act = () => sut.BuildLookupRequest(new LookupParameters());

            act.Should()
                .Throw<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Id')");
#else
                .WithMessage("*Parameter name: Id");
#endif
        }

        /// <summary>
        /// Tests the validation and creation of the lookup uri is done successfully.
        /// </summary>
        [Fact]
        public void ValidateAndCraftUriSuccessfully()
        {
            var sut = BuildService();

            var parameters = new LookupParameters()
            {
                Id = "12345sudfinm",
                Language = new CultureInfo("ja"),
            };

            var uri = sut.ValidateAndBuildUri<LookupParameters>(parameters, sut.BuildLookupRequest);
            var query = HttpUtility.UrlDecode(uri.PathAndQuery);
            query.Should().Contain("id=12345sudfinm");
            query.Should().Contain("lang=ja");
            query.Should().Contain("apiKey=abc123");
        }

        /// <summary>
        /// Tests the validation and creation of the lookup uri fails if the parameters are null.
        /// </summary>
        [Fact]
        public void ValidateAndCraftUriFailsWithException1()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<LookupParameters>(null, sut.BuildLookupRequest);

            act.Should()
                .Throw<HereException>()
                .WithMessage("*See the inner exception for more information.")
                .WithInnerException<ArgumentNullException>();
        }

        /// <summary>
        /// Tests the validation and creation of the lookup uri fails if no id is provided and the exception is wrapped in a here exception.
        /// </summary>
        [Fact]
        public void ValidateAndCraftUriFailsWithException2()
        {
            var sut = BuildService();

            Action act = () => sut.ValidateAndBuildUri<LookupParameters>(new LookupParameters(), sut.BuildLookupRequest);

            act.Should()
                .Throw<HereException>()
                .WithMessage("*See the inner exception for more information.")
                .WithInnerException<ArgumentException>()
#if NETCOREAPP3_1_OR_GREATER
                .WithMessage("*(Parameter 'Id')");
#else
                .WithMessage("*Parameter name: Id");
#endif
        }

        /// <summary>
        /// Tests the geocoding call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task GeocodingAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new GeocodeParameters()
            {
                Query = "123 East",
                QualifiedQuery = "123 West",
                At = new Coordinate()
                {
                    Latitude = 56.789,
                    Longitude = 123.456,
                },
                Limit = 91,
                Language = new CultureInfo("da"),
            };

            parameters.InCountry.Add(new RegionInfo("DK"));

            var result = await sut.GeocodingAsync(parameters).ConfigureAwait(false);
            result.Items.Count.Should().Be(1);
        }

        /// <summary>
        /// Tests the reverse geocoding call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task ReverseGeocodingAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new ReverseGeocodeParameters()
            {
                At = new Coordinate()
                {
                    Latitude = 76.789,
                    Longitude = -12.456,
                },
                Limit = 1,
                Language = new CultureInfo("en"),
            };

            var result = await sut.ReverseGeocodingAsync(parameters).ConfigureAwait(false);
            result.Items.Count.Should().Be(1);
        }

        /// <summary>
        /// Tests the discover call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task DiscoverAsyncSuccessfully()
        {
            var sut = BuildService();

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
                Language = new CultureInfo("pl"),
            };

            var result = await sut.DiscoverAsync(parameters).ConfigureAwait(false);
            result.Items.Count.Should().Be(1);
        }

        /// <summary>
        /// Tests the autosuggest call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task AutosuggestAsyncSuccessfully()
        {
            var sut = BuildService();

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
                Language = new CultureInfo("en"),
            };

            var result = await sut.AutosuggestAsync(parameters).ConfigureAwait(false);
            result.Items.Count.Should().Be(2);
        }

        /// <summary>
        /// Tests the lookup call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task LookupAsyncSuccessfully()
        {
            var sut = BuildService();

            var parameters = new LookupParameters()
            {
                Id = "12345sudfinm",
                Language = new CultureInfo("ja"),
            };

            var result = await sut.LookupAsync(parameters).ConfigureAwait(false);
            result.Title.Should().Be("Royal Oak");
            result.Id.Should().Be("here: pds:place: 826gcpue - d78485b762734169a8d1b4ac2311fd8f");
        }

        /// <summary>
        /// Tests the browse call returns successfully.
        /// </summary>
        /// <returns>A <see cref="Task"/>.</returns>
        [Fact]
        public async Task BrowseAsyncSuccessfully()
        {
            var sut = BuildService();

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
                Language = new CultureInfo("en"),
            };

            var result = await sut.BrowseAsync(parameters).ConfigureAwait(false);
            result.Items.Count.Should().Be(2);
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

        private HereGeocoding BuildService()
        {
            return new HereGeocoding(_httpClient, _keyContainer, _exceptionProvider, _resourceStringProviderFactory);
        }
    }
}
