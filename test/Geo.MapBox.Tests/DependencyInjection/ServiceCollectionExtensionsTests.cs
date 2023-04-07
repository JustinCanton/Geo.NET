// <copyright file="ServiceCollectionExtensionsTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Tests.DependencyInjection
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Geo.MapBox.Abstractions;
    using Geo.MapBox.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ServiceCollectionExtensions"/> class.
    /// </summary>
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddMapBoxServices_WithValidCall_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddMapBoxServices(options => options.UseKey("abc"));

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IMapBoxKeyContainer>().Should().NotBeNull();
            provider.GetRequiredService<IMapBoxGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddMapBoxServices_WithNullOptions_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddMapBoxServices(null);

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IMapBoxKeyContainer>().Should().NotBeNull();
            provider.GetRequiredService<IMapBoxGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddMapBoxServices_WithClientConfiguration_ConfiguresHttpClientAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddMapBoxServices(
                options => options.UseKey("abc"),
                client => client.Timeout = TimeSpan.FromSeconds(5));

            // Assert
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("IMapBoxGeocoding");
            client.Timeout.Should().Be(TimeSpan.FromSeconds(5));
        }
    }
}
