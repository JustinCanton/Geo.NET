// <copyright file="Phoneme.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A phoneme of a location.
    /// </summary>
    public class Phoneme
    {
        /// <summary>
        /// Gets or sets the actual phonetic transcription in the NT-SAMPA format.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the bcp47 language code.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not it is the preferred phoneme.
        /// </summary>
        [JsonProperty("preferred")]
        public bool Preferred { get; set; }
    }
}
