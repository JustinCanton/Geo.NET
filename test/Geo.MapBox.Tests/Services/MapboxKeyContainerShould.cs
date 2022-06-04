// <copyright file="MapboxKeyContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Tests.Services
{
    using FluentAssertions;
    using Geo.MapBox.Services;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="MapBoxKeyContainer"/> class.
    /// </summary>
    public class MapboxKeyContainerShould
    {
        /// <summary>
        /// Tests the key is properly set.
        /// </summary>
        [Fact]
        public void SetKey()
        {
            var keyContainer = new MapBoxKeyContainer("abc123");
            keyContainer.GetKey().Should().Be("abc123");
        }
    }
}