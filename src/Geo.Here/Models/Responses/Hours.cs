// <copyright file="Hours.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains the hours of operation for a location.
    /// </summary>
    public class Hours
    {
        /// <summary>
        /// Gets or sets the text information for a set of hours.
        /// </summary>
        [JsonProperty("text")]
        public List<string> Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the location is open during these hours.
        /// </summary>
        [JsonProperty("isOpen")]
        public bool IsOpen { get; set; }

        /// <summary>
        /// Gets the time periods for these hours.
        /// </summary>
        [JsonProperty("structured")]
        public List<TimePeriod> Structured { get; } = new List<TimePeriod>();
    }
}
