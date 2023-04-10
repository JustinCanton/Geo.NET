// <copyright file="ServiceCollectionExtensionsTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Tests.DependencyInjection
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Geo.Here.Abstractions;
    using Geo.Here.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ServiceCollectionExtensions"/> class.
    /// </summary>
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddHereServices_WithValidCall_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddHereServices(options => options.UseKey("abc"));

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IHereKeyContainer>().Should().NotBeNull();
            provider.GetRequiredService<IHereGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddHereServices_WithNullOptions_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddHereServices(null);

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IHereKeyContainer>().Should().NotBeNull();
            provider.GetRequiredService<IHereGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddHereServices_WithClientConfiguration_ConfiguresHttpClientAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddHereServices(
                options => options.UseKey("abc"),
                client => client.Timeout = TimeSpan.FromSeconds(4));

            // Assert
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("IHereGeocoding");
            client.Timeout.Should().Be(TimeSpan.FromSeconds(4));
        }
    }
}
