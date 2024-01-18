// <copyright file="OpeningHours.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The open hours information for a place.
    /// </summary>
    public class OpeningHours
    {
        /// <summary>
        /// Gets or sets a value indicating whether the place is open now or not.
        /// </summary>
        [JsonPropertyName("open_now")]
        public bool OpenNow { get; set; }

        /// <summary>
        /// Gets or sets the weekday text.
        /// </summary>
        [JsonPropertyName("weekday_text")]
        public IList<string> WeekdayText { get; set; } = new List<string>();
    }
}
