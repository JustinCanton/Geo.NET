// <copyright file="DiscoverResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The response from a discover request.
    /// </summary>
    public class DiscoverResponse
    {
        /// <summary>
        /// Gets or sets the list of locations that match the discover request.
        /// </summary>
        [JsonProperty("items")]
        public List<DiscoverLocation> Items { get; set; }
    }
}
