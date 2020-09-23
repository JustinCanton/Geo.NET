// <copyright file="Chain.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A chain assigned to a location.
    /// </summary>
    public class Chain
    {
        /// <summary>
        /// Gets or sets the identifier number for an associated chain.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
