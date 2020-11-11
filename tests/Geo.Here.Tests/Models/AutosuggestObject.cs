// <copyright file="AutosuggestObject.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Tests.Models
{
    using Geo.Here.Converters;
    using Geo.Here.Models.Responses;
    using Newtonsoft.Json;

    public class AutosuggestObject
    {
        [JsonConverter(typeof(AutosuggestJsonConverter))]
        public BaseLocation Autosuggest { get; set; }
    }
}
