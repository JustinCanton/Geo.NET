// <copyright file="CoordinateConverterShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Tests.Converters
{
    using FluentAssertions;
    using Geo.Bing.Converters;
    using Geo.Bing.Tests.Models;
    using Newtonsoft.Json;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="CoordinateConverter"/> class.
    /// </summary>
    [TestFixture]
    public class CoordinateConverterShould
    {
        /// <summary>
        /// Tests the double array is successfully translated to a coordinate.
        /// </summary>
        [Test]
        public void CorrectlyParseCoordinate()
        {
            var obj = JsonConvert.DeserializeObject<CoordinateObject>("{'Point':[43.6477298,-79.3802169]}");
            obj.Point.Latitude.Should().Be(43.6477298);
            obj.Point.Longitude.Should().Be(-79.3802169);
        }
    }
}