// <copyright file="DiscoverParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    /// <summary>
    /// The parameters possible to use during a discover request.
    /// </summary>
    public class DiscoverParameters : AreaParameters
    {
        /// <summary>
        /// Gets or sets a free-text query.
        /// Examples:
        /// 125, Berliner, berlin
        /// Beacon, Boston, Hospital
        /// Schnurrbart German Pub and Restaurant, Hong Kong
        /// Note: Either q or qq-parameter is required on this endpoint. Both parameters can be provided in the same request.
        /// </summary>
        public string Query { get; set; }
    }
}
