// <copyright file="Coordinate.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Models.Parameters
{
    using System.Globalization;

    /// <summary>
    /// The coordinates (longitude, latitude) used for Trimble Maps reverse geocoding.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Gets or sets the latitude of the location. For example: "40.7128".
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the location. For example: "-74.0060".
        /// </summary>
        public double Longitude { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1}", Longitude, Latitude);
        }
    }
}
