﻿// <copyright file="OpeningHoursWithPeriods.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// An opening hours object with period information.
    /// </summary>
    public class OpeningHoursWithPeriods : OpeningHours
    {
        /// <summary>
        /// Gets an array of opening periods covering seven days, starting from Sunday, in chronological order.
        /// </summary>
        [JsonPropertyName("periods")]
        public IList<Period> Periods { get; } = new List<Period>();
    }
}
