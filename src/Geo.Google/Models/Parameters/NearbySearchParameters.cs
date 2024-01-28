// <copyright file="NearbySearchParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Parameters
{
    using Geo.Google.Enums;

    /// <summary>
    /// Parameters used for the nearby search request.
    /// </summary>
    public class NearbySearchParameters : BaseSearchParameters, IKeyParameters
    {
        /// <summary>
        /// Gets or sets a term to be matched against all content that Google has indexed for this place,
        /// including but not limited to name, type, and address, as well as customer reviews and other third-party content.
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the order in which results are listed.
        /// The default is prominence.
        /// </summary>
        public RankType RankBy { get; set; } = RankType.Prominence;

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
