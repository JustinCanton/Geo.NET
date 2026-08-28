// <copyright file="RectangleBias.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters.Biases
{
    using System.Globalization;
    using Geo.Geoapify.Models.Parameters;

    /// <summary>
    /// A bias preferring results within a rectangular bounding box.
    /// </summary>
    public class RectangleBias : Bias
    {
        /// <summary>
        /// Gets or sets the bounding box to prefer results within.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "rect:{0}", BoundingBox);
        }
    }
}
