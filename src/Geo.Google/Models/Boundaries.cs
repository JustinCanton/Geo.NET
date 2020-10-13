// <copyright file="Boundaries.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
