// <copyright file="ReverseGeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A response for a reverse geocoding operation.
    /// </summary>
    public class ReverseGeocodingResponse
    {
        /// <summary>
        /// Gets or sets the address information.
        /// </summary>
        [JsonPropertyName("address")]
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the location information.
        /// </summary>
        [JsonPropertyName("location")]
        public SpatialLocation Location { get; set; }
    }
}
