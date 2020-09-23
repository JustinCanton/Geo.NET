// <copyright file="Circle.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
