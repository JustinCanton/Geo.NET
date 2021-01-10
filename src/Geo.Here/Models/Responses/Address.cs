// <copyright file="Address.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The postal address of the result item.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the requested address. For example: "Schulstraße 4, 32547 Bad Oeynhausen, Germany".
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets a three-letter country code. For example: "DEU".
        /// </summary>
        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the localised country name. For example: "Deutschland".
        /// </summary>
        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets a code/abbreviation for the state division of a country. For example: "North Rhine-Westphalia".
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets a division of a state; typically, a secondary-level administrative division of a country or equivalent.
        /// </summary>
        [JsonProperty("county")]
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the name of the primary locality of the place. For example: "Bad Oyenhausen".
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets a division of city; typically an administrative unit within a larger city or a customary name of a city's neighbourhood.
        /// For example: "Bad Oyenhausen".
        /// </summary>
        [JsonProperty("district")]
        public string District { get; set; }

        /// <summary>
        /// Gets or sets a subdivision of a district. For example: "Minden-Lübbecke".
        /// </summary>
        [JsonProperty("subdistrict")]
        public string SubDistrict { get; set; }

        /// <summary>
        /// Gets or sets the name of street. For example: "Schulstrasse".
        /// </summary>
        [JsonProperty("street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the name of block.
        /// </summary>
        [JsonProperty("block")]
        public string Block { get; set; }

        /// <summary>
        /// Gets or sets the name of sub-block.
        /// </summary>
        [JsonProperty("subblock")]
        public string SubBlock { get; set; }

        /// <summary>
        /// Gets or sets an alphanumeric string included in a postal address to facilitate mail sorting, such as post code, postcode, or ZIP code.
        /// For example: "32547".
        /// </summary>
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the house number. For example: "4".
        /// For example: "32547".
        /// </summary>
        [JsonProperty("houseNumber")]
        public string HouseNumber { get; set; }
    }
}
