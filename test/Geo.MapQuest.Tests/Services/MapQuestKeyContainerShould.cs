// <copyright file="MapQuestKeyContainerShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Tests.Services
{
    using FluentAssertions;
    using Geo.MapQuest.Services;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="MapQuestKeyContainer"/> class.
    /// </summary>
    public class MapQuestKeyContainerShould
    {
        /// <summary>
        /// Tests the key is properly set.
        /// </summary>
        [Fact]
        public void SetKey()
        {
            var keyContainer = new MapQuestKeyContainer("abc123");
            keyContainer.GetKey().Should().Be("abc123");
        }
    }
}