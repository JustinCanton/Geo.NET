// <copyright file="BaseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Models.Parameters
{
    using System.Globalization;

    /// <summary>
    /// Base parameters across all Bing geocoding APIs.
    /// </summary>
    public class BaseParameters
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not to include the neighbourhood information.
        /// true: Include neighbourhood information when available.
        /// false [default]: Do not include neighbourhood information.
        /// </summary>
        public bool IncludeNeighbourhood { get; set; } = false;

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
        /// Gets or sets the culture to use for the request.
        /// </summary>
        public CultureInfo Culture { get; set; }
    }
}
