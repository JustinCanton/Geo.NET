// <copyright file="Rank.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The ranking information describing how well a Geoapify geocoding result matches the request.
    /// </summary>
    public class Rank
    {
        /// <summary>
        /// Gets or sets the importance of the result relative to other locations.
        /// </summary>
        [JsonPropertyName("importance")]
        public double? Importance { get; set; }

        /// <summary>
        /// Gets or sets the popularity of the result, based on the number of nearby places of interest.
        /// </summary>
        [JsonPropertyName("popularity")]
        public double? Popularity { get; set; }

        /// <summary>
        /// Gets or sets the overall confidence of the match, between 0 and 1.
        /// </summary>
        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }

        /// <summary>
        /// Gets or sets the confidence that the city of the result matches the request, between 0 and 1.
        /// </summary>
        [JsonPropertyName("confidence_city_level")]
        public double? ConfidenceCityLevel { get; set; }

        /// <summary>
        /// Gets or sets the confidence that the street of the result matches the request, between 0 and 1.
        /// </summary>
        [JsonPropertyName("confidence_street_level")]
        public double? ConfidenceStreetLevel { get; set; }

        /// <summary>
        /// Gets or sets the confidence that the building of the result matches the request, between 0 and 1.
        /// </summary>
        [JsonPropertyName("confidence_building_level")]
        public double? ConfidenceBuildingLevel { get; set; }

        /// <summary>
        /// Gets or sets the type of the match, for example "full_match", "inner_part", or "match_by_building".
        /// </summary>
        [JsonPropertyName("match_type")]
        public string MatchType { get; set; }
    }
}
