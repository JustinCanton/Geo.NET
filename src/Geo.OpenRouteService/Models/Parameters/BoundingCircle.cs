// <copyright file="BoundingCircle.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Models.Parameters
{
    /// <summary>
    /// A circular geographic boundary defined by a center point and radius.
    /// </summary>
    public class BoundingCircle
    {
        /// <summary>
        /// Gets or sets the latitude of the circle center in decimal degrees.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the circle center in decimal degrees.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the radius of the circle in kilometers. For forward geocoding the default is 50 km.
        /// For reverse geocoding the default is 1 km and the maximum is 5 km.
        /// </summary>
        public double? Radius { get; set; }
    }
}
