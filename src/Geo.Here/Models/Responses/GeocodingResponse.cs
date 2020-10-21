// <copyright file="GeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The response from a geocoding request.
    /// </summary>
    public class GeocodingResponse
    {
        /// <summary>
        /// Gets the list of locations that match the geocoding request.
        /// </summary>
        [JsonProperty("items")]
        public List<GeocodeLocation> Items { get; } = new List<GeocodeLocation>();
    }
}
