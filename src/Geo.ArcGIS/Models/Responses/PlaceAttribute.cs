// <copyright file="PlaceAttribute.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Attribute information returned when searching for an place.
    /// </summary>
    public class PlaceAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the place address information.
        /// </summary>
        [JsonProperty("Place_addr")]
        public string PlaceAddress { get; set; }

        /// <summary>
        /// Gets or sets the place name information.
        /// </summary>
        [JsonProperty("PlaceName")]
        public string PlaceName { get; set; }
    }
}
