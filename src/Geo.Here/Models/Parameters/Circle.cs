// <copyright file="Circle.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    using System.Globalization;

    /// <summary>
    /// Circle information for search bounds.
    /// </summary>
    public class Circle
    {
        /// <summary>
        /// Gets or sets the centre coordinate of the circle.
        /// </summary>
        public Coordinate Centre { get; set; }

        /// <summary>
        /// Gets or sets the radius of the circle.
        /// </summary>
        public uint Radius { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1};r={2}", Centre.Latitude, Centre.Longitude, Radius);
        }

        /// <summary>
        /// Checks to see if the circle is a valid circle.
        /// </summary>
        /// <returns>A boolean indicating whether the circle is valid or not.</returns>
        internal bool IsValid()
        {
            return Radius > 0 && Centre != null && Centre.IsValid();
        }
    }
}
