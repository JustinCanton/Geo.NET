// <copyright file="QueryParseValue.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
