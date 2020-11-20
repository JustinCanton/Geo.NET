// <copyright file="CoordinateConverterShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Tests.Converters
{
    using FluentAssertions;
    using Geo.MapBox.Converters;
    using Geo.MapBox.Tests.Models;
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
            var obj = JsonConvert.DeserializeObject<CoordinateObject>("{'Coordinate':[40.752777282429321,-73.996387763584124]}");
            obj.Coordinate.Longitude.Should().Be(40.752777282429321);
            obj.Coordinate.Latitude.Should().Be(-73.996387763584124);
        }
    }
}