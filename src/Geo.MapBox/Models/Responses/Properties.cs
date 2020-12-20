// <copyright file="Properties.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Properties used to describe the location.
    /// </summary>
    public class Properties
    {
        /// <summary>
        /// Gets or sets a point accuracy metric for the returned address feature.Can be one of rooftop, parcel, point, interpolated, intersection, street.
        /// </summary>
        [JsonProperty("accuracy")]
        public string Accuracy { get; set; }

        /// <summary>
        /// Gets or sets a string of the full street address for the returned poi feature.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets a string of comma-separated categories for the returned poi feature.
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the name of a suggested Maki icon to visualize a poi feature based on its category.
        /// </summary>
        [JsonProperty("maki")]
        public string Maki { get; set; }

        /// <summary>
        /// Gets or sets the Wikidata identifier for the returned feature.
        /// </summary>
        [JsonProperty("wikidata")]
        public string Wikidata { get; set; }

        /// <summary>
        /// Gets or sets the ISO 3166-1 country and ISO 3166-2 region code for the returned feature.
        /// </summary>
        [JsonProperty("short_code")]
        public string ShortCode { get; set; }
    }
}