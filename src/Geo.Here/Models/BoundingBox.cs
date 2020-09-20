﻿// <copyright file="BoundingBox.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The north/south/east/west bounding box for a map view.
    /// </summary>
    public class BoundingBox
    {
        /// <summary>
        /// Gets or sets the longitude of the western-side of the box.For example: "8.80068"
        /// </summary>
        [JsonProperty("west")]
        public double West { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the southern-side of the box. For example: "52.19333".
        /// </summary>
        [JsonProperty("south")]
        public double South { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the eastern-side of the box. For example: "8.8167".
        /// </summary>
        [JsonProperty("east")]
        public double East { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the northern-side of the box. For example: "52.19555"
        /// </summary>
        [JsonProperty("north")]
        public double North { get; set; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{West},{South},{East},{North}";
        }
    }
}