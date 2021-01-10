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
        public IList<Phoneme> PlaceNames { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the county name.
        /// </summary>
        [JsonProperty("countryName")]
        public IList<Phoneme> CountryNames { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the county name.
        /// </summary>
        [JsonProperty("county")]
        public IList<Phoneme> County { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the city name.
        /// </summary>
        [JsonProperty("city")]
        public IList<Phoneme> City { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the subdistrict name.
        /// </summary>
        [JsonProperty("subdistrict")]
        public IList<Phoneme> SubDistrict { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the name of the place.
        /// </summary>
        [JsonProperty("street")]
        public IList<Phoneme> Street { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the block.
        /// </summary>
        [JsonProperty("block")]
        public IList<Phoneme> Block { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the sub-block.
        /// </summary>
        [JsonProperty("subblock")]
        public IList<Phoneme> SubBlock { get; } = new List<Phoneme>();
    }
}
