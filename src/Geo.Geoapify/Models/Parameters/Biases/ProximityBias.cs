// <copyright file="ProximityBias.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters.Biases
{
    using System.Globalization;
    using Geo.Geoapify.Models;

    /// <summary>
    /// A bias ordering results by their distance from a coordinate.
    /// </summary>
    public class ProximityBias : Bias
    {
        /// <summary>
        /// Gets or sets the coordinate to order the results by the distance from.
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "proximity:{0}", Coordinate);
        }
    }
}
