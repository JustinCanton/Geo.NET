// <copyright file="MapboxKeyContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Tests.Services
{
    using FluentAssertions;
    using Geo.MapBox.Services;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="MapBoxKeyContainer"/> class.
    /// </summary>
    [TestFixture]
    public class MapboxKeyContainerShould
    {
        /// <summary>
        /// Tests the key is properly set.
        /// </summary>
        [Test]
        public void SetKey()
        {
            var keyContainer = new MapBoxKeyContainer("abc123");
            keyContainer.GetKey().Should().Be("abc123");
        }
    }
}