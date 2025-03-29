// <copyright file="ReverseGeocodingResult.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Ola.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class ReverseGeocodingResult
    {
        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; }

        public string Types { get; set; }

        public string Name { get; set; }

        public Geometry Geometry { get; set; }

        [JsonPropertyName("address_components")]
        public IReadOnlyList<AddressComponent> AddressComponents { get; set; }

        [JsonPropertyName("plus_code")]
        public PlusCode PlusCode { get; set; }

        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; }

        public IReadOnlyList<string> Layer { get; set; }
    }
}
