// <copyright file="MatchedSubstring.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A substring match information for an autocomplete result.
    /// </summary>
    public class MatchedSubstring
    {
        /// <summary>
        /// Gets or sets the length of the matched substring in the main term.
        /// </summary>
        [JsonProperty("length")]
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the offset of the matched substring in the main term.
        /// </summary>
        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}
