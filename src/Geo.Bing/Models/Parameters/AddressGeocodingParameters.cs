// <copyright file="AddressGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Models.Parameters
{
    /// <summary>
    /// Parameters for the Bing address geocoding query.
    /// </summary>
    public class AddressGeocodingParameters : ResultParameters
    {
        /// <summary>
        /// Gets or sets the subdivision name in the country or region for an address.
        /// This element is typically treated as the first order administrative subdivision,
        /// but in some cases it is the second, third, or fourth order subdivision in a country,
        /// dependency, or region.
        /// </summary>
        public string AdministrationDistrict { get; set; }

        /// <summary>
        /// Gets or sets the locality, such as the city or neighbourhood, that corresponds to an address.
        /// </summary>
        public string Locality { get; set; }

        /// <summary>
        /// Gets or sets the post code, postal code, or ZIP Code of an address.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the official street line of an address relative to the area,
        /// as specified by the Locality, or PostalCode, properties.
        /// Typical use of this element would be to provide a street address or any official address.
        /// </summary>
        public string AddressLine { get; set; }

        /// <summary>
        /// Gets or sets the ISO country code for the country.
        /// </summary>
        public string CountryRegion { get; set; }
    }
}
