// <copyright file="RoadMetadata.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The meta data information about the road.
    /// </summary>
    public class RoadMetadata
    {
        /// <summary>
        /// Gets or sets the unit for the street speed limit.
        /// </summary>
        [JsonProperty("speedLimitUnits")]
        public string SpeedLimitUnits { get; set; }

        /// <summary>
        /// Gets or sets whether or not the road is a toll road.
        /// </summary>
        [JsonProperty("tollRoad")]
        public string ToadRoad { get; set; }

        /// <summary>
        /// Gets or sets the speed limit of the road.
        /// </summary>
        [JsonProperty("speedLimit")]
        public int SpeedLimit { get; set; }
    }
}
