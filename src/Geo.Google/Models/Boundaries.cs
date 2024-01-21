// <copyright file="Boundaries.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models
{
    using System.Globalization;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Contains two latitude,longitude values defining the southwest and northeast corner of the bounding box.
    /// </summary>
    public class Boundaries : BaseBounding
    {
        /// <summary>
        /// Gets or sets the northeast coordinate of the bounding box.
        /// </summary>
        [JsonPropertyName("northeast")]
        public Coordinate Northeast { get; set; }

        /// <summary>
        /// Gets or sets the southwest coordinate of the bounding box.
        /// </summary>
        [JsonPropertyName("southwest")]
        public Coordinate Southwest { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0},{1}|{2},{3}", Southwest.Latitude, Southwest.Longitude, Northeast.Latitude, Northeast.Longitude);
        }
    }
}
