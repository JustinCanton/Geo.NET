// <copyright file="Boundaries.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Contains  two latitude,longitude values defining the southwest and northeast corner of the bounding box.
    /// </summary>
    public class Boundaries : BaseBounding
    {
        /// <summary>
        /// Gets or sets the northeast coordinate of the bounding box.
        /// </summary>
        [JsonProperty("northeast")]
        public Coordinate Northeast { get; set; }

        /// <summary>
        /// Gets or sets the southwest coordinate of the bounding box.
        /// </summary>
        [JsonProperty("southwest")]
        public Coordinate Southwest { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Southwest.Latitude},{Southwest.Longitude}|{Northeast.Latitude},{Northeast.Longitude}";
        }
    }
}
