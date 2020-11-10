// <copyright file="AttributeConverterShould.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Tests.Converters
{
    using FluentAssertions;
    using Geo.ArcGIS.Converters;
    using Geo.ArcGIS.Models.Responses;
    using Geo.ArcGIS.Tests.Models;
    using Newtonsoft.Json;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the <see cref="AttributeConverter"/> class.
    /// </summary>
    [TestFixture]
    public class AttributeConverterShould
    {
        /// <summary>
        /// Tests the address attribute is correctly parsed.
        /// </summary>
        [Test]
        public void CorrectlyParseAddressAttribute()
        {
            var obj = JsonConvert.DeserializeObject<AttributeObject>("{\"Attribute\":{\"Match_addr\":\"123 East\",\"Addr_type\":\"POI\"}}");
            obj.Attribute.GetType().Should().Be(typeof(AddressAttribute));
            ((AddressAttribute)obj.Attribute).MatchAddress.Should().Be("123 East");
            ((AddressAttribute)obj.Attribute).AddressType.Should().Be("POI");
        }

        /// <summary>
        /// Tests the location attribute is correctly parsed.
        /// </summary>
        [Test]
        public void CorrectlyParseLocationAttribute()
        {
            var obj = JsonConvert.DeserializeObject<AttributeObject>("{\"Attribute\":{\"ResultID\":123,\"LongLabel\":\"123 East\"}}");
            obj.Attribute.GetType().Should().Be(typeof(LocationAttribute));
            ((LocationAttribute)obj.Attribute).ResultId.Should().Be(123);
            ((LocationAttribute)obj.Attribute).LongLabel.Should().Be("123 East");
        }

        /// <summary>
        /// Tests the place attribute is correctly parsed.
        /// </summary>
        [Test]
        public void CorrectlyParsePlaceAttribute()
        {
            var obj = JsonConvert.DeserializeObject<AttributeObject>("{\"Attribute\":{\"Place_addr\":\"123 East\",\"PlaceName\":\"East Side Company\"}}");
            obj.Attribute.GetType().Should().Be(typeof(PlaceAttribute));
            ((PlaceAttribute)obj.Attribute).PlaceAddress.Should().Be("123 East");
            ((PlaceAttribute)obj.Attribute).PlaceName.Should().Be("East Side Company");
        }
    }
}