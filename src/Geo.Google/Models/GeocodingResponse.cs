// <copyright file="GeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The Google Geocoding response object.
    /// </summary>
    public class GeocodingResponse
    {
        /// <summary>
        /// A list of the results for the Google Geocoding API call.
        /// </summary>
        [JsonProperty("results")]
        public IEnumerable<Geocoding> Results { get; set; }

        /// <summary>
        /// The status of the Google Geocoding API call.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
