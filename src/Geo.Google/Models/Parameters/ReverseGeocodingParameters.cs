// <copyright file="ReverseGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Geo.Google.Enums;

    /// <summary>
    /// The parameters for the reverse gecoding Google API.
    /// </summary>
    public class ReverseGeocodingParameters : BaseParameters, IKeyParameters
    {
        /// <summary>
        /// Gets or sets the latitude and longitude values specifying the location
        /// for which you wish to obtain the closest, human-readable address.
        /// </summary>
        [Required]
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Gets or sets a filter of one or more address types.
        /// If the parameter contains multiple address types, the API returns all addresses that match any of the types.
        /// A note about processing: The ResultTypes parameter does not restrict the search to the specified address type(s).
        /// Rather, the ResultTypes acts as a post-search filter: the API fetches all results for the specified latlng,
        /// then discards those results that do not match the specified address type(s).
        /// </summary>
        public IEnumerable<ResultType> ResultTypes { get; set; }

        /// <summary>
        /// Gets or sets a filter of one or more location types.
        /// If the parameter contains multiple location types, the API returns all addresses that match any of the types.
        /// A note about processing: The LocationTypes parameter does not restrict the search to the specified location type(s).
        /// Rather, the LocationTypes acts as a post-search filter: the API fetches all results for the specified latlng,
        /// then discards those results that do not match the specified location type(s).
        /// </summary>
        public IEnumerable<LocationType> LocationTypes { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
