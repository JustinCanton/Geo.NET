// <copyright file="ICountryParameter.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// A country filter for a query.
    /// </summary>
    public interface ICountryParameter
    {
        /// <summary>
        /// Gets a list of countries to filter the request by. It uses the unique 2-letter country code (https://radar.com/documentation/regions/countries). Optional.
        /// </summary>
        IList<string> Countries { get; }
    }
}
