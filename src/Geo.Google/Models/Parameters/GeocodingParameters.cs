// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The parameters for the gecoding Google API.
    /// </summary>
    public class GeocodingParameters : BaseParameters
    {
        /// <summary>
        /// Gets or sets the street address or plus code that you want to geocode.
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets a components filter with elements separated by a pipe (|).
        /// The components filter is also accepted as an optional parameter if an address is provided.
        /// Each element in the components filter consists of a component:value pair, and fully restricts the results from the geocoder.
        /// </summary>
        public string Components { get; set; }

        /// <summary>
        /// Gets or sets the bounding box of the viewport within which to bias geocode results more prominently.
        /// This parameter will only influence, not fully restrict, results from the geocoder.
        /// </summary>
        public Boundaries Bounds { get; set; }

        /// <summary>
        /// Gets or sets the region code, specified as a ccTLD ("top-level domain") two-character value.
        /// This parameter will only influence, not fully restrict, results from the geocoder.
        /// </summary>
        public string Region { get; set; }
    }
}
