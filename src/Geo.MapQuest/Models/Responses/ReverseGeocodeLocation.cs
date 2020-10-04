// <copyright file="ReverseGeocodeLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The location for a reverse geocode request.
    /// </summary>
    public class ReverseGeocodeLocation : GeocodeLocation
    {
        /// <summary>
        /// Gets or sets the speed limit and toll road data, when available.
        /// </summary>
        [JsonProperty("roadMetadata")]
        public RoadMetadata RoadMetadata { get; set; }

        /// <summary>
        /// Gets or sets the nearest intersection (street pair) to a given point.
        /// </summary>
        [JsonProperty("nearestIntersection")]
        public NearestIntersection NearestIntersection { get; set; }
    }
}
