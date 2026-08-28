// <copyright file="ServiceCollectionExtensionsTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Tests.DependencyInjection
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
        public void AddGeoapifyGeocoding_WithValidCall_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var builder = services.AddGeoapifyGeocoding();
            builder.AddKey("abc");

            // Assert
            var provider = services.BuildServiceProvider();

            var options = provider.GetRequiredService<IOptions<KeyOptions<IGeoapifyGeocoding>>>();
            options.Should().NotBeNull();
            options.Value.Key.Should().Be("abc");
            provider.GetRequiredService<IGeoapifyGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddGeoapifyGeocoding_WithNullOptions_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddGeoapifyGeocoding();

            // Assert
            var provider = services.BuildServiceProvider();

            var options = provider.GetRequiredService<IOptions<KeyOptions<IGeoapifyGeocoding>>>();
            options.Should().NotBeNull();
            options.Value.Key.Should().Be(string.Empty);
            provider.GetRequiredService<IGeoapifyGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddGeoapifyGeocoding_WithClientConfiguration_ConfiguresHttpClientAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var builder = services.AddGeoapifyGeocoding();
            builder.AddKey("abc");
            builder.HttpClientBuilder.ConfigureHttpClient(httpClient => httpClient.Timeout = TimeSpan.FromSeconds(5));

            // Assert
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("IGeoapifyGeocoding");
            client.Timeout.Should().Be(TimeSpan.FromSeconds(5));
        }
    }
}
