// <copyright file="BaseResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The base response items returned from all requests.
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Gets or sets a unique identifier that can be used with other Google APIs.
        /// </summary>
        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; }
    }
}
