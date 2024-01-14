// <copyright file="SpatialLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A location with a spatial reference.
    /// </summary>
    public class SpatialLocation : Coordinate
    {
        /// <summary>
        /// Gets or sets the spatial reference of the location.
        /// </summary>
        [JsonPropertyName("spatialReference")]
        public SpatialReference SpatialReference { get; set; }
    }
}
