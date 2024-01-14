// <copyright file="LocationPhoneme.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Phonemes for a location.
    /// </summary>
    public class LocationPhoneme
    {
        /// <summary>
        /// Gets phonemes for the name of the place.
        /// </summary>
        [JsonPropertyName("placeName")]
        public IList<Phoneme> PlaceNames { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the county name.
        /// </summary>
        [JsonPropertyName("countryName")]
        public IList<Phoneme> CountryNames { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the county name.
        /// </summary>
        [JsonPropertyName("county")]
        public IList<Phoneme> County { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the city name.
        /// </summary>
        [JsonPropertyName("city")]
        public IList<Phoneme> City { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the subdistrict name.
        /// </summary>
        [JsonPropertyName("subdistrict")]
        public IList<Phoneme> SubDistrict { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the name of the place.
        /// </summary>
        [JsonPropertyName("street")]
        public IList<Phoneme> Street { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the block.
        /// </summary>
        [JsonPropertyName("block")]
        public IList<Phoneme> Block { get; } = new List<Phoneme>();

        /// <summary>
        /// Gets phonemes for the sub-block.
        /// </summary>
        [JsonPropertyName("subblock")]
        public IList<Phoneme> SubBlock { get; } = new List<Phoneme>();
    }
}
