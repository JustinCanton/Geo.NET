// <copyright file="Score.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models
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
