// <copyright file="PlaceAutocomplete.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Geo.Google.Converters;
    using Geo.Google.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// A place autocomplete result returned by Google.
    /// </summary>
    public class PlaceAutocomplete : QueryAutocomplete
    {
        /// <summary>
        /// Gets or sets an integer indicating the straight-line distance between the predicted place, and the specified origin point, in meters.
        /// This field is only returned when the origin point is specified in the request.
        /// This field is not returned in predictions of type route.
        /// </summary>
        [JsonProperty("distance_meters")]
        public int Distance { get; set; }

        /// <summary>
        /// Gets an array indicates the type of the returned result.
        /// </summary>
        [JsonProperty("types", ItemConverterType = typeof(DefaultStringEnumConverter<AddressType>))]
        public List<AddressType> Types { get; } = new List<AddressType>();

        /// <summary>
        /// Gets pre-formatted text that can be shown in your autocomplete results.
        /// </summary>
        [JsonProperty("matched_substrings")]
        public List<StructureFormatting> StructuredFormatting { get; } = new List<StructureFormatting>();
    }
}
