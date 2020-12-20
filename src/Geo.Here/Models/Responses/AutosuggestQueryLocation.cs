// <copyright file="AutosuggestQueryLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A response  location for the autosuggest request.
    /// </summary>
    public class AutosuggestQueryLocation : BaseLocation
    {
        /// <summary>
        /// Gets or sets the URL of the follow-up query.
        /// </summary>
        [JsonProperty("href")]
        public string Href { get; set; }

        /// <summary>
        /// Gets or sets an item describing how the parts of the response element matched the input query.
        /// </summary>
        [JsonProperty("highlights")]
        public Highlight Highlight { get; set; }
    }
}
