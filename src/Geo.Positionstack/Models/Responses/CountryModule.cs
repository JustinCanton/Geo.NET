// <copyright file="CountryModule.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// An extensive set of country information.
    /// </summary>
    public class CountryModule
    {
        /// <summary>
        /// Gets or sets the country's latitude coordinate.
        /// </summary>
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the country's longitude coordinate.
        /// </summary>
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the common name of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("common_name")]
        public string CommonName { get; set; }

        /// <summary>
        /// Gets or sets the official name of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("official_name")]
        public string OfficialName { get; set; }

        /// <summary>
        /// Gets or sets the capital of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        /// <summary>
        /// Gets or sets a flag icon of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("flag")]
        public string Flag { get; set; }

        /// <summary>
        /// Gets or sets a value indicationg whether or not the country is landlocked.
        /// </summary>
        [JsonPropertyName("landlocked")]
        public bool? Landlocked { get; set; }

        /// <summary>
        /// Gets or sets a value indicationg whether or not the country is independent.
        /// </summary>
        [JsonPropertyName("independent")]
        public bool? Independent { get; set; }

        /// <summary>
        /// Gets or sets global information about a country.
        /// </summary>
        [JsonPropertyName("global")]
        public GlobalInformation GlobalInformation { get; set; }

        /// <summary>
        /// Gets or sets information about the phone for a country.
        /// </summary>
        [JsonPropertyName("dial")]
        public PhoneInformation PhoneInformation { get; set; }

        /// <summary>
        /// Gets the currency associated with the location result.
        /// </summary>
        [JsonPropertyName("currencies")]
        public IList<Currency> Currencies { get; } = new List<Currency>();

        /// <summary>
        /// Gets the language of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("languages")]
        public IList<Language> Languages { get; } = new List<Language>();
    }
}
