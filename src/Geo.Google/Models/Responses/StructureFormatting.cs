// <copyright file="StructureFormatting.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Pre-formatted text that can be shown in your autocomplete results.
    /// </summary>
    public class StructureFormatting
    {
        /// <summary>
        /// Gets or sets the main text of a prediction, usually the name of the place.
        /// </summary>
        [JsonProperty("main_text")]
        public string MainText { get; set; }

        /// <summary>
        /// Gets or sets the secondary text of a prediction, usually the location of the place.
        /// </summary>
        [JsonProperty("secondary_text")]
        public string SecondaryText { get; set; }

        /// <summary>
        /// Gets an array with offset value and length.
        /// These describe the location of the entered term in the prediction result text, so that the term can be highlighted if desired.
        /// </summary>
        [JsonProperty("main_text_matched_substrings")]
        public IList<MatchedSubstring> MatchedSubstrings { get; } = new List<MatchedSubstring>();
    }
}
