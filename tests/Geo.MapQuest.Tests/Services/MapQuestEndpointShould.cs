// <copyright file="MapQuestEndpointShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Tests.Services
{
    using FluentAssertions;
    using Geo.MapQuest.Services;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="MapQuestEndpoint"/> class.
    /// </summary>
    [TestFixture]
    public class MapQuestEndpointShould
    {
        /// <summary>
        /// Tests the lisenced endpoint is properly set.
        /// </summary>
        [Test]
        public void SetLisencedEndpoint()
        {
            var endpoint = new MapQuestEndpoint(true);
            endpoint.UseLicensedEndpoint().Should().BeTrue();
        }

        /// <summary>
        /// Tests the non-lisenced endpoint is properly set.
        /// </summary>
        [Test]
        public void SetNonLisencedEndpoint()
        {
            var endpoint = new MapQuestEndpoint(false);
            endpoint.UseLicensedEndpoint().Should().BeFalse();
        }
    }
}