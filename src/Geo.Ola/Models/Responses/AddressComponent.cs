// <copyright file="AddressComponent.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Ola.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class AddressComponent
    {
        public IReadOnlyList<string> Types { get; set; }

        [JsonPropertyName("short_name")]
        public string ShortName { get; set; }

        [JsonPropertyName("long_name")]
        public string LongName { get; set; }
    }
}
