// <copyright file="LookupResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Models.Responses
{
    using System.Collections.Generic;

    /// <summary>
    /// The response from a Nominatim lookup request.
    /// Returns a list of places for the given OSM IDs.
    /// </summary>
    public class LookupResponse : List<NominatimPlace>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LookupResponse"/> class.
        /// </summary>
        public LookupResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupResponse"/> class.
        /// </summary>
        /// <param name="places">The collection of places to initialize with.</param>
        public LookupResponse(IEnumerable<NominatimPlace> places)
            : base(places)
        {
        }
    }
}