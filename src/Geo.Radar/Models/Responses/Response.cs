// <copyright file="Response.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The result of a request.
    /// </summary>
    /// <typeparam name="T">The type of the address.</typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Gets or sets the metadata associate with the request.
        /// </summary>
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        /// <summary>
        /// Gets or sets the addresses that are associated with the request.
        /// </summary>
        [JsonPropertyName("addresses")]
        public IList<T> Addresses { get; set; } = new List<T>();
    }
}
