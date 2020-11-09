// <copyright file="ArcGISKeyContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Tests.Services
{
    using FluentAssertions;
    using Geo.ArcGIS.Services;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="ArcGISKeyContainerShould"/> class.
    /// </summary>
    [TestFixture]
    public class ArcGISKeyContainerShould
    {
        /// <summary>
        /// Tests the key is properly set.
        /// </summary>
        [Test]
        public void SetKey()
        {
            var keyContainer = new ArcGISKeyContainer("abc123", "secret123");
            var keys = keyContainer.GetKeys();
            keys.Item1.Should().Be("abc123");
            keys.Item2.Should().Be("secret123");
        }
    }
}