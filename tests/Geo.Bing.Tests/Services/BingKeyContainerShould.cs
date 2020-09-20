﻿// <copyright file="BingKeyContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Tests.Services
{
    using FluentAssertions;
    using Geo.Bing.Services;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="BingKeyContainer"/> class.
    /// </summary>
    [TestFixture]
    public class BingKeyContainerShould
    {
        /// <summary>
        /// Tests the key is properly set.
        /// </summary>
        [Test]
        public void SetKey()
        {
            var keyContainer = new BingKeyContainer("abc123");
            keyContainer.GetKey().Should().Be("abc123");
        }
    }
}