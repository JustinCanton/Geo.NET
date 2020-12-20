// <copyright file="DayTime.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A day/time object.
    /// </summary>
    public class DayTime
    {
        /// <summary>
        /// Gets or sets a number from 0–6, corresponding to the days of the week, starting on Sunday.For example, 2 means Tuesday.
        /// </summary>
        [JsonProperty("day")]
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets a time of day in 24-hour hhmm format.Values are in the range 0000–2359. The time will be reported in the place’s time zone.
        /// </summary>
        [JsonProperty("time")]
        public int Time { get; set; }
    }
}
