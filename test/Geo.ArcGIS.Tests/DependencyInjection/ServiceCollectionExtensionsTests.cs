// <copyright file="ServiceCollectionExtensionsTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Tests.DependencyInjection
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Geo.ArcGIS.Abstractions;
    using Geo.ArcGIS.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ServiceCollectionExtensions"/> class.
    /// </summary>
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddArcGISServices_WithValidCall_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddArcGISServices(options => options.UseClientCredentials("abc", "123"));

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IArcGISCredentialsContainer>().Should().NotBeNull();
            provider.GetRequiredService<IArcGISTokenRetrevial>().Should().NotBeNull();
            provider.GetRequiredService<IArcGISTokenContainer>().Should().NotBeNull();
            provider.GetRequiredService<IArcGISGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddArcGISServices_WithNullOptions_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddArcGISServices(null);

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IArcGISCredentialsContainer>().Should().NotBeNull();
            provider.GetRequiredService<IArcGISTokenRetrevial>().Should().NotBeNull();
            provider.GetRequiredService<IArcGISTokenContainer>().Should().NotBeNull();
            provider.GetRequiredService<IArcGISGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddArcGISServices_WithClientConfiguration_ConfiguresHttpClientAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddArcGISServices(
                options => options.UseClientCredentials("abc", "123"),
                client => client.Timeout = TimeSpan.FromSeconds(1));

            // Assert
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("IArcGISGeocoding");
            client.Timeout.Should().Be(TimeSpan.FromSeconds(1));
        }
    }
}
