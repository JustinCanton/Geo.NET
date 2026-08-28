// <copyright file="TimeZone.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The time zone information of a Geoapify geocoding result.
    /// </summary>
    public class TimeZone
    {
        /// <summary>
        /// Gets or sets the IANA name of the time zone, for example "America/New_York".
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the alternative name of the time zone.
        /// </summary>
        [JsonPropertyName("name_alt")]
        public string NameAlternative { get; set; }

        /// <summary>
        /// Gets or sets the standard time offset from UTC, for example "-05:00".
        /// </summary>
        [JsonPropertyName("offset_STD")]
        public string OffsetStandard { get; set; }

        /// <summary>
        /// Gets or sets the standard time offset from UTC in seconds.
        /// </summary>
        [JsonPropertyName("offset_STD_seconds")]
        public int? OffsetStandardSeconds { get; set; }

        /// <summary>
        /// Gets or sets the daylight saving time offset from UTC, for example "-04:00".
        /// </summary>
        [JsonPropertyName("offset_DST")]
        public string OffsetDaylightSaving { get; set; }

        /// <summary>
        /// Gets or sets the daylight saving time offset from UTC in seconds.
        /// </summary>
        [JsonPropertyName("offset_DST_seconds")]
        public int? OffsetDaylightSavingSeconds { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation of the standard time, for example "EST".
        /// </summary>
        [JsonPropertyName("abbreviation_STD")]
        public string AbbreviationStandard { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation of the daylight saving time, for example "EDT".
        /// </summary>
        [JsonPropertyName("abbreviation_DST")]
        public string AbbreviationDaylightSaving { get; set; }
    }
}
