// <copyright file="TimePeriod.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A structure containing the time period information.
    /// </summary>
    public class TimePeriod
    {
        /// <summary>
        /// Gets or sets the start of the hours.
        /// </summary>
        [JsonPropertyName("start")]
        public string Start { get; set; }

        /// <summary>
        /// Gets or sets the duration of this time period.
        /// </summary>
        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets the recurrence of this time period.
        /// </summary>
        [JsonPropertyName("recurrence")]
        public string Recurrence { get; set; }
    }
}
