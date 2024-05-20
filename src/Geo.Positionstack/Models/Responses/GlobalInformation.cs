// <copyright file="GlobalInformation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Global information about a country.
    /// </summary>
    public class GlobalInformation
    {
        /// <summary>
        /// Gets or sets the ISO 3166 Alpha 2 code of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("alpha2")]
        public string Alpha2 { get; set; }

        /// <summary>
        /// Gets or sets the ISO 3166 Alpha 3 code of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("alpha3")]
        public string Alpha3 { get; set; }

        /// <summary>
        /// Gets or sets the numeric ISO 3166 code of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("numeric_code")]
        public string NumericCode { get; set; }

        /// <summary>
        /// Gets or sets the country's region name. Example: Americas.
        /// </summary>
        [JsonPropertyName("region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the country's sub-region name. Example: Northern America.
        /// </summary>
        [JsonPropertyName("subregion")]
        public string Subregion { get; set; }

        /// <summary>
        /// Gets or sets the country's world region name. Example: AMER.
        /// </summary>
        [JsonPropertyName("world_region")]
        public string WorldRegion { get; set; }

        /// <summary>
        /// Gets or sets the country's region name. Example: Americas.
        /// </summary>
        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        /// <summary>
        /// Gets or sets the country's sub-region code. Example: 021.
        /// </summary>
        [JsonPropertyName("subregion_code")]
        public string SubregionCode { get; set; }

        /// <summary>
        /// Gets or sets the continent name of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("continent_name")]
        public string ContinentName { get; set; }

        /// <summary>
        /// Gets or sets the continent code of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("continent_code")]
        public string ContinentCode { get; set; }
    }
}
