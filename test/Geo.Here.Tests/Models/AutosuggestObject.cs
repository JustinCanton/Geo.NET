// <copyright file="AutosuggestObject.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
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
