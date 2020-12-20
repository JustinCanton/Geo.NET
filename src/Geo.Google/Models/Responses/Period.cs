// <copyright file="Period.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Open/close period for a place.
    /// </summary>
    public class Period
    {
        /// <summary>
        /// Gets or sets a pair of day and time objects describing when the place opens.
        /// </summary>
        [JsonProperty("open")]
        public DayTime Open { get; set; }

        /// <summary>
        /// Gets or sets a pair of day and time objects describing when the place closes.
        /// Note: If a place is always open, the close section will be missing from the response.
        /// Clients can rely on always-open being represented as an open period containing day with value 0 and time with value 0000, and no close.
        /// </summary>
        [JsonProperty("close")]
        public DayTime Close { get; set; }
    }
}
