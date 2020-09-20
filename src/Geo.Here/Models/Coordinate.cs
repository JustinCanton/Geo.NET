﻿// <copyright file="Coordinate.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The coordinates (latitude, longitude) of a pin on a map corresponding to the searched place.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Gets or sets the latitude of the address. For example: "52.19404".
        /// </summary>
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the address. For example: "8.80135".
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