// <copyright file="SunModule.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Astrology data for a location.
    /// </summary>
    public class SunModule
    {
        /// <summary>
        /// Gets or sets the sunrise information.
        /// </summary>
        [JsonPropertyName("rise")]
        public SunInformation Sunrise { get; set; }

        /// <summary>
        /// Gets or sets the sunset information.
        /// </summary>
        [JsonPropertyName("set")]
        public SunInformation Sunset { get; set; }

        /// <summary>
        /// Gets or sets the sun transit time as a UNIX timestamp (UTC).
        /// </summary>
        [JsonPropertyName("transit")]
        public uint? Transit { get; set; }
    }
}
