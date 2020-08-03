// <copyright file="GeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A response for a geocoding operation.
    /// </summary>
    public class GeocodingResponse
    {
        /// <summary>
        /// Gets or sets the spatial reference of the location.
        /// </summary>
        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }

        /// <summary>
        /// Gets or sets the possible locations to match the geocoding.
        /// </summary>
        [JsonProperty("locations")]
        public List<LocationInformation> Locations { get; set; }
    }
}
