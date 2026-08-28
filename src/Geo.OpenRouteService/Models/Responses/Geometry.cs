// <copyright file="Geometry.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Models.Responses
{
    using System.Text.Json.Serialization;
    using Geo.OpenRouteService.Converters;
    using Geo.OpenRouteService.Models;

    /// <summary>
    /// A GeoJSON geometry object. For ORS geocoding results this is always a Point.
    /// The coordinates are stored as [longitude, latitude] in the JSON and converted via <see cref="CoordinateConverter"/>.
    /// </summary>
    public class Geometry
    {
        /// <summary>
        /// Gets or sets the GeoJSON geometry type (always "Point" for geocoding results).
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the coordinate represented by this geometry.
        /// </summary>
        [JsonPropertyName("coordinates")]
        [JsonConverter(typeof(CoordinateConverter))]
        public Coordinate Coordinates { get; set; }
    }
}
