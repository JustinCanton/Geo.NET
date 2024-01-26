// <copyright file="ClientCredentialsBuilderTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests.DependencyInjection
{
    using System;
    using FluentAssertions;
    using Geo.Core.Tests.Models;
    using Geo.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Xunit;

    /// <summary>
    /// Test cases for the <see cref="ClientCredentialsBuilder{T}"/> class.
    /// </summary>
    public class ClientCredentialsBuilderTests
    {
        [Fact]
        public void Ctor_WithNullHttpClientBuilder_ThrowsException()
        {
            // Arrange
            Action act = () => new ClientCredentialsBuilder<TestClass>(null);

            // Act/Assert
            act
                .Should()
                .Throw<ArgumentNullException>()
#if NET48
                .WithMessage("*Parameter name: httpClientBuilder");
#else
                .WithMessage("Value cannot be null. (Parameter 'httpClientBuilder')");
#endif
        }

        [Fact]
        public void AddClientCredentials_WithInvalidClientCredentials_ThrowsException()
        {
            // Arrange
            var services = new ServiceCollection();
            var builder = new ClientCredentialsBuilder<TestClass>(services.AddHttpClient<TestClass>());

            // Act
            Action act1 = () => builder.AddClientCredentials(null, "value");
            Action act2 = () => builder.AddClientCredentials(string.Empty, "value");
            Action act3 = () => builder.AddClientCredentials("value", null);
            Action act4 = () => builder.AddClientCredentials("value", " ");
            Action act5 = () => builder.AddClientCredentials(null, string.Empty);

            // Assert
            act1
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("The API client id cannot be null or empty");

            act2
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("The API client id cannot be null or empty");

            act3
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("The API client secret cannot be null or empty");

            act4
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("The API client secret cannot be null or empty");

            act5
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("The API client id cannot be null or empty");
        }

        [Fact]
        public void AddClientCredentials_WithValidClientCredentials_CorrectlyConfiguresServices()
        {
            // Arrange
            var services = new ServiceCollection();
            var builder = new ClientCredentialsBuilder<TestClass>(services.AddHttpClient<TestClass>());

            // Act
            builder.AddClientCredentials("Test123", "123Test");

            // Assert
            var provider = services.BuildServiceProvider();
            var options = provider.GetService<IOptions<ClientCredentialsOptions<TestClass>>>();
            options.Value.Should().NotBeNull();
            options.Value.ClientId.Should().Be("Test123");
            options.Value.ClientSecret.Should().Be("123Test");
        }
    }
}
