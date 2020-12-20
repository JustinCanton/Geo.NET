// <copyright file="Circle.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
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
        public int Radius { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Centre.Latitude},{Centre.Longitude};r={Radius}";
        }
    }
}
