// <copyright file="BoundingBox.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Models.Parameters
{
    /// <summary>
    /// A rectangular geographic bounding box defined by its four edges.
    /// </summary>
    public class BoundingBox
    {
        /// <summary>
        /// Gets or sets the minimum longitude (west edge) in decimal degrees.
        /// </summary>
        public double MinLongitude { get; set; }

        /// <summary>
        /// Gets or sets the maximum longitude (east edge) in decimal degrees.
        /// </summary>
        public double MaxLongitude { get; set; }

        /// <summary>
        /// Gets or sets the minimum latitude (south edge) in decimal degrees.
        /// </summary>
        public double MinLatitude { get; set; }

        /// <summary>
        /// Gets or sets the maximum latitude (north edge) in decimal degrees.
        /// </summary>
        public double MaxLatitude { get; set; }
    }
}
