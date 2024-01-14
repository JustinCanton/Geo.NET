// <copyright file="Coordinate.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Globalization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A coordinate lat/lng combination.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Gets or sets the longitude of the coordinate.
        /// </summary>
        [JsonPropertyName("x")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the coordinate.
        /// </summary>
        [JsonPropertyName("y")]
        public double Latitude { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1}", Longitude, Latitude);
        }
    }
}
