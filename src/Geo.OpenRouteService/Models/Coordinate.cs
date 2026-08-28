// <copyright file="Coordinate.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Models
{
    using System.Globalization;

    /// <summary>
    /// A geographic coordinate with latitude and longitude.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Gets or sets the latitude in decimal degrees.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude in decimal degrees.
        /// </summary>
        public double Longitude { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1}", Latitude, Longitude);
        }
    }
}
