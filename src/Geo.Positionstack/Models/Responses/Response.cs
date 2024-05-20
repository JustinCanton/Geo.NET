// <copyright file="Response.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The result of a request.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets the data with the location information.
        /// </summary>
        [JsonPropertyName("data")]
        public Data Data { get; set; }
    }
}
