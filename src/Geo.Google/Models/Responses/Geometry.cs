// <copyright file="Geometry.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Contains the geometry information for the location.
    /// </summary>
    public class Geometry
    {
        /// <summary>
        /// Gets or sets the geocoded latitude,longitude value.
        /// </summary>
        [JsonPropertyName("location")]
        public Coordinate Location { get; set; }

        /// <summary>
        /// Gets or sets the recommended viewport for displaying the returned result,
        /// specified as two latitude,longitude values defining the southwest and northeast corner of the viewport bounding box.
        /// </summary>
        [JsonPropertyName("viewport")]
        public Boundaries Viewport { get; set; }
    }
}
