// <copyright file="IndexMatch.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Index of a match based on some parameters.
    /// </summary>
    public class IndexMatch
    {
        /// <summary>
        /// Gets or sets the first index of the matched range (0-based indexing).
        /// </summary>
        [JsonProperty("start")]
        public int Start { get; set; }

        /// <summary>
        /// Gets or sets the last index of the matched range (0-based indexing).
        /// </summary>
        [JsonProperty("end")]
        public int End { get; set; }
    }
}
