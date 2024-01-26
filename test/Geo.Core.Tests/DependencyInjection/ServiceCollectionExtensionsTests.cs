// <copyright file="ServiceCollectionExtensionsTests.cs" company="Geo.NET">
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
    /// Test cases the <see cref="ServiceCollectionExtensions"/> class.
    /// </summary>
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddKeyOptions_WithNullServices_ThrowsException()
        {
            // Arrange
            Action act = () => ((IServiceCollection)null).AddKeyOptions<TestClass>();

            // Act/Assert
            act
                .Should()
                .Throw<ArgumentNullException>()
#if NET48
                .WithMessage("*Parameter name: services");
#else
                .WithMessage("Value cannot be null. (Parameter 'services')");
#endif
        }

        [Fact]
        public void AddKeyOptions_WithNoConfiguration_CorrectlyRegistersService()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddKeyOptions<TestClass>();

            // Assert
            var provider = services.BuildServiceProvider();
            var options = provider.GetService<IOptions<KeyOptions<TestClass>>>();
            options.Value.Should().NotBeNull();
            options.Value.Key.Should().BeEmpty();
        }

        [Fact]
        public void AddKeyOptions_WithValidConfiguration_CorrectlyRegistersService()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddKeyOptions<TestClass>(x => x.Key = "Test123");

            // Assert
            var provider = services.BuildServiceProvider();
            var options = provider.GetService<IOptions<KeyOptions<TestClass>>>();
            options.Value.Should().NotBeNull();
            options.Value.Key.Should().Be("Test123");
        }

        [Fact]
        public void AddClientCredentialsOptions_WithNullServices_ThrowsException()
        {
            // Arrange
            Action act = () => ((IServiceCollection)null).AddClientCredentialsOptions<TestClass>();

            // Act/Assert
            act
                .Should()
                .Throw<ArgumentNullException>()
#if NET48
                .WithMessage("*Parameter name: services");
#else
                .WithMessage("Value cannot be null. (Parameter 'services')");
#endif
        }

        [Fact]
        public void AddClientCredentialsOptions_WithNoConfiguration_CorrectlyRegistersService()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddClientCredentialsOptions<TestClass>();

            // Assert
            var provider = services.BuildServiceProvider();
            var options = provider.GetService<IOptions<ClientCredentialsOptions<TestClass>>>();
            options.Value.Should().NotBeNull();
            options.Value.ClientId.Should().BeEmpty();
            options.Value.ClientSecret.Should().BeEmpty();
        }

        [Fact]
        public void AddClientCredentialsOptions_WithValidConfiguration_CorrectlyRegistersService()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddClientCredentialsOptions<TestClass>(x =>
            {
                x.ClientId = "Test123";
                x.ClientSecret = "123Test";
            });

            // Assert
            var provider = services.BuildServiceProvider();
            var options = provider.GetService<IOptions<ClientCredentialsOptions<TestClass>>>();
            options.Value.Should().NotBeNull();
            options.Value.ClientId.Should().Be("Test123");
            options.Value.ClientSecret.Should().Be("123Test");
        }
    }
}
