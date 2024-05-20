// <copyright file="ILocationGeocodeParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The location based filter parameters used for geocoding or reverse geocoding.
    /// </summary>
    public interface ILocationGeocodeParameters
    {
        /// <summary>
        /// Gets one or more 2-letter(e.g.AU) or 3-letter country codes(e.g.AUS) to filter the geocoding results.
        /// <para>Optional.</para>
        /// </summary>
        IList<string> Countries { get; }

        /// <summary>
        /// Gets or sets a filter for the geocoding results specifying a region. This could be a neighbourhood, district, city, county, state or administrative area. Example: region= Berlin to filter by locations in Berlin.
        /// <para>Optional.</para>
        /// </summary>
        string Region { get; set; }
    }
}
