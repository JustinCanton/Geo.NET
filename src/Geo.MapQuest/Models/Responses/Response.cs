// <copyright file="Response.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

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
        [JsonProperty("info")]
        public Information Information { get; set; }

        /// <summary>
        /// Gets or sets the options related to the request.
        /// </summary>
        [JsonProperty("options")]
        public Options Options { get; set; }

        /// <summary>
        /// Gets the results from the request.
        /// </summary>
        [JsonProperty("results")]
        public List<T> Results { get; } = new List<T>();
    }
}
