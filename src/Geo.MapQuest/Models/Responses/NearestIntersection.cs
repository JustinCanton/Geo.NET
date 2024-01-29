// <copyright file="NearestIntersection.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The nearest intersection information.
    /// </summary>
    public class NearestIntersection
    {
        /// <summary>
        /// Gets or sets the nearest street name.
        /// </summary>
        [JsonPropertyName("streetDisplayName")]
        public string StreetDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the distance in meters to the nearest intersection.
        /// </summary>
        [JsonPropertyName("distanceMeters")]
        public double DistanceMeters { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the nearest intersection.
        /// </summary>
        [JsonPropertyName("latLng")]
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Gets or sets the label of the nearest intersection.
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }
    }
}
