// <copyright file="Address.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The address information for the location.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the latitude of the address. For example: "52.19404".
        /// </summary>
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the address. For example: "8.80135".
        /// </summary>
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the geometry information for the address.
        /// </summary>
        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        /// <summary>
        /// Gets or sets the localised country name. For example: "United States".
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets a two-letter country code. For example: "US".
        /// </summary>
        [JsonPropertyName("countryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the country flag.
        /// </summary>
        [JsonPropertyName("countryFlag")]
        public string CountryFlag { get; set; }

        /// <summary>
        /// Gets or sets the county information for an address.
        /// </summary>
        [JsonPropertyName("county")]
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the borough information for an address.
        /// </summary>
        [JsonPropertyName("borough")]
        public string Borough { get; set; }

        /// <summary>
        /// Gets or sets the name of the primary locality of the place. For example: "Bad Oyenhausen".
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the number. For example: "32547".
        /// </summary>
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the name of neighbourhood of the address.
        /// </summary>
        [JsonPropertyName("neighborhood")]
        public string Neighborhood { get; set; }

        /// <summary>
        /// Gets or sets an alphanumeric string included in a postal address to facilitate mail sorting, such as post code, postcode, or ZIP code.
        /// For example: "32547".
        /// </summary>
        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the state code for the state. For example: "NY".
        /// </summary>
        [JsonPropertyName("stateCode")]
        public string StateCode { get; set; }

        /// <summary>
        /// Gets or sets a code/abbreviation for the state division of a country. For example: "North Rhine-Westphalia".
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the name of street. For example: "Schulstrasse".
        /// </summary>
        [JsonPropertyName("street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the layer information for the address.
        /// </summary>
        [JsonPropertyName("layer")]
        public string Layer { get; set; }

        /// <summary>
        /// Gets or sets the formatted address. For example: "20 Jay St, Brooklyn, New York, NY 11201 USA".
        /// </summary>
        [JsonPropertyName("formattedAddress")]
        public string FormattedAddress { get; set; }

        /// <summary>
        /// Gets or sets the address label. For example: "20 Jay St".
        /// </summary>
        [JsonPropertyName("addressLabel")]
        public string AddressLabel { get; set; }
    }
}
