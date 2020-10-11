// <copyright file="Response.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The response from the MapBox request.
    /// </summary>
    /// <typeparam name="TQuery">The type of the initial query to the MapBox endpoint.</typeparam>
    public class Response<TQuery>
    {
        /// <summary>
        /// Gets or sets the GeoJSON type for the results.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the query information sent to MapBox.
        /// </summary>
        [JsonProperty("query")]
        public TQuery Query { get; set; }

        /// <summary>
        /// Gets or sets a list of features that matched the query.
        /// </summary>
        [JsonProperty("features")]
        public List<Feature> Features { get; set; }

        /// <summary>
        /// Gets or sets a string that attributes the results of the Mapbox Geocoding API to Mapbox.
        /// </summary>
        [JsonProperty("attribution")]
        public string Attribution { get; set; }
    }
}
