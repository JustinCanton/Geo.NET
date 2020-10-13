// <copyright file="TextSearchParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    /// <summary>
    /// Parameters used for the text search request.
    /// </summary>
    public class TextSearchParameters : BaseSearchParameters
    {
        /// <summary>
        /// Gets or sets the text string on which to search, for example: "restaurant" or "123 Main Street".
        /// The Google Places service will return candidate matches based on this string and order the results based on their perceived relevance.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the region code, specified as a ccTLD (country code top-level domain) two-character value.
        /// Most ccTLD codes are identical to ISO 3166-1 codes, with some exceptions.
        /// This parameter will only influence, not fully restrict, search results.
        /// If more relevant results exist outside of the specified region, they may be included.
        /// When this parameter is used, the country name is omitted from the resulting formatted_address for results in the specified region.
        /// </summary>
        public string Region { get; set; }
    }
}
