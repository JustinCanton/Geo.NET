// <copyright file="HereKeyContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Tests.Services
{
    using FluentAssertions;
    using Geo.Here.Services;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="HereKeyContainer"/> class.
    /// </summary>
    [TestFixture]
    public class HereKeyContainerShould
    {
        /// <summary>
        /// Tests the key is properly set.
        /// </summary>
        [Test]
        public void SetKey()
        {
            var keyContainer = new HereKeyContainer("abc123");
            keyContainer.GetKey().Should().Be("abc123");
        }
    }
}