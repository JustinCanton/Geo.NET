// <copyright file="QueryAutocomplete.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A query autocomplete result returned by Google.
    /// </summary>
    public class QueryAutocomplete : BaseResponse
    {
        /// <summary>
        /// Gets or sets the human-readable name for the returned result.
        /// For establishment results, this is usually the business name.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets an array of terms identifying each section of the returned description.
        /// </summary>
        [JsonProperty("terms")]
        public List<Term> Terms { get; } = new List<Term>();

        /// <summary>
        /// Gets an array with offset value and length.
        /// These describe the location of the entered term in the prediction result text, so that the term can be highlighted if desired.
        /// </summary>
        [JsonProperty("matched_substrings")]
        public List<MatchedSubstring> MatchedSubstrings { get; } = new List<MatchedSubstring>();
    }
}
