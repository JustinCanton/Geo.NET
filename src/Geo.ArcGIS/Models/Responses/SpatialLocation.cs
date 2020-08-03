// <copyright file="SpatialLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
