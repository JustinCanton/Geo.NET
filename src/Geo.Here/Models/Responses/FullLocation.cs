﻿// <copyright file="FullLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A location with most fields necessary.
    /// </summary>
    public class FullLocation : PartialLocation
    {
        /// <summary>
        /// Gets or sets the coordinates(latitude, longitude) of a pin on a map corresponding to the searched place.
        /// </summary>
        [JsonProperty("position")]
        public Coordinate Position { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the place you are navigating to(for example, driving or walking).
        /// This is a point on a road or in a parking lot.
        /// </summary>
        [JsonProperty("access")]
        public List<Coordinate> Access { get; set; }

        /// <summary>
        /// Gets or sets the geo coordinates of the map bounding box containing the results.
        /// </summary>
        [JsonProperty("mapView")]
        public BoundingBox MapView { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the House Number matched is a fallback to the closest Address Range or Point Address.
        /// </summary>
        [JsonProperty("houseNumberFallback")]
        public bool HouseNumberFallback { get; set; }
    }
}