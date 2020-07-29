// <copyright file="GoogleKeyContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Tests
{
    using FluentAssertions;
    using Geo.Google.Services;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="GoogleKeyContainer"/> class.
    /// </summary>
    [TestFixture]
    public class GoogleKeyContainerShould
    {
        /// <summary>
        /// Tests the key is properly set.
        /// </summary>
        [Test]
        public void SetKey()
        {
            var keyContainer = new GoogleKeyContainer("abc123");
            keyContainer.GetKey().Should().Be("abc123");
        }
    }
}