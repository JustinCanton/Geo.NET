// <copyright file="IBaseFilterParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Interface for filter parameters used in HERE requests.
    /// </summary>
    public interface IBaseFilterParameters : IBaseParameters
    {
        /// <summary>
        /// Gets or sets the center of the search context expressed as coordinates.
        /// Required parameter for endpoints that are expected to rank results by distance from the explicitly specified search center.
        /// Example: -13.163068,-72.545128 (Machu Picchu Mountain, Peru).
        /// </summary>
        Coordinate At { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to be returned.
        /// </summary>
        [Range(0, 100)]
        uint Limit { get; set; }
    }
}
