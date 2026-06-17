// <copyright file="GeocodeResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A geocoding result returned by the Trimble Maps API.
    /// </summary>
    public class GeocodeResponse
    {
        /// <summary>
        /// Gets or sets the matched address.
        /// </summary>
        [JsonPropertyName("Address")]
        public GeocodeAddress Address { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the matched location.
        /// </summary>
        [JsonPropertyName("Coords")]
        public GeocodeCoords Coords { get; set; }

        /// <summary>
        /// Gets or sets the formatted label for the location.
        /// </summary>
        [JsonPropertyName("Label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the place name, which may include a Trimble place ID.
        /// </summary>
        [JsonPropertyName("PlaceName")]
        public string PlaceName { get; set; }

        /// <summary>
        /// Gets or sets the numeric geographic region code.
        /// </summary>
        [JsonPropertyName("Region")]
        public int? Region { get; set; }

        /// <summary>
        /// Gets or sets the time zone name.
        /// </summary>
        [JsonPropertyName("TimeZone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the UTC offset string (e.g. "GMT-5:00").
        /// </summary>
        [JsonPropertyName("TimeZoneOffset")]
        public string TimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether daylight saving time is in effect.
        /// </summary>
        [JsonPropertyName("isDST")]
        public bool? IsDst { get; set; }

        /// <summary>
        /// Gets or sets the confidence level of the match (Exact, Good, Uncertain, or Failed).
        /// </summary>
        [JsonPropertyName("ConfidenceLevel")]
        public string ConfidenceLevel { get; set; }

        /// <summary>
        /// Gets or sets the distance in miles from the input coordinates to the nearest road.
        /// </summary>
        [JsonPropertyName("DistanceFromRoad")]
        public double? DistanceFromRoad { get; set; }

        /// <summary>
        /// Gets or sets the speed limit information for the nearest road.
        /// </summary>
        [JsonPropertyName("SpeedLimitInfo")]
        public SpeedLimitInfo SpeedLimitInfo { get; set; }

        /// <summary>
        /// Gets the errors or warnings associated with this result.
        /// </summary>
        [JsonPropertyName("Errors")]
        public IList<GeocodeError> Errors { get; } = new List<GeocodeError>();
    }
}
