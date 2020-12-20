// <copyright file="ReverseGeocodeResult.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The result of a reverse geocode request.
    /// </summary>
    public class ReverseGeocodeResult
    {
        /// <summary>
        /// Gets or sets the provided location properties passed in the reverse geocode request.
        /// </summary>
        [JsonProperty("providedLocation")]
        public ReverseGeocodeProvidedLocation ProvidedLocation { get; set; }

        /// <summary>
        /// Gets the locations that match the reverse geocode request.
        /// </summary>
        [JsonProperty("locations")]
        public List<ReverseGeocodeLocation> Locations { get; } = new List<ReverseGeocodeLocation>();
    }
}
