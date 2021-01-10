// <copyright file="GoogleKeyContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Tests.Services
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