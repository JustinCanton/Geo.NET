// <copyright file="GeocodeLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A location response from a geocode method.
    /// </summary>
    public class GeocodeLocation : FullLocation
    {
        /// <summary>
        /// Gets or sets the distance from the search center to this result item in meters. For example: "172039".
        /// </summary>
        [JsonPropertyName("distance")]
        public long Distance { get; set; }

        /// <summary>
        /// Gets or sets the scoring information, which indicates for each result how well it matches to the original query.
        /// </summary>
        [JsonPropertyName("scoring")]
        public Score Scoring { get; set; }
    }
}
