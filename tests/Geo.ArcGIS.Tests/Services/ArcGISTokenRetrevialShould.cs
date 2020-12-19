// <copyright file="ArcGISTokenRetrevialShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Tests.Services
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Geo.ArcGIS.Services;
    using Moq;
    using Moq.Protected;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="ArcGISTokenRetrevial"/> class.
    /// </summary>
    [TestFixture]
    public class ArcGISTokenRetrevialShould
    {
        private Mock<HttpMessageHandler> _handlerMock;
        private ArcGISCredentialsContainer _keyContainer;

        /// <summary>
        /// One time setup information.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _keyContainer = new ArcGISCredentialsContainer("abc123", "secret123");

            _handlerMock = new Mock<HttpMessageHandler>();
            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{'access_token':'1234567890abc','expires_in':15000}"),
                });
        }

        /// <summary>
        /// Tests the content for the token retrieval request is built successfully with the expected data.
        /// </summary>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Test]
        public async Task BuildContentSuccessfully()
        {
            using var httpClient = new HttpClient(_handlerMock.Object);
            var service = new ArcGISTokenRetrevial(httpClient, _keyContainer);

            var content = service.BuildContent();
            content.Headers.ContentType.ToString().Should().Be("application/x-www-form-urlencoded");

            var collection = await content.ReadAsStringAsync().ConfigureAwait(false);
            collection.Should().Contain("client_id=abc123")
                .And.Contain("client_secret=secret123")
                .And.Contain("grant_type=client_credentials");
        }

        /// <summary>
        /// Tests the get token method returns an empty token if there is an invalid key.
        /// </summary>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Test]
        public async Task GetTokenShouldReturnEmptyWithNoKey()
        {
            using var httpClient = new HttpClient(_handlerMock.Object);
            var keyContainer = new ArcGISCredentialsContainer(string.Empty, string.Empty);
            var service = new ArcGISTokenRetrevial(httpClient, keyContainer);

            var token = await service.GetTokenAsync(CancellationToken.None).ConfigureAwait(false);
            token.AccessToken.Should().Be(string.Empty);
            token.ExpiresIn.Should().Be(int.MaxValue);
        }

        /// <summary>
        /// Tests the get token method returns a token if everything works correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Test]
        public async Task GetTokenShouldReturnValidToken()
        {
            using var httpClient = new HttpClient(_handlerMock.Object);
            var service = new ArcGISTokenRetrevial(httpClient, _keyContainer);

            var token = await service.GetTokenAsync(CancellationToken.None).ConfigureAwait(false);
            token.AccessToken.Should().Be("1234567890abc");
            token.ExpiresIn.Should().Be(15000);
        }
    }
}
