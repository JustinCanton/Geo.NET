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
    using Geo.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
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
            var builder = services.AddArcGISGeocoding();
            builder.AddClientCredentials("abc", "123");

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IOptions<ClientCredentialsOptions<IArcGISGeocoding>>>().Should().NotBeNull();
            provider.GetRequiredService<IArcGISTokenProvider>().Should().NotBeNull();
            provider.GetRequiredService<IArcGISGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddArcGISServices_WithNullOptions_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddArcGISGeocoding();

            // Assert
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<IOptions<ClientCredentialsOptions<IArcGISGeocoding>>>().Should().NotBeNull();
            provider.GetRequiredService<IArcGISTokenProvider>().Should().NotBeNull();
            provider.GetRequiredService<IArcGISGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddArcGISServices_WithClientConfiguration_ConfiguresHttpClientAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var builder = services.AddArcGISGeocoding();
            builder.AddClientCredentials("abc", "123");
            builder.HttpClientBuilder.ConfigureHttpClient(httpClient => httpClient.Timeout = TimeSpan.FromSeconds(1));

            // Assert
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("IArcGISGeocoding");
            client.Timeout.Should().Be(TimeSpan.FromSeconds(1));
        }
    }
}
