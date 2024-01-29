// <copyright file="MatchedSubstring.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A substring match information for an autocomplete result.
    /// </summary>
    public class MatchedSubstring
    {
        /// <summary>
        /// Gets or sets the length of the matched substring in the main term.
        /// </summary>
        [JsonPropertyName("length")]
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the offset of the matched substring in the main term.
        /// </summary>
        [JsonPropertyName("offset")]
        public int Offset { get; set; }
    }
}
