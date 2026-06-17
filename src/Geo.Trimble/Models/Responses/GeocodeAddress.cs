// <copyright file="GeocodeAddress.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The address portion of a Trimble Maps geocoding response.
    /// </summary>
    public class GeocodeAddress
    {
        /// <summary>
        /// Gets or sets the street address.
        /// </summary>
        [JsonPropertyName("StreetAddress")]
        public string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [JsonPropertyName("City")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state or province.
        /// </summary>
        [JsonPropertyName("State")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the postal/ZIP code.
        /// </summary>
        [JsonPropertyName("Zip")]
        public string Zip { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        [JsonPropertyName("County")]
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [JsonPropertyName("Country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the abbreviated country code.
        /// </summary>
        [JsonPropertyName("CountryAbbreviation")]
        public string CountryAbbreviation { get; set; }
    }
}
