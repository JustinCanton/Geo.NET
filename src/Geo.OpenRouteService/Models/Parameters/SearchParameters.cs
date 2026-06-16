// <copyright file="SearchParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Models.Parameters
{
    /// <summary>
    /// Parameters for the ORS forward geocoding search endpoint (<c>/geocode/search</c>).
    /// </summary>
    public class SearchParameters : BaseSearchParameters
    {
        /// <summary>
        /// Gets or sets the search query text. Required.
        /// </summary>
        public string Text { get; set; }
    }
}
