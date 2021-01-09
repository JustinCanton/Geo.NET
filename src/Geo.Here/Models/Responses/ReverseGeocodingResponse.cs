// <copyright file="ReverseGeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The response from a reverse geocoding request.
    /// </summary>
    public class ReverseGeocodingResponse
    {
        /// <summary>
        /// Gets the list of locations that match the reverse geocoding request.
        /// </summary>
        [JsonProperty("items")]
        public IList<GeocodeLocation> Items { get; } = new List<GeocodeLocation>();
    }
}
