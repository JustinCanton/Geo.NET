// <copyright file="LocationInformation.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using Geo.ArcGIS.Converters;
    using Newtonsoft.Json;

    /// <summary>
    /// A location from ArcGIS.
    /// </summary>
    public class LocationInformation
    {
        /// <summary>
        /// Gets or sets the address of this match.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets The lat/lng coordinates of this match.
        /// </summary>
        [JsonProperty("location")]
        public Coordinate Location { get; set; }

        /// <summary>
        /// Gets or sets the confidence level of the geocoder in this match, on a scale of 1-100.
        /// </summary>
        [JsonProperty("score")]
        public double Score { get; set; }

        /// <summary>
        /// Gets or sets any additional fields requested.
        /// This will change depending on what call candidate call is placed.
        /// This will contain either PlaceName and PlaceAddress or MatchAddress and AddressType.
        /// </summary>
        [JsonProperty("attributes")]
        public Attribute Attributes { get; set; }
    }
}
