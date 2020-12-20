// <copyright file="Coordinate.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Models
{
    using Geo.MapBox.Converters;
    using Newtonsoft.Json;

    /// <summary>
    /// The coordinates (latitude, longitude) of a pin on a map corresponding to the searched place.
    /// </summary>
    [JsonConverter(typeof(CoordinateConverter))]
    public class Coordinate
    {
        /// <summary>
        /// Gets or sets the latitude of the address. For example: "52.19404".
        /// </summary>
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the address. For example: "8.80135".
        /// </summary>
        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Longitude},{Latitude}";
        }
    }
}
