// <copyright file="Address.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// An address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the address information.
        /// </summary>
        [JsonProperty("addressLine")]
        public string AddressLine { get; set; }

        /// <summary>
        /// Gets or sets the administration district information.
        /// </summary>
        [JsonProperty("adminDistrict")]
        public string AdministrationDistrict { get; set; }

        /// <summary>
        /// Gets or sets the secondary administration information.
        /// </summary>
        [JsonProperty("adminDistrict2")]
        public string AdministrationDistrict2 { get; set; }

        /// <summary>
        /// Gets or sets the region in the country.
        /// </summary>
        [JsonProperty("countryRegion")]
        public string CountryRegion { get; set; }

        /// <summary>
        /// Gets or sets the secondary region in the country.
        /// </summary>
        [JsonProperty("countryRegionIso2")]
        public string CountryRegionIso2 { get; set; }

        /// <summary>
        /// Gets or sets the correctly formatted address.
        /// </summary>
        [JsonProperty("formattedAddress")]
        public string FormattedAddress { get; set; }

        /// <summary>
        /// Gets or sets the area of the address.
        /// </summary>
        [JsonProperty("locality")]
        public string Locality { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the address.
        /// </summary>
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood of the address.
        /// </summary>
        [JsonProperty("neighborhood")]
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets any landmarks near the address.
        /// </summary>
        [JsonProperty("landmark")]
        public string Landmark { get; set; }
    }
}
