// <copyright file="FieldScore.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The scores for the different location information.
    /// </summary>
    public class FieldScore
    {
        /// <summary>
        /// Gets or sets the country score.
        /// </summary>
        [JsonProperty("country")]
        public double Country { get; set; }

        /// <summary>
        /// Gets or sets the city score.
        /// </summary>
        [JsonProperty("city")]
        public double City { get; set; }

        /// <summary>
        /// Gets or sets the street scores.
        /// </summary>
        [JsonProperty("streets")]
        public List<double> Streets { get; set; }

        /// <summary>
        /// Gets or sets the house number score.
        /// </summary>
        [JsonProperty("houseNumber")]
        public double HouseNumber { get; set; }
    }
}
