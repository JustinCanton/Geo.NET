// <copyright file="BoundingBoxConverterShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Tests.Converters
{
    using FluentAssertions;
    using Geo.Bing.Converters;
    using Geo.Bing.Tests.Models;
    using Newtonsoft.Json;
    using Xunit;

    /// <summary>
    /// Unit tests for the <see cref="BoundingBoxConverter"/> class.
    /// </summary>
    public class BoundingBoxConverterShould
    {
        /// <summary>
        /// Tests the double array is successfully translated to a bounding box.
        /// </summary>
        [Fact]
        public void CorrectlyParseBoundingBox()
        {
            var obj = JsonConvert.DeserializeObject<BoundingBoxObject>("{'Box':[40.752777282429321,-73.996387763584124,40.760502717570674,-73.982790236415866]}");
            obj.Box.NorthLatitude.Should().Be(40.760502717570674);
            obj.Box.SouthLatitude.Should().Be(40.752777282429321);
            obj.Box.WestLongitude.Should().Be(-73.996387763584124);
            obj.Box.EastLongitude.Should().Be(-73.982790236415866);
        }
    }
}