// <copyright file="AutocompleteParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Models.Parameters
{
    /// <summary>
    /// Parameters for the ORS autocomplete endpoint (<c>/geocode/autocomplete</c>).
    /// </summary>
    public class AutocompleteParameters : BaseSearchParameters
    {
        /// <summary>
        /// Gets or sets the partial search query text. Required.
        /// </summary>
        public string Text { get; set; }
    }
}
