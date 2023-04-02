// <copyright file="BoundingBox.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Parameters
{
    using System.Globalization;

    /// <summary>
    /// The north/south/east/west bounding box for a map view.
    /// </summary>
    public class BoundingBox
    {
        /// <summary>
        /// Gets or sets the longitude of the western-side of the box.For example: "8.80068".
        /// </summary>
        public double West { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the southern-side of the box. For example: "52.19333".
        /// </summary>
        public double South { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the eastern-side of the box. For example: "8.8167".
        /// </summary>
        public double East { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the northern-side of the box. For example: "52.19555".
        /// </summary>
        public double North { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1},{2},{3}", North, West, South, East);
        }
    }
}
