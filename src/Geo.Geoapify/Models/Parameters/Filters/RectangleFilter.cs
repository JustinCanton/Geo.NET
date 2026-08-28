// <copyright file="RectangleFilter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters.Filters
{
    using System.Globalization;

    /// <summary>
    /// A filter restricting results to a rectangular bounding box.
    /// </summary>
    public class RectangleFilter : Filter
    {
        /// <summary>
        /// Gets or sets the bounding box to restrict results to.
        /// </summary>
        public BoundingBox BoundingBox { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "rect:{0}", BoundingBox);
        }
    }
}
