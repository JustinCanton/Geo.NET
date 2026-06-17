// <copyright file="StructuredSearchParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Models.Parameters
{
    /// <summary>
    /// Parameters for the ORS structured geocoding endpoint (<c>/geocode/search/structured</c>).
    /// At least one address component must be provided.
    /// </summary>
    public class StructuredSearchParameters : BaseSearchParameters
    {
        /// <summary>
        /// Gets or sets the street address including house number.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood name.
        /// </summary>
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the borough name.
        /// </summary>
        public string Borough { get; set; }

        /// <summary>
        /// Gets or sets the locality (city or town) name.
        /// </summary>
        public string Locality { get; set; }

        /// <summary>
        /// Gets or sets the county name.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the region (state or province) name.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the country name or ISO 3166-1 alpha-2/alpha-3 code.
        /// </summary>
        public string Country { get; set; }
    }
}
