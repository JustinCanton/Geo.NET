// <copyright file="BoundingBox.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters
{
    using System.Globalization;

    /// <summary>
    /// A rectangular geographic bounding box defined by two opposite corners.
    /// </summary>
    public class BoundingBox
    {
        /// <summary>
        /// Gets or sets the longitude of the first corner in decimal degrees.
        /// </summary>
        public double Longitude1 { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the first corner in decimal degrees.
        /// </summary>
        public double Latitude1 { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the second corner in decimal degrees.
        /// </summary>
        public double Longitude2 { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the second corner in decimal degrees.
        /// </summary>
        public double Latitude2 { get; set; }

        /// <summary>
        /// Returns the bounding box in the Geoapify rectangle format.
        /// </summary>
        /// <returns>A <see cref="string"/> in the format "lon1,lat1,lon2,lat2".</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3}", Longitude1, Latitude1, Longitude2, Latitude2);
        }
    }
}
