﻿// <copyright file="GeocodeResult.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The result of a geocode request.
    /// </summary>
    public class GeocodeResult
    {
        /// <summary>
        /// Gets or sets the provided location properties passed in the geocode request.
        /// </summary>
        [JsonProperty("providedLocation")]
        public GeocodeProvidedLocation ProvidedLocation { get; set; }

        /// <summary>
        /// Gets or sets the locations that match the geocode request.
        /// </summary>
        [JsonProperty("locations")]
        public List<GeocodeLocation> Locations { get; set; }
    }
}