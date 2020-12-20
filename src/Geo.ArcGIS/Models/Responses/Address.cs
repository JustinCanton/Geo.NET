// <copyright file="Address.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The address information.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the match address.
        /// </summary>
        [JsonProperty("Match_addr")]
        public string MatchAddress { get; set; }

        /// <summary>
        /// Gets or sets the long label string for the address.
        /// </summary>
        [JsonProperty("LongLabel")]
        public string LongLabel { get; set; }

        /// <summary>
        /// Gets or sets the short label string for the address.
        /// </summary>
        [JsonProperty("ShortLabel")]
        public string ShortLabel { get; set; }

        /// <summary>
        /// Gets or sets the address type.
        /// </summary>
        [JsonProperty("Addr_type")]
        public string AddressType { get; set; }

        /// <summary>
        /// Gets or sets the type of address.
        /// </summary>
        [JsonProperty("Type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the place name if the address has a name.
        /// </summary>
        [JsonProperty("PlaceName")]
        public string PlaceName { get; set; }

        /// <summary>
        /// Gets or sets the address number.
        /// </summary>
        [JsonProperty("AddNum")]
        public string AddressNumber { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [JsonProperty("Address")]
        public string AddressName { get; set; }

        /// <summary>
        /// Gets or sets the block name.
        /// </summary>
        [JsonProperty("Block")]
        public string Block { get; set; }

        /// <summary>
        /// Gets or sets the sector of the address.
        /// </summary>
        [JsonProperty("Sector")]
        public string Sector { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood information for the address.
        /// </summary>
        [JsonProperty("Neighborhood")]
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the district of the address.
        /// </summary>
        [JsonProperty("District")]
        public string District { get; set; }

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        [JsonProperty("City")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the metropolitian area of the address.
        /// </summary>
        [JsonProperty("MetroArea")]
        public string MetropolitanArea { get; set; }

        /// <summary>
        /// Gets or sets the sub-region of the address.
        /// </summary>
        [JsonProperty("Subregion")]
        public string Subregion { get; set; }

        /// <summary>
        /// Gets or sets the region of the address.
        /// </summary>
        [JsonProperty("Region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the territory of the address.
        /// </summary>
        [JsonProperty("Territory")]
        public string Territory { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the address.
        /// </summary>
        [JsonProperty("Postal")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the postal code extension of the address.
        /// </summary>
        [JsonProperty("PostalExt")]
        public string PostalCodeExtension { get; set; }

        /// <summary>
        /// Gets or sets the country code of the address.
        /// </summary>
        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }
    }
}
