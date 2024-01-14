// <copyright file="Address.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The postal address of the result item.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the requested address. For example: "Schulstraße 4, 32547 Bad Oeynhausen, Germany".
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets a three-letter country code. For example: "DEU".
        /// </summary>
        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the localised country name. For example: "Deutschland".
        /// </summary>
        [JsonPropertyName("countryName")]
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets a state code or state name abbreviation – country specific. For example, in the United States it is the two letter state abbreviation: "CA" for California.
        /// </summary>
        [JsonPropertyName("stateCode")]
        public string StateCode { get; set; }

        /// <summary>
        /// Gets or sets a code/abbreviation for the state division of a country. For example: "North Rhine-Westphalia".
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets a county code or county name abbreviation – country specific. For example, for Italy it is the province abbreviation: "RM" for Rome.
        /// </summary>
        [JsonPropertyName("countyCode")]
        public string CountyCode { get; set; }

        /// <summary>
        /// Gets or sets a division of a state; typically, a secondary-level administrative division of a country or equivalent.
        /// </summary>
        [JsonPropertyName("county")]
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the name of the primary locality of the place. For example: "Bad Oyenhausen".
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets a division of city; typically an administrative unit within a larger city or a customary name of a city's neighbourhood.
        /// For example: "Bad Oyenhausen".
        /// </summary>
        [JsonPropertyName("district")]
        public string District { get; set; }

        /// <summary>
        /// Gets or sets a subdivision of a district. For example: "Minden-Lübbecke".
        /// </summary>
        [JsonPropertyName("subdistrict")]
        public string SubDistrict { get; set; }

        /// <summary>
        /// Gets or sets the name of street. For example: "Schulstrasse".
        /// </summary>
        [JsonPropertyName("street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets the names of streets in case of intersection result. For example: ["Friedrichstraße","Unter den Linden"].
        /// </summary>
        [JsonPropertyName("streets")]
        public IList<string> Streets { get; } = new List<string>();

        /// <summary>
        /// Gets or sets the name of block.
        /// </summary>
        [JsonPropertyName("block")]
        public string Block { get; set; }

        /// <summary>
        /// Gets or sets the name of sub-block.
        /// </summary>
        [JsonPropertyName("subblock")]
        public string SubBlock { get; set; }

        /// <summary>
        /// Gets or sets an alphanumeric string included in a postal address to facilitate mail sorting, such as post code, postcode, or ZIP code.
        /// For example: "32547".
        /// </summary>
        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the house number. For example: "4".
        /// For example: "32547".
        /// </summary>
        [JsonPropertyName("houseNumber")]
        public string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of building.
        /// </summary>
        [JsonPropertyName("building")]
        public string Building { get; set; }
    }
}
