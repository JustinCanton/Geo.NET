// <copyright file="ArcGISTokenContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Tests.Services
{
    using System.Threading;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.Models;
    using Geo.ArcGIS.Services;
    using Moq;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ArcGISTokenContainer"/> class.
    /// </summary>
    public class ArcGISTokenContainerShould
    {
        private readonly Mock<IArcGISTokenRetrevial> _mockTokenRetrevial;
        private readonly CancellationToken _quickExpireToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArcGISTokenContainerShould"/> class.
        /// </summary>
        public ArcGISTokenContainerShould()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            _quickExpireToken = source.Token;

            _mockTokenRetrevial = new Mock<IArcGISTokenRetrevial>();

            _mockTokenRetrevial
                .Setup(x => x.GetTokenAsync(
                    It.Is<CancellationToken>(y => y == CancellationToken.None)))
                .ReturnsAsync(
                    new Token() { AccessToken = "key123", ExpiresIn = 100 });

            _mockTokenRetrevial
                .Setup(x => x.GetTokenAsync(
                    It.Is<CancellationToken>(y => y == _quickExpireToken)))
                .ReturnsAsync(
                    new Token() { AccessToken = "key456", ExpiresIn = 5 });
        }

        /// <summary>
        /// Tests the token is retrieved and returned.
        /// </summary>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Fact]
        public async Task GetTokenSuccessfully()
        {
            var tokenContainer = new ArcGISTokenContainer(_mockTokenRetrevial.Object);

            var token = await tokenContainer.GetTokenAsync(CancellationToken.None).ConfigureAwait(false);
            token.Should().Be("key123");
        }

        /// <summary>
        /// Tests the same token is returned if the expiry time hasn't passed.
        /// </summary>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Fact]
        public async Task GetTokenTwice()
        {
            var tokenContainer = new ArcGISTokenContainer(_mockTokenRetrevial.Object);

            var token = await tokenContainer.GetTokenAsync(CancellationToken.None).ConfigureAwait(false);
            token.Should().Be("key123");

            var tokenAgain = await tokenContainer.GetTokenAsync(_quickExpireToken).ConfigureAwait(false);
            tokenAgain.Should().Be("key123");
        }

        /// <summary>
        /// Tests a different token is returned if the expiry time has passed.
        /// </summary>
        /// <returns>A <see cref="Task"/> with the results.</returns>
        [Fact]
        public async Task GetTokenExpiredAndRefetched()
        {
            var tokenContainer = new ArcGISTokenContainer(_mockTokenRetrevial.Object);

            var token = await tokenContainer.GetTokenAsync(_quickExpireToken).ConfigureAwait(false);
            token.Should().Be("key456");

            var tokenAgain = await tokenContainer.GetTokenAsync(CancellationToken.None).ConfigureAwait(false);
            tokenAgain.Should().Be("key123");
        }
    }
}
