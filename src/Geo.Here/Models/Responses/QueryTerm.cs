// <copyright file="QueryTerm.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A suggestion for refining search query terms.
    /// </summary>
    public class QueryTerm
    {
        /// <summary>
        /// Gets or sets the term that will be suggested to the user.
        /// </summary>
        [JsonProperty("term")]
        public string Term { get; set; }

        /// <summary>
        /// Gets or sets the sub-string of the original query that is replaced by this Query Term.
        /// </summary>
        [JsonProperty("replaces")]
        public string Replaces { get; set; }

        /// <summary>
        /// Gets or sets the start index in codepoints (inclusive) of the text replaced in the original query.
        /// </summary>
        [JsonProperty("start")]
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the end index in codepoints (exclusive) of the text replaced in the original query.
        /// </summary>
        [JsonProperty("end")]
        public int End { get; set; }
    }
}
