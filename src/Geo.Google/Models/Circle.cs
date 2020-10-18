// <copyright file="Circle.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models
{
    /// <summary>
    /// A circular bounding shape for requests.
    /// </summary>
    public class Circle : BaseBounding
    {
        /// <summary>
        /// Gets or sets the coordinate marking the centre of the circle.
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Gets or sets the radius of the circle.
        /// </summary>
        public int Radius { get; set; } = 0;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Radius}@{Coordinate}";
        }
    }
}
