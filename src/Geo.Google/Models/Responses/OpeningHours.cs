﻿// <copyright file="OpeningHours.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The open hours information for a place.
    /// </summary>
    public class OpeningHours
    {
        /// <summary>
        /// Gets or sets a value indicating whether the place is open now or not.
        /// </summary>
        [JsonProperty("open_now")]
        public bool OpenNow { get; set; }

        /// <summary>
        /// Gets the weekday text.
        /// </summary>
        [JsonProperty("weekday_text")]
        public List<string> WeekdayText { get; } = new List<string>();
    }
}