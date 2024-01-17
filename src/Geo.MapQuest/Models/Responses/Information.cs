// <copyright file="Information.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The general information.
    /// </summary>
    public class Information
    {
        /// <summary>
        /// Gets or sets the status code for the request.
        /// </summary>
        [JsonPropertyName("statuscode")]
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the copyright information for the request.
        /// </summary>
        [JsonPropertyName("copyright")]
        public Copyright Copyright { get; set; }

        /// <summary>
        /// Gets or sets the extra messages for the request.
        /// </summary>
        [JsonPropertyName("messages")]
        public IList<string> Messages { get; set; } = new List<string>();
    }
}
