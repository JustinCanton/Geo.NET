// <copyright file="DiscoverResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
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
        /// Gets the list of locations that match the discover request.
        /// </summary>
        [JsonProperty("items")]
        public List<DiscoverLocation> Items { get; } = new List<DiscoverLocation>();
    }
}
