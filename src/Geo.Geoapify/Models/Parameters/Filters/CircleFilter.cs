// <copyright file="CircleFilter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters.Filters
{
    using System.Globalization;
    using Geo.Geoapify.Models;

    /// <summary>
    /// A filter restricting results to a circle around a coordinate.
    /// </summary>
    public class CircleFilter : Filter
    {
        /// <summary>
        /// Gets or sets the centre of the circle.
        /// </summary>
        public Coordinate Centre { get; set; }

        /// <summary>
        /// Gets or sets the radius of the circle in metres.
        /// </summary>
        public double Radius { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "circle:{0},{1}", Centre, Radius);
        }
    }
}
