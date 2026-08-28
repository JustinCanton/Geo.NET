// <copyright file="GeocodingMetadata.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Metadata about the geocoding query returned alongside results.
    /// </summary>
    public class GeocodingMetadata
    {
        /// <summary>
        /// Gets or sets the version of the geocoding engine.
        /// </summary>
        [JsonPropertyName("version")]
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the attribution string for the data sources.
        /// </summary>
        [JsonPropertyName("attribution")]
        public string Attribution { get; set; }

        /// <summary>
        /// Gets or sets the echoed query parameters as a raw JSON element.
        /// </summary>
        [JsonPropertyName("query")]
        public JsonElement? Query { get; set; }

        /// <summary>
        /// Gets or sets any warnings returned by the geocoding engine.
        /// </summary>
        [JsonPropertyName("warnings")]
        public IList<string> Warnings { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the Unix timestamp of the response.
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long? Timestamp { get; set; }
    }
}
