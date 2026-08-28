// <copyright file="Filter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters.Filters
{
    /// <summary>
    /// The base class for the Geoapify location filters. Filters restrict results to the area they describe.
    /// When multiple filters are provided, they are combined with AND logic.
    /// </summary>
    public abstract class Filter
    {
        /// <summary>
        /// Returns the filter in the format expected by the Geoapify <c>filter</c> query parameter.
        /// </summary>
        /// <returns>A <see cref="string"/> with the Geoapify representation of the filter.</returns>
        public abstract override string ToString();
    }
}
