// <copyright file="ReverseGeocodingResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Ola.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class ReverseGeocodingResponse
    {
        [JsonPropertyName("error_message")]
        public string ErrorMessage { get; set; }

        [JsonPropertyName("info_messages")]
        public IReadOnlyList<string> InfoMessages { get; set; }

        public IReadOnlyList<ReverseGeocodingResult> Results { get; set; }

        [JsonPropertyName("plus_code")]
        public PlusCode PlusCode { get; set; }

        public string Status { get; set; }
    }
}
