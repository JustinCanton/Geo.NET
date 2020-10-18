// <copyright file="OpeningHoursWithPeriods.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// An opening hours object with period information.
    /// </summary>
    public class OpeningHoursWithPeriods : OpeningHours
    {
        /// <summary>
        /// Gets an array of opening periods covering seven days, starting from Sunday, in chronological order.
        /// </summary>
        [JsonProperty("periods")]
        public List<Period> Periods { get; } = new List<Period>();
    }
}
