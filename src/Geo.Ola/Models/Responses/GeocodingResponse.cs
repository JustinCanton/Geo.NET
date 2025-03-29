// <copyright file="GeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Ola.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class GeocodingResponse
    {
        public IReadOnlyList<GeocodingResult> GeocodingResults { get; set; }

        public string Status { get; set; }

        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }
    }
}
