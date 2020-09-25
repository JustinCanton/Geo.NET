// <copyright file="Highlight.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A highlight of the search.
    /// </summary>
    public class Highlight
    {
        /// <summary>
        /// Gets or sets ranges of indexes that matched in the title attribute.
        /// </summary>
        [JsonProperty("titles")]
        public List<IndexMatch> Titles { get; set; }
    }
}
