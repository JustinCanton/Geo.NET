// <copyright file="LocationPhoneme.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Phonemes for a location.
    /// </summary>
    public class LocationPhoneme
    {
        /// <summary>
        /// Gets phonemes for the name of the place.
        /// </summary>
        [JsonProperty("placeName")]
        public List<Phoneme> PlaceNames { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the county name.
        /// </summary>
        [JsonProperty("countryName")]
        public List<Phoneme> CountryNames { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the county name.
        /// </summary>
        [JsonProperty("county")]
        public List<Phoneme> County { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the city name.
        /// </summary>
        [JsonProperty("city")]
        public List<Phoneme> City { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the subdistrict name.
        /// </summary>
        [JsonProperty("subdistrict")]
        public List<Phoneme> SubDistrict { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the name of the place.
        /// </summary>
        [JsonProperty("street")]
        public List<Phoneme> Street { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the block.
        /// </summary>
        [JsonProperty("block")]
        public List<Phoneme> Block { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the sub-block.
        /// </summary>
        [JsonProperty("subblock")]
        public List<Phoneme> SubBlock { get; } = new List<Phoneme>();
    }
}
