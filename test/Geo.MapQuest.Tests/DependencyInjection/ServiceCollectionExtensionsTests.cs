// <copyright file="ServiceCollectionExtensionsTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Tests.DependencyInjection
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Geo.Extensions.DependencyInjection;
    using Geo.MapQuest.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ServiceCollectionExtensions"/> class.
    /// </summary>
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddMapQuestGeocoding_WithValidCall_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var builder = services.AddMapQuestGeocoding();
            builder.AddKey("abc");

            // Assert
            var provider = services.BuildServiceProvider();

            var options = provider.GetRequiredService<IOptions<MapQuestOptions>>();
            options.Should().NotBeNull();
            options.Value.Key.Should().Be("abc");
            options.Value.UseLicensedEndpoint.Should().BeFalse();
            provider.GetRequiredService<IMapQuestGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddMapQuestGeocoding_WithNullOptions_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddMapQuestGeocoding();

            // Assert
            var provider = services.BuildServiceProvider();

            var options = provider.GetRequiredService<IOptions<MapQuestOptions>>();
            options.Should().NotBeNull();
            options.Value.Key.Should().Be(string.Empty);
            options.Value.UseLicensedEndpoint.Should().BeFalse();
            provider.GetRequiredService<IMapQuestGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddMapQuestGeocoding_WithClientConfiguration_ConfiguresHttpClientAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var builder = services.AddMapQuestGeocoding();
            builder.AddKey("abc");
            builder.HttpClientBuilder.ConfigureHttpClient(httpClient => httpClient.Timeout = TimeSpan.FromSeconds(6));

            // Assert
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("IMapQuestGeocoding");
            client.Timeout.Should().Be(TimeSpan.FromSeconds(6));
        }

        [Fact]
        public void AddMapQuestGeocoding_WithLicensedEndpoints_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var builder = services.AddMapQuestGeocoding();
            builder.AddKey("abc");
            builder.UseLicensedEndpoints();

            // Assert
            var provider = services.BuildServiceProvider();

            var options = provider.GetRequiredService<IOptions<MapQuestOptions>>();
            options.Should().NotBeNull();
            options.Value.Key.Should().Be("abc");
            options.Value.UseLicensedEndpoint.Should().BeTrue();
            provider.GetRequiredService<IMapQuestGeocoding>().Should().NotBeNull();
        }
    }
}
