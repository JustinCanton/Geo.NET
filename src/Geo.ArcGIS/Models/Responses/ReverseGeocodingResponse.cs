// <copyright file="ReverseGeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A response for a reverse geocoding operation.
    /// </summary>
    public class ReverseGeocodingResponse
    {
        /// <summary>
        /// Gets or sets the address information.
        /// </summary>
        [JsonProperty("address")]
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the location information.
        /// </summary>
        [JsonProperty("location")]
        public SpatialLocation Location { get; set; }
    }
}
