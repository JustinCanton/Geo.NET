// <copyright file="DetailsParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Parameters used for the details request.
    /// </summary>
    public class DetailsParameters : BaseParameters, IKeyParameters
    {
        /// <summary>
        /// Gets or sets a textual identifier that uniquely identifies a place, returned from a Place Search.
        /// </summary>
        public string PlaceId { get; set; }

        /// <summary>
        /// Gets the list of fields to return from the request.
        /// </summary>
        public IList<string> Fields { get; } = new List<string>();

        /// <summary>
        /// Gets or sets the region code to use for restrictions.
        /// This parameter will only influence, not fully restrict, search results.
        /// If more relevant results exist outside of the specified region, they may be included.
        /// When this parameter is used, the country name is omitted from the resulting formatted_address for results in the specified region.
        /// </summary>
        public RegionInfo Region { get; set; }

        /// <summary>
        /// Gets or sets a random string which identifies an autocomplete session for billing purposes.
        /// If this parameter is omitted from an autocomplete request, the request is billed independently.
        /// </summary>
        public string SessionToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to disable translation of reviews.
        /// When true, the language code is included in the response along with the original-language review text. Optional.
        /// </summary>
        public bool? ReviewsNoTranslations { get; set; }

        /// <summary>
        /// Gets or sets the sort order for reviews.
        /// Accepted values: most_relevant, newest. Optional.
        /// </summary>
        public string ReviewsSort { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
