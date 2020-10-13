// <copyright file="Geocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Geo.Google.Converters;
    using Geo.Google.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// A geocoding result returned by Google.
    /// </summary>
    public class Geocoding
    {
        [JsonProperty("address_components")]
        private readonly List<AddressComponent> _addressComponents = new List<AddressComponent>();

        [JsonProperty("types", ItemConverterType = typeof(DefaultStringEnumConverter<AddressType>))]
        private readonly List<AddressType> _types = new List<AddressType>();

        [JsonProperty("postcode_localities")]
        private readonly List<string> _postcodeLocalities = new List<string>();

        /// <summary>
        /// Gets or sets a string containing the human-readable address of this location.
        /// </summary>
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        /// <summary>
        /// Gets an array containing the separate components applicable to this address.
        /// </summary>
        public ReadOnlyCollection<AddressComponent> AddressComponents
        {
            get
            {
                return _addressComponents.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets an array indicates the type of the returned result.
        /// </summary>
        public ReadOnlyCollection<AddressType> Types
        {
            get
            {
                return _types.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets or sets the geometry information for the location.
        /// </summary>
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        /// <summary>
        /// Gets or sets an encoded location reference, derived from latitude and longitude coordinates,
        /// that represents an area: 1/8000th of a degree by 1/8000th of a degree (about 14m x 14m at the equator) or smaller.
        /// </summary>
        [JsonProperty("plus_code")]
        public PlusCode PlusCode { get; set; }

        /// <summary>
        /// Gets or sets a unique identifier that can be used with other Google APIs.
        /// </summary>
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the geocoder did not return an exact match for the original request,
        /// though it was able to match part of the requested address.
        /// </summary>
        [JsonProperty("partial_match")]
        public bool PartialMatch { get; set; }

        /// <summary>
        /// Gets an array denoting all the localities contained in a postal code.
        /// This is only populated when the result is a postal code that contains multiple localities.
        /// </summary>
        public ReadOnlyCollection<string> PostcodeLocalities
        {
            get
            {
                return _postcodeLocalities.AsReadOnly();
            }
        }

        /// <summary>
        /// Adds an address component to the list of geocoding address components.
        /// </summary>
        /// <param name="addressComponent">A <see cref="AddressComponent"/> to add.</param>
        public void AddAddressComponent(AddressComponent addressComponent)
        {
            _addressComponents.Add(addressComponent);
        }

        /// <summary>
        /// Adds a type to the list of address component types.
        /// </summary>
        /// <param name="type">The type to add.</param>
        public void AddType(AddressType type)
        {
            _types.Add(type);
        }

        /// <summary>
        /// Adds a postcode locality to the list of postcode localities.
        /// </summary>
        /// <param name="postcodeLocality">The postcode locality to add.</param>
        public void AddPostcodeLocality(string postcodeLocality)
        {
            _postcodeLocalities.Add(postcodeLocality);
        }
    }
}
