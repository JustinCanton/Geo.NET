// <copyright file="ServiceCollectionExtensionsTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Tests.DependencyInjection
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Geo.Bing.Abstractions;
    using Geo.Bing.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ServiceCollectionExtensions"/> class.
    /// </summary>
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddBingServices_WithValidCall_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddBingServices(options => options.UseKey("abc"));

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IBingKeyContainer>().Should().NotBeNull();
            provider.GetRequiredService<IBingGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddBingServices_WithNullOptions_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddBingServices(null);

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IBingKeyContainer>().Should().NotBeNull();
            provider.GetRequiredService<IBingGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddBingServices_WithClientConfiguration_ConfiguresHttpClientAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddBingServices(
                options => options.UseKey("abc"),
                httpClient => httpClient.Timeout = TimeSpan.FromSeconds(2));

            // Assert
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("IBingGeocoding");
            client.Timeout.Should().Be(TimeSpan.FromSeconds(2));
        }
    }
}
