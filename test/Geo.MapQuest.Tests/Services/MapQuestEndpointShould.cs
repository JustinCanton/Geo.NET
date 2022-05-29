// <copyright file="MapQuestEndpointShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Tests.Services
{
    using FluentAssertions;
    using Geo.MapQuest.Services;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="MapQuestEndpoint"/> class.
    /// </summary>
    public class MapQuestEndpointShould
    {
        /// <summary>
        /// Tests the licensed endpoint is properly set.
        /// </summary>
        [Fact]
        public void SetLicensedEndpoint()
        {
            var endpoint = new MapQuestEndpoint(true);
            endpoint.UseLicensedEndpoint().Should().BeTrue();
        }

        /// <summary>
        /// Tests the non-licensed endpoint is properly set.
        /// </summary>
        [Fact]
        public void SetNonLicensedEndpoint()
        {
            var endpoint = new MapQuestEndpoint(false);
            endpoint.UseLicensedEndpoint().Should().BeFalse();
        }
    }
}