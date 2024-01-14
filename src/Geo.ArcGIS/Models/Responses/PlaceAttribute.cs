// <copyright file="PlaceAttribute.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Attribute information returned when searching for an place.
    /// </summary>
    public class PlaceAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the place address information.
        /// </summary>
        [JsonPropertyName("Place_addr")]
        public string PlaceAddress { get; set; }

        /// <summary>
        /// Gets or sets the place name information.
        /// </summary>
        [JsonPropertyName("PlaceName")]
        public string PlaceName { get; set; }
    }
}
