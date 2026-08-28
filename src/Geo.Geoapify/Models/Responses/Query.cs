// <copyright file="Query.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The echo of the query that generated a Geoapify geocoding response.
    /// </summary>
    public class Query
    {
        /// <summary>
        /// Gets or sets the text that was searched for. Only returned for forward geocoding and autocomplete.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the address components the text was parsed into. Only returned for forward geocoding and autocomplete.
        /// </summary>
        [JsonPropertyName("parsed")]
        public ParsedQuery Parsed { get; set; }

        /// <summary>
        /// Gets or sets the latitude that was reverse geocoded. Only returned for reverse geocoding.
        /// </summary>
        [JsonPropertyName("lat")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude that was reverse geocoded. Only returned for reverse geocoding.
        /// </summary>
        [JsonPropertyName("lon")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the Open Location Code (plus code) of the reverse geocoded coordinate.
        /// Only returned for reverse geocoding.
        /// </summary>
        [JsonPropertyName("plus_code")]
        public string PlusCode { get; set; }
    }
}
