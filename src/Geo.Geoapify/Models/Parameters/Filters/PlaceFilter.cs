// <copyright file="PlaceFilter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters.Filters
{
    using System.Globalization;

    /// <summary>
    /// A filter restricting results to the boundary of a place previously returned by Geoapify.
    /// </summary>
    public class PlaceFilter : Filter
    {
        /// <summary>
        /// Gets or sets the Geoapify place identifier to restrict results to.
        /// </summary>
        public string PlaceId { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "place:{0}", PlaceId);
        }
    }
}
