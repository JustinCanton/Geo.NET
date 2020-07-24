// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Parameters for the Bing geocoding query.
    /// </summary>
    public class GeocodingParameters
    {
        /// <summary>
        /// Gets or sets a string that contains information about a location, such as an address or landmark name.
        /// </summary>
        [Required]
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to include the neighbourhood information.
        /// true: Include neighborhood information when available.
        /// false [default]: Do not include neighborhood information.
        /// </summary>
        public bool IncludeNeighborhood { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether or not to have the response show how the query string was parsed into address values,
        /// such as addressLine, locality, adminDistrict, and postalCode.
        /// </summary>
        public bool IncludeQueryParse { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to include the two-letter ISO country code or not.
        /// </summary>
        public bool IncludeCiso2 { get; set; } = false;

        /// <summary>
        /// Gets or sets the maximum number of results allowable to be returned.
        /// Allowable values are between 1 and 20. The default value is 5.
        /// If the value is 0, the default value will be used.
        /// </summary>
        public int MaximumResults { get; set; } = 0;
    }
}
