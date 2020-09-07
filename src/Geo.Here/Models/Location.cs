// <copyright file="Location.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models
{
    using System.Collections.Generic;
    using Geo.Here.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// Location matches.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets or sets the localized display name of this result item.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the result item. This ID can be used for a Look Up by ID search as well.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the location.
        /// </summary>
        [JsonProperty("resultType")]
        public ResultType ResultType { get; set; }

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
        /// Gets or sets the coordinates(latitude, longitude) of a pin on a map corresponding to the searched place.
        /// </summary>
        [JsonProperty("position")]
        public Coordinate Position { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the place you are navigating to(for example, driving or walking).
        /// This is a point on a road or in a parking lot.
        /// </summary>
        [JsonProperty("access")]
        public List<Coordinate> Access { get; set; }

        /// <summary>
        /// Gets or sets the distance from the search center to this result item in meters. For example: "172039".
        /// </summary>
        [JsonProperty("distance")]
        public long Distance { get; set; }

        /// <summary>
        /// Gets or sets the geo coordinates of the map bounding box containing the results.
        /// </summary>
        [JsonProperty("mapView")]
        public BoundingBox MapView { get; set; }

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

        /// <summary>
        /// Gets or sets a value indicating whether the House Number matched is a fallback to the closest Address Range or Point Address.
        /// </summary>
        [JsonProperty("houseNumberFallback")]
        public bool HouseNumberFallback { get; set; }

        /// <summary>
        /// Gets or sets the score for this location.
        /// </summary>
        [JsonProperty("scoring")]
        public Score Score { get; set; }
    }
}
