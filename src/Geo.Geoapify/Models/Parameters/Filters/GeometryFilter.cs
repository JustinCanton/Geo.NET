// <copyright file="GeometryFilter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters.Filters
{
    using System.Globalization;

    /// <summary>
    /// A filter restricting results to a geometry previously stored with Geoapify, for example an isoline.
    /// </summary>
    public class GeometryFilter : Filter
    {
        /// <summary>
        /// Gets or sets the Geoapify geometry identifier to restrict results to.
        /// </summary>
        public string GeometryId { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "geometry:{0}", GeometryId);
        }
    }
}
