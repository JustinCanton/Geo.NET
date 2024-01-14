// <copyright file="Response.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The response from a MapQuest request.
    /// </summary>
    /// <typeparam name="T">The type of request result.</typeparam>
    public class Response<T>
        where T : class
    {
        /// <summary>
        /// Gets or sets the information related to the request.
        /// </summary>
        [JsonPropertyName("info")]
        public Information Information { get; set; }

        /// <summary>
        /// Gets or sets the options related to the request.
        /// </summary>
        [JsonPropertyName("options")]
        public Options Options { get; set; }

        /// <summary>
        /// Gets the results from the request.
        /// </summary>
        [JsonPropertyName("results")]
        public IList<T> Results { get; } = new List<T>();
    }
}
