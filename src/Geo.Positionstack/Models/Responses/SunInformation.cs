// <copyright file="SunInformation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Information about the sun and its rising and setting.
    /// </summary>
    public class SunInformation
    {
        /// <summary>
        /// Gets or sets the sunrise/sunset time as a UNIX timestamp (UTC).
        /// </summary>
        [JsonPropertyName("time")]
        public uint? Time { get; set; }

        /// <summary>
        /// Gets or sets the astronomical sunrise/sunset time as a UNIX timestamp (UTC).
        /// </summary>
        [JsonPropertyName("astronomical")]
        public uint? Astronomical { get; set; }

        /// <summary>
        /// Gets or sets the civil sunrise/sunset time as a UNIX timestamp (UTC).
        /// </summary>
        [JsonPropertyName("civil")]
        public uint? Civil { get; set; }

        /// <summary>
        /// Gets or sets the nautical sunrise/sunset time as a UNIX timestamp (UTC).
        /// </summary>
        [JsonPropertyName("nautical")]
        public uint? Nautical { get; set; }
    }
}
