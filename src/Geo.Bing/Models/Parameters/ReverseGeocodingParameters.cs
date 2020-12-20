// <copyright file="ReverseGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Models.Parameters
{
    /// <summary>
    /// Parameters for the Bing reverse geocoding query.
    /// </summary>
    public class ReverseGeocodingParameters : BaseParameters
    {
        /// <summary>
        /// Gets or sets a point on the Earth specified by a latitude and longitude.
        /// </summary>
        public Coordinate Point { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to return the address information.
        /// </summary>
        public bool IncludeAddress { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to return the neighbourhood information.
        /// </summary>
        public bool IncludeAddressNeighbourhood { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to return the populated place information.
        /// </summary>
        public bool IncludePopulatedPlace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to return the postcode information.
        /// </summary>
        public bool IncludePostcode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to return the administration division 1 information.
        /// </summary>
        public bool IncludeAdministrationDivision1 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to return the administration division 2 information.
        /// </summary>
        public bool IncludeAdministrationDivision2 { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to return the country region information.
        /// </summary>
        public bool IncludeCountryRegion { get; set; }
    }
}
