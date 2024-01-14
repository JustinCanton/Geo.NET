// <copyright file="Coordinate.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models
{
    using System.Globalization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The coordinates (latitude, longitude) of a pin on a map corresponding to the searched place.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Gets or sets the latitude of the address. For example: "52.19404".
        /// </summary>
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the address. For example: "8.80135".
        /// </summary>
        [JsonPropertyName("lng")]
        public double Longitude { get; set; }

        /// <summary>
        /// Sets the latitude of the address. For example: "52.19404".
        /// This is a private value used to fetch data when the json has a different name.
        /// </summary>
        [JsonPropertyName("latitude")]
        private double Latitude2
        {
            set { Latitude = value; }
        }

        /// <summary>
        /// Sets the longitude of the address. For example: "8.80135".
        /// This is a private value used to fetch data when the json has a different name.
        /// </summary>
        [JsonPropertyName("longitude")]
        private double Longitude2
        {
            set { Longitude = value; }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1}", Latitude, Longitude);
        }
    }
}
