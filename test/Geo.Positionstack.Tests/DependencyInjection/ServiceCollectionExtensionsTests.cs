// <copyright file="ServiceCollectionExtensionsTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Tests.DependencyInjection
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Geo.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ServiceCollectionExtensions"/> class.
    /// </summary>
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddPositionstackGeocoding_WithValidCall_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var builder = services.AddPositionstackGeocoding();
            builder.AddKey("abc");

            // Assert
            var provider = services.BuildServiceProvider();

            var options = provider.GetRequiredService<IOptions<KeyOptions<IPositionstackGeocoding>>>();
            options.Should().NotBeNull();
            options.Value.Key.Should().Be("abc");
            provider.GetRequiredService<IPositionstackGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddPositionstackGeocoding_WithNullOptions_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddPositionstackGeocoding();

            // Assert
            var provider = services.BuildServiceProvider();

            var options = provider.GetRequiredService<IOptions<KeyOptions<IPositionstackGeocoding>>>();
            options.Should().NotBeNull();
            options.Value.Key.Should().Be(string.Empty);
            provider.GetRequiredService<IPositionstackGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddPositionstackGeocoding_WithClientConfiguration_ConfiguresHttpClientAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var builder = services.AddPositionstackGeocoding();
            builder.AddKey("abc");
            builder.HttpClientBuilder.ConfigureHttpClient(httpClient => httpClient.Timeout = TimeSpan.FromSeconds(5));

            // Assert
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("IPositionstackGeocoding");
            client.Timeout.Should().Be(TimeSpan.FromSeconds(5));
        }
    }
}
