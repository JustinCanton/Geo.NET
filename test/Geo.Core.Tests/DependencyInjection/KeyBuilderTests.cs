// <copyright file="KeyBuilderTests.cs" company="Geo.NET">
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
    /// Test cases for the <see cref="KeyBuilder{T}"/> class.
    /// </summary>
    public class KeyBuilderTests
    {
        [Fact]
        public void Ctor_WithNullHttpClientBuilder_ThrowsException()
        {
            // Arrange
            Action act = () => new KeyBuilder<TestClass>(null);

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
            var builder = new KeyBuilder<TestClass>(services.AddHttpClient<TestClass>());

            // Act
            Action act1 = () => builder.AddKey(null);
            Action act2 = () => builder.AddKey(string.Empty);
            Action act3 = () => builder.AddKey(" ");

            // Assert
            act1
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("The API key cannot be null or empty");

            act2
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("The API key cannot be null or empty");

            act3
                .Should()
                .Throw<ArgumentException>()
                .WithMessage("The API key cannot be null or empty");
        }

        [Fact]
        public void AddClientCredentials_WithValidClientCredentials_CorrectlyConfiguresServices()
        {
            // Arrange
            var services = new ServiceCollection();
            var builder = new KeyBuilder<TestClass>(services.AddHttpClient<TestClass>());

            // Act
            builder.AddKey("Test123");

            // Assert
            var provider = services.BuildServiceProvider();
            var options = provider.GetService<IOptions<KeyOptions<TestClass>>>();
            options.Value.Should().NotBeNull();
            options.Value.Key.Should().Be("Test123");
        }
    }
}
