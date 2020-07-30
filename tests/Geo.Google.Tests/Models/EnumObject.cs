// <copyright file="EnumObject.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
