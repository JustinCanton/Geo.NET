// <copyright file="LocationInformation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// A location from ArcGIS.
    /// </summary>
    public class LocationInformation
    {
        /// <summary>
        /// Gets or sets the address of this match.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets The lat/lng coordinates of this match.
        /// </summary>
        [JsonPropertyName("location")]
        public Coordinate Location { get; set; }

        /// <summary>
        /// Gets or sets the confidence level of the geocoder in this match, on a scale of 1-100.
        /// </summary>
        [JsonPropertyName("score")]
        public double Score { get; set; }

        /// <summary>
        /// Gets or sets any additional fields requested.
        /// This will change depending on what call candidate call is placed.
        /// This will contain either PlaceName and PlaceAddress or MatchAddress and AddressType.
        /// </summary>
        [JsonPropertyName("attributes")]
        public Attribute Attributes { get; set; }
    }
}
