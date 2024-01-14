// <copyright file="Hours.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Contains the hours of operation for a location.
    /// </summary>
    public class Hours
    {
        /// <summary>
        /// Gets the text information for a set of hours.
        /// </summary>
        [JsonPropertyName("text")]
        public IList<string> Text { get; } = new List<string>();

        /// <summary>
        /// Gets or sets a value indicating whether the location is open during these hours.
        /// </summary>
        [JsonPropertyName("isOpen")]
        public bool IsOpen { get; set; }

        /// <summary>
        /// Gets the time periods for these hours.
        /// </summary>
        [JsonPropertyName("structured")]
        public IList<TimePeriod> Structured { get; } = new List<TimePeriod>();
    }
}
