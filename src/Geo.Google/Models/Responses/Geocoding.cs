// <copyright file="Geocoding.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Geo.Google.Converters;
    using Geo.Google.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// A geocoding result returned by Google.
    /// </summary>
    public class Geocoding : BaseResponse
    {
        /// <summary>
        /// Gets or sets a string containing the human-readable address of this location.
        /// </summary>
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        /// <summary>
        /// Gets an array containing the separate components applicable to this address.
        /// </summary>
        [JsonProperty("address_components")]
        public IList<AddressComponent> AddressComponents { get; } = new List<AddressComponent>();

        /// <summary>
        /// Gets an array indicates the type of the returned result.
        /// </summary>
        [JsonProperty("types", ItemConverterType = typeof(DefaultStringEnumConverter<AddressType>))]
        public IList<AddressType> Types { get; } = new List<AddressType>();

        /// <summary>
        /// Gets or sets the geometry information for the location.
        /// </summary>
        [JsonProperty("geometry")]
        public GeocodingGeometry Geometry { get; set; }

        /// <summary>
        /// Gets or sets an encoded location reference, derived from latitude and longitude coordinates,
        /// that represents an area: 1/8000th of a degree by 1/8000th of a degree (about 14m x 14m at the equator) or smaller.
        /// </summary>
        [JsonProperty("plus_code")]
        public PlusCode PlusCode { get; set; }

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
        [JsonProperty("postcode_localities")]
        public IList<string> PostcodeLocalities { get; } = new List<string>();
    }
}
