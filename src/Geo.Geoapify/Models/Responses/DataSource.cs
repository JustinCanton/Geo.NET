// <copyright file="DataSource.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The data source a Geoapify geocoding result originated from.
    /// </summary>
    public class DataSource
    {
        /// <summary>
        /// Gets or sets the name of the source, for example "openstreetmap".
        /// </summary>
        [JsonPropertyName("sourcename")]
        public string SourceName { get; set; }

        /// <summary>
        /// Gets or sets the attribution required when displaying the result.
        /// </summary>
        [JsonPropertyName("attribution")]
        public string Attribution { get; set; }

        /// <summary>
        /// Gets or sets the license the source data is published under.
        /// </summary>
        [JsonPropertyName("license")]
        public string License { get; set; }

        /// <summary>
        /// Gets or sets the url of the object within the source.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
