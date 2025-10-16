// <copyright file="IPolygonParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Models.Parameters
{
    using Geo.Nominatim.Enums;

    /// <summary>
    /// Defines parameters for controlling the output of polygon geometry in Nominatim queries.
    /// </summary>
    public interface IPolygonParameters
    {
        /// <summary>
        /// Gets or sets the output format for the full geometry of the place in the result.
        /// </summary>
        PolygonOutput PolygonOutput { get; set; }

        /// <summary>
        /// Gets or sets the maximum allowed deviation in degrees between the returned geometry and the original geometry.
        /// Topology is preserved in the geometry.
        /// </summary>
        float PolygonThreshold { get; set; }
    }
}
