// <copyright file="BaseResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The base resopnse items returned from all requests.
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Gets or sets a unique identifier that can be used with other Google APIs.
        /// </summary>
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
    }
}
