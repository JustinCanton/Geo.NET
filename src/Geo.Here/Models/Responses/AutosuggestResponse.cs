// <copyright file="AutosuggestResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Geo.Here.Converters;
    using Newtonsoft.Json;

    /// <summary>
    /// The response from a autosuggest request.
    /// </summary>
    public class AutosuggestResponse
    {
        /// <summary>
        /// Gets the list of locations that match the discover request.
        /// </summary>
        [JsonProperty("items", ItemConverterType = typeof(AutosuggestJsonConverter))]
        public List<BaseLocation> Items { get; } = new List<BaseLocation>();

        /// <summary>
        /// Gets the suggestions for refining individual query terms.
        /// </summary>
        [JsonProperty("queryTerms")]
        public List<QueryTerm> QueryTerms { get; } = new List<QueryTerm>();
    }
}
