// <copyright file="ArcGISTokenProviderShould.cs" company="Geo.NET">
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
    /// Unit tests for the <see cref="ArcGISTokenProvider"/> class.
    /// </summary>
    public class ArcGISTokenProviderShould : IDisposable
    {
        private readonly Mock<HttpMessageHandler> _handlerMock = new Mock<HttpMessageHandler>();
        private readonly List<HttpResponseMessage> _responseMessages = new List<HttpResponseMessage>();
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISTokenProviderShould"/> class.
        /// </summary>
        public ArcGISTokenProviderShould()
        {
            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"access_token\":\"1234567890abc\",\"expires_in\":15000}"),
            });

            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.Content.ReadAsStringAsync().GetAwaiter().GetResult().Contains("abc123")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1])
                .Verifiable();

            _responseMessages.Add(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"access_token\":\"1234567890def\",\"expires_in\":15000}"),
            });

            _handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(x => x.Content.ReadAsStringAsync().GetAwaiter().GetResult().Contains("abc456")),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(_responseMessages[_responseMessages.Count - 1])
                .Verifiable();
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
                var service = new ArcGISTokenProvider(httpClient);

                var content = service.BuildContent("abc123", "secret123");
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
                var service = new ArcGISTokenProvider(httpClient);

                var token = await service.GetTokenAsync(string.Empty, string.Empty, CancellationToken.None);
                token.Should().BeNull();
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
                var service = new ArcGISTokenProvider(httpClient);

                var token = await service.GetTokenAsync("abc123", "secret123", CancellationToken.None);
                token.Should().Be("1234567890abc");
            }
        }

        [Fact]
        public async Task GetTokenAsync_CalledTwice_ShouldFetchTokenOnce()
        {
            using (var httpClient = new HttpClient(_handlerMock.Object))
            {
                var service = new ArcGISTokenProvider(httpClient);

                var token = await service.GetTokenAsync("abc123", "secret123", CancellationToken.None);
                token.Should().Be("1234567890abc");

                token = await service.GetTokenAsync("abc123", "secret123", CancellationToken.None);
                token.Should().Be("1234567890abc");

                _handlerMock
                    .Protected()
                    .Verify(
                        "SendAsync",
                        Times.Once(),
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>());
            }
        }

        [Fact]
        public async Task GetTokenAsync_CalledWithDifferentClientCredentials_ShouldFetchBothTokens()
        {
            using (var httpClient = new HttpClient(_handlerMock.Object))
            {
                var service = new ArcGISTokenProvider(httpClient);

                var token = await service.GetTokenAsync("abc123", "secret123", CancellationToken.None);
                token.Should().Be("1234567890abc");

                token = await service.GetTokenAsync("abc456", "secret456", CancellationToken.None);
                token.Should().Be("1234567890def");

                // We have to check the bounds here because the data is stored statically, and isn't removed from the dictionary between tests
                // If any other test runs before this one, the data is populated and the SendAsync method is only called once
                _handlerMock
                    .Protected()
                    .Verify(
                        "SendAsync",
                        Times.AtLeast(1),
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>());

                _handlerMock
                    .Protected()
                    .Verify(
                        "SendAsync",
                        Times.AtMost(2),
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>());
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
