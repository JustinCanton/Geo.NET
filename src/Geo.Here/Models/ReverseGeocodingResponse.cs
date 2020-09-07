// <copyright file="ReverseGeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The response from a reverse geocoding request.
    /// </summary>
    public class ReverseGeocodingResponse
    {
        /// <summary>
        /// Gets or sets the list of locations that match the reverse geocoding request.
        /// </summary>
        [JsonProperty("items")]
        public List<Location> Items { get; set; }
    }
}
