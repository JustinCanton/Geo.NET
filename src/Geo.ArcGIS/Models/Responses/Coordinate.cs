// <copyright file="Coordinate.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A coordinate lat/lng combination.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Gets or sets the longitude of the coordinate.
        /// </summary>
        [JsonProperty("x")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the coordinate.
        /// </summary>
        [JsonProperty("y")]
        public double Latitude { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Longitude},{Latitude}";
        }
    }
}
