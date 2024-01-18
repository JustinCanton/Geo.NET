// <copyright file="AddressComponent.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Geo.Google.Enums;

    /// <summary>
    /// A component of an address.
    /// </summary>
    public class AddressComponent
    {
        /// <summary>
        /// Gets or sets the full text description or name of the address component as returned by the Geocoder.
        /// </summary>
        [JsonPropertyName("long_name")]
        public string LongName { get; set; }

        /// <summary>
        /// Gets or sets an abbreviated textual name for the address component, if available.
        /// </summary>
        [JsonPropertyName("short_name")]
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or sets an array indicating the type of the address component.
        /// </summary>
        [JsonPropertyName("types")]
        public IList<AddressType> Types { get; set; } = new List<AddressType>();
    }
}
