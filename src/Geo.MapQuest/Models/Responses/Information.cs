// <copyright file="Information.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The general information.
    /// </summary>
    public class Information
    {
        /// <summary>
        /// Gets or sets the status code for the request.
        /// </summary>
        [JsonProperty("statuscode")]
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the copyright information for the request.
        /// </summary>
        [JsonProperty("copyright")]
        public Copyright Copyright { get; set; }

        /// <summary>
        /// Gets the extra messages for the request.
        /// </summary>
        [JsonProperty("messages")]
        public List<string> Messages { get; } = new List<string>();
    }
}
