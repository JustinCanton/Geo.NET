// <copyright file="GeoNETExceptionProviderTests.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Core.Tests
{
    using System;
    using FluentAssertions;
    using Geo.Core.Tests.Models;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="GeoNETExceptionProvider"/> class.
    /// </summary>
    public class GeoNETExceptionProviderTests
    {
        [Fact]
        public void GetException_WithoutInnerException_ReturnsException()
        {
            // Arrange
            var sut = new GeoNETExceptionProvider();

            // Act
            var ex = sut.GetException<TestException>("Test exception");

            // Assert
            ex.Message.Should().Be("Test exception");
            ex.InnerException.Should().BeNull();
        }

        [Fact]
        public void GetException_WithInnerException_ReturnsException()
        {
            // Arrange
            var sut = new GeoNETExceptionProvider();

            // Act
            var ex = sut.GetException<TestException>("Test exception", new ArgumentNullException());

            // Assert
            ex.Message.Should().Be("Test exception");
            ex.InnerException.Should().NotBeNull();
            ex.InnerException.Should().BeOfType<ArgumentNullException>();
        }
    }
}
