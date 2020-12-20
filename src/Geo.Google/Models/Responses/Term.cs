// <copyright file="Term.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Identifies a section of the returned description.
    /// </summary>
    public class Term
    {
        /// <summary>
        /// Gets or sets the start position of this term in the description.
        /// </summary>
        [JsonProperty("offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the text of the term.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
