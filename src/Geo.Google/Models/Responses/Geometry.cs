// <copyright file="Geometry.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using Geo.Google.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains the geometry information for the location.
    /// </summary>
    public class Geometry
    {
        /// <summary>
        /// Gets or sets the geocoded latitude,longitude value.
        /// </summary>
        [JsonProperty("location")]
        public Coordinate Location { get; set; }

        /// <summary>
        /// Gets or sets additional data about the specified location.
        /// </summary>
        [JsonProperty("location_type")]
        public LocationType LocationType { get; set; }

        /// <summary>
        /// Gets or sets the recommended viewport for displaying the returned result,
        /// specified as two latitude,longitude values defining the southwest and northeast corner of the viewport bounding box.
        /// </summary>
        [JsonProperty("viewport")]
        public Boundaries Viewport { get; set; }

        /// <summary>
        /// Gets or sets the (optionally returned) boundaries which can fully contain the returned result.
        /// </summary>
        [JsonProperty("bounds")]
        public Boundaries Bounds { get; set; }
    }
}
