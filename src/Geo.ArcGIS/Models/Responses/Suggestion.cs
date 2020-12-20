// <copyright file="Suggestion.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A character-by-character autocomplete suggestion to be generated for user input in a client application.
    /// </summary>
    public class Suggestion
    {
        /// <summary>
        /// Gets or sets the text of the suggestion.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets an ID attribute value that, along with the text attribute, links a suggestion to an address or place.
        /// </summary>
        [JsonProperty("magicKey")]
        public string MagicKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the suggestion item represents a collection of places, as opposed to a specific place.
        /// </summary>
        [JsonProperty("isCollection")]
        public bool IsCollection { get; set; }
    }
}
