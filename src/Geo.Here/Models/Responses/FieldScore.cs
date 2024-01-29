﻿// <copyright file="FieldScore.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The scores for the different location information.
    /// </summary>
    public class FieldScore
    {
        /// <summary>
        /// Gets or sets the country score.
        /// </summary>
        [JsonPropertyName("country")]
        public double Country { get; set; }

        /// <summary>
        /// Gets or sets the city score.
        /// </summary>
        [JsonPropertyName("city")]
        public double City { get; set; }

        /// <summary>
        /// Gets or sets the street scores.
        /// </summary>
        [JsonPropertyName("streets")]
        public IList<double> Streets { get; set; } = new List<double>();

        /// <summary>
        /// Gets or sets the house number score.
        /// </summary>
        [JsonPropertyName("houseNumber")]
        public double HouseNumber { get; set; }
    }
}
