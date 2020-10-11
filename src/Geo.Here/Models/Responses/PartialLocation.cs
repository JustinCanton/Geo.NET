﻿// <copyright file="PartialLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
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
        /// Gets or sets the list of categories assigned to this place.
        /// </summary>
        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the list of food types assigned to this place.
        /// </summary>
        [JsonProperty("foodTypes")]
        public List<Category> FoodTypes { get; set; }
    }
}