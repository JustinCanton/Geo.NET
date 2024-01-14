﻿// <copyright file="Point.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A point on the Earth specified by a latitude and longitude.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Gets or sets the type of the underlying shape.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the coordinate information for the point.
        /// </summary>
        [JsonPropertyName("coordinates")]
        public Coordinate Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the method that was used to compute the geocode point.
        /// It is one of the following values:
        /// Interpolation: The geocode point was matched to a point on a road using interpolation.
        /// InterpolationOffset: The geocode point was matched to a point on a road using interpolation with an additional offset to shift the point to the side of the street.
        /// Parcel: The geocode point was matched to the center of a parcel.
        /// Rooftop: The geocode point was matched to the rooftop of a building.
        /// </summary>
        [JsonPropertyName("calculationMethod")]
        public string CalculationMethod { get; set; }

        /// <summary>
        /// Gets or sets the best use for the geocode point.
        /// Each geocode point is defined as a Route point, a Display point or both.
        /// Use Route points if you are creating a route to the location.
        /// Use Display points if you are showing the location on a map.
        /// For example, if the location is a park, a Route point may specify an entrance to the park where you can enter with a car,
        /// and a Display point may be a point that specifies the center of the park.
        /// It is one or more of the following values:
        /// Display
        /// Route.
        /// </summary>
        [JsonPropertyName("usageTypes")]
        public IList<string> UsageTypes { get; set;  } = new List<string>();

        /// <summary>
        /// Gets or sets the geographic area that contains the location.
        /// A bounding box contains SouthLatitude, WestLongitude, NorthLatitude, and EastLongitude values in degrees.
        /// </summary>
        [JsonPropertyName("boundingBox")]
        public BoundingBox BoundingBox { get; set; }
    }
}
