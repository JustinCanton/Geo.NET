// <copyright file="BaseSearchParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Parameters shared across all search requests.
    /// </summary>
    public class BaseSearchParameters : CoordinateParameters
    {
        /// <summary>
        /// Gets or sets the value that restricts results to only those places above the specified price level.
        /// Valid values are in the range from 0 (most affordable) to 4 (most expensive), inclusive.
        /// The exact amount indicated by a specific value will vary from region to region.
        /// The default is 0.
        /// </summary>
        [Range(0, 4)]
        public uint MinimumPrice { get; set; } = 0;

        /// <summary>
        /// Gets or sets the value that restricts results to only those places below the specified price level.
        /// Valid values are in the range from 0 (most affordable) to 4 (most expensive), inclusive.
        /// The exact amount indicated by a specific value will vary from region to region.
        /// The default is 4.
        /// </summary>
        [Range(0, 4)]
        public uint MaximumPrice { get; set; } = 4;

        /// <summary>
        /// Gets or sets a value indicating whether to return only those places that are open for business at the time the query is sent.
        /// Places that do not specify opening hours in the Google Places database will not be returned if you include this parameter in your query.
        /// The default is false.
        /// </summary>
        public bool OpenNow { get; set; } = false;

        /// <summary>
        /// Gets or sets a value saying to return up to 20 results from a previously run search.
        /// Setting a pagetoken parameter will execute a search with the same parameters used previously — all parameters other than pagetoken will be ignored.
        /// </summary>
        public uint PageToken { get; set; } = 0;

        /// <summary>
        /// Gets or sets a restriction on the results to places matching the specified type.
        /// Only one type may be specified (if more than one type is provided, all types following the first entry are ignored).
        /// See the list of supported types (https://developers.google.com/places/web-service/supported_types).
        /// </summary>
        public string Type { get; set; }
    }
}