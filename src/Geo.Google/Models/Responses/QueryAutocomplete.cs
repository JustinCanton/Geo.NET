// <copyright file="QueryAutocomplete.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A query autocomplete result returned by Google.
    /// </summary>
    public class QueryAutocomplete : BaseResponse
    {
        /// <summary>
        /// Gets or sets the human-readable name for the returned result.
        /// For establishment results, this is usually the business name.
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets an array of terms identifying each section of the returned description.
        /// </summary>
        [JsonPropertyName("terms")]
        public IList<Term> Terms { get; set; } = new List<Term>();

        /// <summary>
        /// Gets or sets an array with offset value and length.
        /// These describe the location of the entered term in the prediction result text, so that the term can be highlighted if desired.
        /// </summary>
        [JsonPropertyName("matched_substrings")]
        public IList<MatchedSubstring> MatchedSubstrings { get; set; } = new List<MatchedSubstring>();
    }
}
