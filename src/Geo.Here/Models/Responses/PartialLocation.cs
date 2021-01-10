// <copyright file="PartialLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Geo.Here.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// Contains a partial view of the location.
    /// </summary>
    public class PartialLocation : BaseLocation
    {
        /// <summary>
        /// Gets or sets the type of the house number.
        /// </summary>
        [JsonProperty("houseNumberType")]
        public HouseNumberType HouseNumberType { get; set; }

        /// <summary>
        /// Gets or sets the type of the address block.
        /// </summary>
        [JsonProperty("addressBlockType")]
        public AddressBlockType AddressBlockType { get; set; }

        /// <summary>
        /// Gets or sets the type of the locality.
        /// </summary>
        [JsonProperty("localityType")]
        public LocalityType LocalityType { get; set; }

        /// <summary>
        /// Gets or sets the type of the administrative area.
        /// </summary>
        [JsonProperty("administrativeAreaType")]
        public AdministrativeAreaType AdministrativeAreaType { get; set; }

        /// <summary>
        /// Gets or sets the postal address of the result item.
        /// </summary>
        [JsonProperty("address")]
        public Address Address { get; set; }

        /// <summary>
        /// Gets the list of categories assigned to this place.
        /// </summary>
        [JsonProperty("categories")]
        public IList<Category> Categories { get; } = new List<Category>();

        /// <summary>
        /// Gets the list of food types assigned to this place.
        /// </summary>
        [JsonProperty("foodTypes")]
        public IList<Category> FoodTypes { get; } = new List<Category>();
    }
}
