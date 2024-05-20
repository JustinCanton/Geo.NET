// <copyright file="Language.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Language of the country associated with the location result.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Gets or sets the 2-letter language code of the given language.
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the official name of the given language.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
