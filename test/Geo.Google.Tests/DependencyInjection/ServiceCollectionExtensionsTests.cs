// <copyright file="ServiceCollectionExtensionsTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Tests.DependencyInjection
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Geo.Google.Abstractions;
    using Geo.Google.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ServiceCollectionExtensions"/> class.
    /// </summary>
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddGoogleServices_WithValidCall_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddGoogleServices(options => options.UseKey("abc"));

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IGoogleKeyContainer>().Should().NotBeNull();
            provider.GetRequiredService<IGoogleGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddGoogleServices_WithNullOptions_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddGoogleServices(null);

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IGoogleKeyContainer>().Should().NotBeNull();
            provider.GetRequiredService<IGoogleGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddGoogleServices_WithClientConfiguration_ConfiguresHttpClientAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddGoogleServices(
                options => options.UseKey("abc"),
                httpClient => httpClient.Timeout = TimeSpan.FromSeconds(3));

            // Assert
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("IGoogleGeocoding");
            client.Timeout.Should().Be(TimeSpan.FromSeconds(3));
        }
    }
}
