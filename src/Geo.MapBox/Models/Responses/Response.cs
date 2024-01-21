﻿// <copyright file="Response.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The response from the MapBox request.
    /// </summary>
    /// <typeparam name="TQuery">The type of the initial query to the MapBox endpoint.</typeparam>
    public class Response<TQuery>
    {
        /// <summary>
        /// Gets or sets the GeoJSON type for the results.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the query information sent to MapBox.
        /// </summary>
        [JsonPropertyName("query")]
        public TQuery Query { get; set; }

        /// <summary>
        /// Gets or sets a list of features that matched the query.
        /// </summary>
        [JsonPropertyName("features")]
        public IList<Feature> Features { get; set; } = new List<Feature>();

        /// <summary>
        /// Gets or sets a string that attributes the results of the Mapbox Geocoding API to Mapbox.
        /// </summary>
        [JsonPropertyName("attribution")]
        public string Attribution { get; set; }
    }
}
