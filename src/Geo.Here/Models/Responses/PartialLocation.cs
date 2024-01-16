// <copyright file="PartialLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Geo.Here.Enums;

    /// <summary>
    /// Contains a partial view of the location.
    /// </summary>
    public class PartialLocation : BaseLocation
    {
        /// <summary>
        /// Gets or sets the type of the house number.
        /// </summary>
        [JsonPropertyName("houseNumberType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public HouseNumberType HouseNumberType { get; set; }

        /// <summary>
        /// Gets or sets the type of the address block.
        /// </summary>
        [JsonPropertyName("addressBlockType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AddressBlockType AddressBlockType { get; set; }

        /// <summary>
        /// Gets or sets the type of the locality.
        /// </summary>
        [JsonPropertyName("localityType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LocalityType LocalityType { get; set; }

        /// <summary>
        /// Gets or sets the type of the administrative area.
        /// </summary>
        [JsonPropertyName("administrativeAreaType")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AdministrativeAreaType AdministrativeAreaType { get; set; }

        /// <summary>
        /// Gets or sets the postal address of the result item.
        /// </summary>
        [JsonPropertyName("address")]
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the list of categories assigned to this place.
        /// </summary>
        [JsonPropertyName("categories")]
        public IList<Category> Categories { get; set; } = new List<Category>();

        /// <summary>
        /// Gets or sets the list of food types assigned to this place.
        /// </summary>
        [JsonPropertyName("foodTypes")]
        public IList<Category> FoodTypes { get; set; } = new List<Category>();
    }
}
