// <copyright file="ServiceCollectionExtensionsTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Tests.DependencyInjection
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Geo.MapQuest.Abstractions;
    using Geo.MapQuest.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ServiceCollectionExtensions"/> class.
    /// </summary>
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddMapQuestServices_WithValidCall_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddMapQuestServices(options => options.UseKey("abc"));

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IMapQuestKeyContainer>().Should().NotBeNull();
            provider.GetRequiredService<IMapQuestEndpoint>().Should().NotBeNull();
            provider.GetRequiredService<IMapQuestGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddMapQuestServices_WithNullOptions_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddMapQuestServices(null);

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IMapQuestKeyContainer>().Should().NotBeNull();
            provider.GetRequiredService<IMapQuestEndpoint>().Should().NotBeNull();
            provider.GetRequiredService<IMapQuestGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddMapQuestServices_WithClientConfiguration_ConfiguresHttpClientAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddMapQuestServices(
                options => options.UseKey("abc"),
                client => client.Timeout = TimeSpan.FromSeconds(6));

            // Assert
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("IMapQuestGeocoding");
            client.Timeout.Should().Be(TimeSpan.FromSeconds(6));
        }
    }
}
