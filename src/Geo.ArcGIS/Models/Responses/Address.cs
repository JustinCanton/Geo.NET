// <copyright file="Address.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The address information.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the match address.
        /// </summary>
        [JsonPropertyName("Match_addr")]
        public string MatchAddress { get; set; }

        /// <summary>
        /// Gets or sets the long label string for the address.
        /// </summary>
        [JsonPropertyName("LongLabel")]
        public string LongLabel { get; set; }

        /// <summary>
        /// Gets or sets the short label string for the address.
        /// </summary>
        [JsonPropertyName("ShortLabel")]
        public string ShortLabel { get; set; }

        /// <summary>
        /// Gets or sets the address type.
        /// </summary>
        [JsonPropertyName("Addr_type")]
        public string AddressType { get; set; }

        /// <summary>
        /// Gets or sets the type of address.
        /// </summary>
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the place name if the address has a name.
        /// </summary>
        [JsonPropertyName("PlaceName")]
        public string PlaceName { get; set; }

        /// <summary>
        /// Gets or sets the address number.
        /// </summary>
        [JsonPropertyName("AddNum")]
        public string AddressNumber { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [JsonPropertyName("Address")]
        public string AddressName { get; set; }

        /// <summary>
        /// Gets or sets the block name.
        /// </summary>
        [JsonPropertyName("Block")]
        public string Block { get; set; }

        /// <summary>
        /// Gets or sets the sector of the address.
        /// </summary>
        [JsonPropertyName("Sector")]
        public string Sector { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood information for the address.
        /// </summary>
        [JsonPropertyName("Neighborhood")]
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the district of the address.
        /// </summary>
        [JsonPropertyName("District")]
        public string District { get; set; }

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        [JsonPropertyName("City")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the metropolitian area of the address.
        /// </summary>
        [JsonPropertyName("MetroArea")]
        public string MetropolitanArea { get; set; }

        /// <summary>
        /// Gets or sets the sub-region of the address.
        /// </summary>
        [JsonPropertyName("Subregion")]
        public string Subregion { get; set; }

        /// <summary>
        /// Gets or sets the region of the address.
        /// </summary>
        [JsonPropertyName("Region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the territory of the address.
        /// </summary>
        [JsonPropertyName("Territory")]
        public string Territory { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the address.
        /// </summary>
        [JsonPropertyName("Postal")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the postal code extension of the address.
        /// </summary>
        [JsonPropertyName("PostalExt")]
        public string PostalCodeExtension { get; set; }

        /// <summary>
        /// Gets or sets the country code of the address.
        /// </summary>
        [JsonPropertyName("CountryCode")]
        public string CountryCode { get; set; }
    }
}
