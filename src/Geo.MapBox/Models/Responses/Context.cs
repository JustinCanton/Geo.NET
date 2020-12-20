// <copyright file="Context.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Models.Responses
{
    using System.Collections.Generic;
    using Geo.MapBox.Converters;
    using Newtonsoft.Json;

    /// <summary>
    /// The context information for a location.
    /// </summary>
    [JsonConverter(typeof(ContextConverter))]
    public class Context
    {
        /// <summary>
        /// Gets or sets the id of the context item.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the wikidata for the context item.
        /// </summary>
        [JsonProperty("wikidata")]
        public string Wikidata { get; set; }

        /// <summary>
        /// Gets or sets the short code of the context item.
        /// </summary>
        [JsonProperty("short_code")]
        public string ShortCode { get; set; }

        /// <summary>
        /// Gets the text items of the context.
        /// </summary>
        public List<ContextText> ContextText { get; } = new List<ContextText>();
    }
}