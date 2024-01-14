// <copyright file="ArcGISTokenRetrevialShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Geo.ArcGIS.Services;
    using Moq;
    using Moq.Protected;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ArcGISTokenRetrevial"/> class.
    /// </summary>
    public class ArcGISTokenRetrevialShould : IDisposable
    {
        private readonly Mock<HttpMessageHandler> _handlerMock;
        private readonly ArcGISCredentialsContainer _keyContainer;
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISTokenRetrevialShould"/> class.
        /// </summary>
        public ArcGISTokenRetrevialShould()
        {
            _keyContainer = new ArcGISCredentialsContainer("abc123", "secret123");

            _handlerMock = new Mock<HttpMessageHandler>();

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'access_token':'1234567890abc','expires_in':15000}"),
            });

            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1]);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Tests the content for the token retrieval request is built successfully with the expected data.
        /// </summary>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Fact]
        public async Task BuildContentSuccessfully()
        {
            using (var httpClient = new HttpClient(_handlerMock.Object))
            {
                var service = new ArcGISTokenRetrevial(httpClient, _keyContainer);

                var content = service.BuildContent();
                content.Headers.ContentType.ToString().Should().Be("application/x-www-form-urlencoded");

                var collection = await content.ReadAsStringAsync();
                collection.Should().Contain("client_id=abc123")
                    .And.Contain("client_secret=secret123")
                    .And.Contain("grant_type=client_credentials");
            }
        }

        /// <summary>
        /// Tests the get token method returns an empty token if there is an invalid key.
        /// </summary>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Fact]
        public async Task GetTokenShouldReturnEmptyWithNoKey()
        {
            using (var httpClient = new HttpClient(_handlerMock.Object))
            {
                var keyContainer = new ArcGISCredentialsContainer(string.Empty, string.Empty);
                var service = new ArcGISTokenRetrevial(httpClient, keyContainer);

                var token = await service.GetTokenAsync(CancellationToken.None);
                token.AccessToken.Should().Be(string.Empty);
                token.ExpiresIn.Should().Be(int.MaxValue);
            }
        }

        /// <summary>
        /// Tests the get token method returns a token if everything works correctly.
        /// </summary>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Fact]
        public async Task GetTokenShouldReturnValidToken()
        {
            using (var httpClient = new HttpClient(_handlerMock.Object))
            {
                var service = new ArcGISTokenRetrevial(httpClient, _keyContainer);

                var token = await service.GetTokenAsync(CancellationToken.None);
                token.AccessToken.Should().Be("1234567890abc");
                token.ExpiresIn.Should().Be(15000);
            }
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
                foreach (var message in _responseMessages)
                {
                    message?.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
