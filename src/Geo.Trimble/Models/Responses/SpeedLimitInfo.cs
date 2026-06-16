// <copyright file="SpeedLimitInfo.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Speed limit information returned in a Trimble Maps reverse geocoding response.
    /// </summary>
    public class SpeedLimitInfo
    {
        /// <summary>
        /// Gets or sets the speed limit value.
        /// </summary>
        [JsonPropertyName("Speed")]
        public int Speed { get; set; }

        /// <summary>
        /// Gets or sets the speed limit type classification.
        /// </summary>
        [JsonPropertyName("SpeedType")]
        public int SpeedType { get; set; }

        /// <summary>
        /// Gets or sets the road link identifiers.
        /// </summary>
        [JsonPropertyName("LinkIds")]
        public long LinkIds { get; set; }

        /// <summary>
        /// Gets or sets the road class designation.
        /// </summary>
        [JsonPropertyName("RoadClass")]
        public string RoadClass { get; set; }

        /// <summary>
        /// Gets or sets the measurement units (KPH or MPH).
        /// </summary>
        [JsonPropertyName("Units")]
        public string Units { get; set; }
    }
}
