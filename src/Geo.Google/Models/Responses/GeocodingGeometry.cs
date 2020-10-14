// <copyright file="GeocodingGeometry.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using Geo.Google.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// The geometry object returned by geocoding requests.
    /// </summary>
    public class GeocodingGeometry : Geometry
    {
        /// <summary>
        /// Gets or sets additional data about the specified location.
        /// </summary>
        [JsonProperty("location_type")]
        public LocationType LocationType { get; set; }

        /// <summary>
        /// Gets or sets the (optionally returned) boundaries which can fully contain the returned result.
        /// </summary>
        [JsonProperty("bounds")]
        public Boundaries Bounds { get; set; }
    }
}
