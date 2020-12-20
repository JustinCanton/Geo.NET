// <copyright file="AddressComponent.cs" company="Geo.NET">
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
    /// A component of an address.
    /// </summary>
    public class AddressComponent
    {
        /// <summary>
        /// Gets or sets the full text description or name of the address component as returned by the Geocoder.
        /// </summary>
        [JsonProperty("long_name")]
        public string LongName { get; set; }

        /// <summary>
        /// Gets or sets an abbreviated textual name for the address component, if available.
        /// </summary>
        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        /// <summary>
        /// Gets an array indicating the type of the address component.
        /// </summary>
        [JsonProperty("types", ItemConverterType = typeof(DefaultStringEnumConverter<AddressType>))]
        public List<AddressType> Types { get; } = new List<AddressType>();
    }
}
