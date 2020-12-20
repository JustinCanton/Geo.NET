// <copyright file="Coordinate.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Models
{
    using Geo.Bing.Converters;
    using Newtonsoft.Json;

    /// <summary>
    /// A latitude/longitude combination to form a coordinate.
    /// </summary>
    [JsonConverter(typeof(CoordinateConverter))]
    public class Coordinate
    {
        /// <summary>
        /// Gets or sets the latitude of the coordinate.
        /// </summary>
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the coordinate.
        /// </summary>
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Latitude},{Longitude}";
        }
    }
}
