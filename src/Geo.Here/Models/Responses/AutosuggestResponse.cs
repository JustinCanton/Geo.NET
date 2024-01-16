// <copyright file="AutosuggestResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The response from a autosuggest request.
    /// </summary>
    public class AutosuggestResponse
    {
        /// <summary>
        /// Gets or sets the list of locations that match the discover request.
        /// </summary>
        [JsonPropertyName("items")]
        public IList<BaseLocation> Items { get; set; } = new List<BaseLocation>();

        /// <summary>
        /// Gets or sets the suggestions for refining individual query terms.
        /// </summary>
        [JsonPropertyName("queryTerms")]
        public IList<QueryTerm> QueryTerms { get; set; } = new List<QueryTerm>();
    }
}
