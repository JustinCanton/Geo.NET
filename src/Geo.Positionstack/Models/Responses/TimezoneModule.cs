// <copyright file="TimezoneModule.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Timezone information for a location.
    /// </summary>
    public class TimezoneModule
    {
        /// <summary>
        /// Gets or sets the common name of the timezone.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the GMT offset of the timezone in seconds.
        /// </summary>
        [JsonPropertyName("offset_sec")]
        public int? OffsetInSeconds { get; set; }

        /// <summary>
        /// Gets or sets the GMT offset of the timezone as a string.
        /// </summary>
        [JsonPropertyName("offset_string")]
        public string OffsetString { get; set; }
    }
}
