// <copyright file="ServiceCollectionExtensionsTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Tests.DependencyInjection
{
    using System;
    using System.Net.Http;
    using FluentAssertions;
    using Geo.Extensions.DependencyInjection;
    using Geo.Nominatim.Services;
    using Geo.Nominatim.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="ServiceCollectionExtensions"/> class.
    /// </summary>
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddNominatimGeocoding_WithValidCall_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var builder = services.AddNominatimGeocoding();
            builder.AddEmail("test@example.com");

            // Assert
            var provider = services.BuildServiceProvider();

            var options = provider.GetRequiredService<IOptions<NominatimOptions>>();
            options.Should().NotBeNull();
            options.Value.Email.Should().Be("test@example.com");
            provider.GetRequiredService<INominatimGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddNominatimGeocoding_WithNullOptions_ConfiguresAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddNominatimGeocoding();

            // Assert
            var provider = services.BuildServiceProvider();

            var options = provider.GetRequiredService<IOptions<NominatimOptions>>();
            options.Should().NotBeNull();
            options.Value.Email.Should().BeNull();
            provider.GetRequiredService<INominatimGeocoding>().Should().NotBeNull();
        }

        [Fact]
        public void AddNominatimGeocoding_WithClientConfiguration_ConfiguresHttpClientAllServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var builder = services.AddNominatimGeocoding();
            builder.AddEmail("test@example.com");
            builder.HttpClientBuilder.ConfigureHttpClient(httpClient => httpClient.Timeout = TimeSpan.FromSeconds(6));

            // Assert
            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("INominatimGeocoding");
            client.Timeout.Should().Be(TimeSpan.FromSeconds(6));
        }

        [Fact]
        public void AddEmail_WithValidEmail_ConfiguresEmail()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var builder = services.AddNominatimGeocoding();
            builder.AddEmail("user@domain.com");

            // Assert
            var provider = services.BuildServiceProvider();

            var options = provider.GetRequiredService<IOptions<NominatimOptions>>();
            options.Should().NotBeNull();
            options.Value.Email.Should().Be("user@domain.com");
        }

        [Fact]
        public void AddEmail_WithNullEmail_ThrowsException()
        {
            // Arrange
            var services = new ServiceCollection();
            var builder = services.AddNominatimGeocoding();

            // Act & Assert
            Action act = () => builder.AddEmail(null);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The email cannot be null or empty");
        }

        [Fact]
        public void AddEmail_WithEmptyEmail_ThrowsException()
        {
            // Arrange
            var services = new ServiceCollection();
            var builder = services.AddNominatimGeocoding();

            // Act & Assert
            Action act = () => builder.AddEmail(string.Empty);

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The email cannot be null or empty");
        }

        [Fact]
        public void AddEmail_WithWhiteSpaceEmail_ThrowsException()
        {
            // Arrange
            var services = new ServiceCollection();
            var builder = services.AddNominatimGeocoding();

            // Act & Assert
            Action act = () => builder.AddEmail("   ");

            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The email cannot be null or empty");
        }
    }
}
