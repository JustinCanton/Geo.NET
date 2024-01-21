﻿// <copyright file="Address.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// An address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the address information.
        /// </summary>
        [JsonPropertyName("addressLine")]
        public string AddressLine { get; set; }

        /// <summary>
        /// Gets or sets the administration district information.
        /// </summary>
        [JsonPropertyName("adminDistrict")]
        public string AdministrationDistrict { get; set; }

        /// <summary>
        /// Gets or sets the secondary administration information.
        /// </summary>
        [JsonPropertyName("adminDistrict2")]
        public string AdministrationDistrict2 { get; set; }

        /// <summary>
        /// Gets or sets the region in the country.
        /// </summary>
        [JsonPropertyName("countryRegion")]
        public string CountryRegion { get; set; }

        /// <summary>
        /// Gets or sets the secondary region in the country.
        /// </summary>
        [JsonPropertyName("countryRegionIso2")]
        public string CountryRegionIso2 { get; set; }

        /// <summary>
        /// Gets or sets the correctly formatted address.
        /// </summary>
        [JsonPropertyName("formattedAddress")]
        public string FormattedAddress { get; set; }

        /// <summary>
        /// Gets or sets the area of the address.
        /// </summary>
        [JsonPropertyName("locality")]
        public string Locality { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the address.
        /// </summary>
        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood of the address.
        /// </summary>
        [JsonPropertyName("neighborhood")]
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets any landmarks near the address.
        /// </summary>
        [JsonPropertyName("landmark")]
        public string Landmark { get; set; }
    }
}
