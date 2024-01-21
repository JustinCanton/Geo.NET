// <copyright file="Chain.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A chain assigned to a location.
    /// </summary>
    public class Chain
    {
        /// <summary>
        /// Gets or sets the identifier number for an associated chain.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}
