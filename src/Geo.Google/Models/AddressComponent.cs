// <copyright file="AddressComponent.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Geo.Google.Converters;
    using Geo.Google.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// A component of an address.
    /// </summary>
    public class AddressComponent
    {
        [JsonProperty("types", ItemConverterType = typeof(DefaultStringEnumConverter<AddressType>))]
        private readonly List<AddressType> _types = new List<AddressType>();

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
        public ReadOnlyCollection<AddressType> Types
        {
            get
            {
                return _types.AsReadOnly();
            }
        }

        /// <summary>
        /// Adds a type to the list of address component types.
        /// </summary>
        /// <param name="type">The type to add.</param>
        public void AddType(AddressType type)
        {
            _types.Add(type);
        }
    }
}
