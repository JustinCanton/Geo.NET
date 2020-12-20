// <copyright file="QueryParseValue.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Bing.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A parsed valueof the location query string.
    /// </summary>
    public class QueryParseValue
    {
        /// <summary>
        /// Gets or sets the property name from the query string.
        /// </summary>
        [JsonProperty("property")]
        public string Property { get; set; }

        /// <summary>
        /// Gets or sets the value from the query string.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
