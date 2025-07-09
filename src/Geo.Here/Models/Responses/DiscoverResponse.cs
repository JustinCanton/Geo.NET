// <copyright file="DiscoverResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The response from a discover request.
    /// </summary>
    public class DiscoverResponse : IItemsResponse<DiscoverLocation>
    {
        /// <inheritdoc/>
        [JsonPropertyName("items")]
        public IList<DiscoverLocation> Items { get; set; } = new List<DiscoverLocation>();
    }
}
