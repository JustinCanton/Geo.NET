// <copyright file="PlaceAutocomplete.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Geo.Google.Enums;

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
        [JsonPropertyName("distance_meters")]
        public int Distance { get; set; }

        /// <summary>
        /// Gets or sets an array indicates the type of the returned result.
        /// </summary>
        [JsonPropertyName("types")]
        public IList<AddressType> Types { get; set; } = new List<AddressType>();

        /// <summary>
        /// Gets or sets pre-formatted text that can be shown in your autocomplete results.
        /// </summary>
        [JsonPropertyName("matched_substrings")]
        public IList<StructureFormatting> StructuredFormatting { get; set; } = new List<StructureFormatting>();
    }
}
