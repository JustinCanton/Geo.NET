// <copyright file="TimePeriod.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A structure containing the time period information.
    /// </summary>
    public class TimePeriod
    {
        /// <summary>
        /// Gets or sets the start of the hours.
        /// </summary>
        [JsonProperty("start")]
        public string Start { get; set; }

        /// <summary>
        /// Gets or sets the duration of this time period.
        /// </summary>
        [JsonProperty("duration")]
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets the recurrence of this time period.
        /// </summary>
        [JsonProperty("recurrence")]
        public string Recurrence { get; set; }
    }
}
