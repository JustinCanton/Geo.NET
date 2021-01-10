// <copyright file="Score.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The overall score of the geocoding search.
    /// </summary>
    public class Score
    {
        /// <summary>
        /// Gets or sets the entire query score.
        /// </summary>
        [JsonProperty("queryScore")]
        public double QueryScore { get; set; }

        /// <summary>
        /// Gets or sets the score for the different fields of a location.
        /// </summary>
        [JsonProperty("fieldScore")]
        public FieldScore FieldScore { get; set; }
    }
}
