// <copyright file="Geometry.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The geometry information for the address.
    /// </summary>
    public class Geometry
    {
        /// <summary>
        /// Gets or sets the type of the coordinate.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the address.
        /// </summary>
        [JsonPropertyName("coordinates")]
        public Coordinate Coordinate { get; set; }
    }
}
