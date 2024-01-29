// <copyright file="GeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The Google geocoding response object.
    /// </summary>
    public class GeocodingResponse
    {
        /// <summary>
        /// Gets or sets a list of the results for the Google Geocoding API call.
        /// </summary>
        [JsonPropertyName("results")]
        public IEnumerable<Geocoding> Results { get; set; } = new List<Geocoding>();

        /// <summary>
        /// Gets or sets the status of the Google Geocoding API call.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
