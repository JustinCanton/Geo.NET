// <copyright file="BrowseParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    /// <summary>
    /// The parameters possible to use during a browse request.
    /// </summary>
    public class BrowseParameters : AreaParameters
    {
        /// <summary>
        /// Gets or sets a category filter consisting of a comma-separated list of category-Ids for Categories defined in the HERE Places Category System,
        /// described in the Appendix to the HERE Search Developer Guide.
        /// Places with any assigned categories that match any of the requested categories are included in the response.
        /// </summary>
        public string Categories { get; set; }

        /// <summary>
        /// Gets or sets the full-text filter on POI names/titles.
        /// Results with a partial match on the name parameter are included in the response.
        /// </summary>
        public string Name { get; set; }
    }
}
