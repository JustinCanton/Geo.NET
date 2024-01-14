// <copyright file="GeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A response for a geocoding operation.
    /// </summary>
    public class GeocodingResponse
    {
        /// <summary>
        /// Gets or sets the spatial reference of the location.
        /// </summary>
        [JsonPropertyName("spatialReference")]
        public SpatialReference SpatialReference { get; set; }

        /// <summary>
        /// Gets the possible locations to match the geocoding.
        /// </summary>
        [JsonPropertyName("locations")]
        public IList<LocationInformation> Locations { get; } = new List<LocationInformation>();
    }
}
