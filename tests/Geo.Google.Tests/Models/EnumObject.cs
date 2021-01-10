// <copyright file="EnumObject.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Tests.Models
{
    using Geo.Google.Converters;
    using Geo.Google.Tests.Enums;
    using Newtonsoft.Json;

    public class EnumObject
    {
        [JsonConverter(typeof(DefaultStringEnumConverter<Test>))]
        public Test Value { get; set; }
    }
}
