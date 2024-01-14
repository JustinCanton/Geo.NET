// <copyright file="SuggestResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A response for a character-by-character autocomplete suggestion to be generated for user input in a client application.
    /// </summary>
    public class SuggestResponse
    {
        /// <summary>
        /// Gets a list of suggestions based on a text input.
        /// </summary>
        [JsonPropertyName("suggestions")]
        public IList<Suggestion> Suggestions { get; } = new List<Suggestion>();
    }
}
