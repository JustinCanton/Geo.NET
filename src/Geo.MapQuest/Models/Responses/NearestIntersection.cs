// <copyright file="NearestIntersection.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The nearest intersection information.
    /// </summary>
    public class NearestIntersection
    {
        /// <summary>
        /// Gets or sets the nearest street name.
        /// </summary>
        [JsonProperty("streetDisplayName")]
        public string StreetDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the distance in meters to the nearest intersection.
        /// </summary>
        [JsonProperty("distanceMeters")]
        public double DistanceMeters { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the nearest intersection.
        /// </summary>
        [JsonProperty("latLng")]
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Gets or sets the label of the nearest intersection.
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
