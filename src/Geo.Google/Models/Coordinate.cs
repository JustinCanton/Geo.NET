// <copyright file="Coordinate.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// A latitude/longitude pair forming a coordinate.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Gets or sets the latitude of a location.
        /// </summary>
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of a location.
        /// </summary>
        [JsonProperty("lng")]
        public double Longitude { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Latitude},{Longitude}";
        }
    }
}
