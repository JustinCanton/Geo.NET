// <copyright file="FieldScore.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
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
        /// Gets the street scores.
        /// </summary>
        [JsonProperty("streets")]
        public IList<double> Streets { get; } = new List<double>();

        /// <summary>
        /// Gets or sets the house number score.
        /// </summary>
        [JsonProperty("houseNumber")]
        public double HouseNumber { get; set; }
    }
}
