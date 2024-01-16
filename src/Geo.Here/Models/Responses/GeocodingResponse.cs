// <copyright file="GeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The response from a geocoding request.
    /// </summary>
    public class GeocodingResponse
    {
        /// <summary>
        /// Gets or sets the list of locations that match the geocoding request.
        /// </summary>
        [JsonPropertyName("items")]
        public IList<GeocodeLocation> Items { get; set; } = new List<GeocodeLocation>();
    }
}
