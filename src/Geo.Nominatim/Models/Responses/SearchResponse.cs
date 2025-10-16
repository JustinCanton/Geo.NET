// <copyright file="SearchResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Models.Responses
{
    using System.Collections.Generic;

    /// <summary>
    /// The response from a Nominatim search request.
    /// Returns a list of places matching the search query.
    /// </summary>
    public class SearchResponse : List<NominatimPlace>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResponse"/> class.
        /// </summary>
        public SearchResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResponse"/> class.
        /// </summary>
        /// <param name="places">The collection of places to initialize with.</param>
        public SearchResponse(IEnumerable<NominatimPlace> places)
            : base(places)
        {
        }
    }
}