// <copyright file="BaseFilterParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The base filter parameters that are used with almost all HERE requests.
    /// </summary>
    public class BaseFilterParameters : BaseParameters
    {
        /// <summary>
        /// Gets or sets the center of the search context expressed as coordinates.
        /// Required parameter for endpoints that are expected to rank results by distance from the explicitly specified search center.
        /// Example: -13.163068,-72.545128 (Machu Picchu Mountain, Peru).
        /// </summary>
        public Coordinate At { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to be returned.
        /// </summary>
        [Range(0, 100)]
        public uint Limit { get; set; } = 0;
    }
}
