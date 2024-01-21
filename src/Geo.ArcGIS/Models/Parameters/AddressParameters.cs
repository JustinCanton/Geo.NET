// <copyright file="AddressParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    /// <summary>
    /// A parameters object for the address information for a ArcGIS request.
    /// </summary>
    public class AddressParameters
    {
        /// <summary>
        /// Gets or sets the address of the location.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets a string that represents the second line of a street address.
        /// This can include street name/house number, building name, place-name, or subunit.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets a string that represents the third line of a street address.
        /// This can include street name/house number, building name, place-name, or subunit.
        /// </summary>
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood of the location.
        /// </summary>
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the city of the location.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the subregion of the location.
        /// </summary>
        public string Subregion { get; set; }

        /// <summary>
        /// Gets or sets the region of the location.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the standard postal code for an address, typically, a three– to six-digit alphanumeric code.
        /// </summary>
        public string Postal { get; set; }

        /// <summary>
        /// Gets or sets a postal code extension, such as the United States Postal Service ZIP+4 code, provides finer resolution or higher accuracy when also passing postal.
        /// </summary>
        public string PostalExt { get; set; }

        /// <summary>
        /// Gets or sets a value representing the country. Providing this value increases geocoding speed.
        /// Acceptable values include the full country name in English or the official language of the country, the two-character country code, or the three-character country code.
        /// </summary>
        public string CountryCode { get; set; }
    }
}
