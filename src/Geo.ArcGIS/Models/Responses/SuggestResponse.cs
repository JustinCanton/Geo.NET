// <copyright file="SuggestResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A response for a character-by-character autocomplete suggestion to be generated for user input in a client application.
    /// </summary>
    public class SuggestResponse
    {
        /// <summary>
        /// Gets a list of suggestions based on a text input.
        /// </summary>
        [JsonProperty("suggestions")]
        public List<Suggestion> Suggestions { get; } = new List<Suggestion>();
    }
}
