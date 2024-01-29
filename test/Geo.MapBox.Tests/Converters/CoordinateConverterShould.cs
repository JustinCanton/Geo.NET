﻿// <copyright file="CoordinateConverterShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Tests.Converters
{
    using System.Text.Json;
    using FluentAssertions;
    using Geo.MapBox.Converters;
    using Geo.MapBox.Tests.Models;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="CoordinateConverter"/> class.
    /// </summary>
    public class CoordinateConverterShould
    {
        /// <summary>
        /// Tests the double array is successfully translated to a coordinate.
        /// </summary>
        [Fact]
        public void CorrectlyParseCoordinate()
        {
            var obj = JsonSerializer.Deserialize<CoordinateObject>("{\"Coordinate\":[40.752777282429321,-73.996387763584124]}");
            obj.Coordinate.Longitude.Should().Be(40.752777282429321);
            obj.Coordinate.Latitude.Should().Be(-73.996387763584124);
        }
    }
}