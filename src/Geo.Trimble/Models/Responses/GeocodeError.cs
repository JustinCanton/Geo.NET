// <copyright file="GeocodeError.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// An error or warning returned in a Trimble Maps geocoding response.
    /// </summary>
    public class GeocodeError
    {
        /// <summary>
        /// Gets or sets the error type (Warning or Exception).
        /// </summary>
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [JsonPropertyName("Code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the legacy numeric error code.
        /// </summary>
        [JsonPropertyName("LegacyErrorCode")]
        public int? LegacyErrorCode { get; set; }

        /// <summary>
        /// Gets or sets a human-readable description of the error.
        /// </summary>
        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }
}
