// <copyright file="SpatialLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A location with a spatial reference.
    /// </summary>
    public class SpatialLocation : Coordinate
    {
        /// <summary>
        /// Gets or sets the spatial reference of the location.
        /// </summary>
        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }
    }
}
