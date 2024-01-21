// <copyright file="Geometry.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The geometry information for a location.
    /// </summary>
    public class Geometry
    {
        /// <summary>
        /// Gets or sets the type of the GeoJSON.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the location.
        /// </summary>
        [JsonPropertyName("coordinates")]
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an address is interpolated along a road network.
        /// The geocoder can usually return exact address points, but if an address is not present the geocoder can use interpolated data as a fallback.
        /// In edge cases, interpolation may not be possible if surrounding address data is not present,
        /// in which case the next fallback will be the center point of the street feature itself.
        /// </summary>
        [JsonPropertyName("interpolated")]
        public bool Interpolated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether an out-of-parity match.
        /// This occurs when an interpolated address is not in the expected range for the indicated side of the street.
        /// </summary>
        [JsonPropertyName("omitted")]
        public bool Omitted { get; set; }
    }
}
