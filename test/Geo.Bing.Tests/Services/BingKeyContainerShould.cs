﻿// <copyright file="BingKeyContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Tests.Services
{
    using FluentAssertions;
    using Geo.Bing.Services;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="BingKeyContainer"/> class.
    /// </summary>
    public class BingKeyContainerShould
    {
        /// <summary>
        /// Tests the key is properly set.
        /// </summary>
        [Fact]
        public void SetKey()
        {
            var keyContainer = new BingKeyContainer("abc123");
            keyContainer.GetKey().Should().Be("abc123");
        }
    }
}