// <copyright file="GeocodingGeometry.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Text.Json.Serialization;
    using Geo.Google.Enums;

    /// <summary>
    /// The geometry object returned by geocoding requests.
    /// </summary>
    public class GeocodingGeometry : Geometry
    {
        /// <summary>
        /// Gets or sets additional data about the specified location.
        /// </summary>
        [JsonPropertyName("location_type")]
        public LocationType LocationType { get; set; }

        /// <summary>
        /// Gets or sets the (optionally returned) boundaries which can fully contain the returned result.
        /// </summary>
        [JsonPropertyName("bounds")]
        public Boundaries Bounds { get; set; }
    }
}
