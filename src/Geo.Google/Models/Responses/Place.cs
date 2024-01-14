// <copyright file="Place.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Geo.Google.Converters;
    using Geo.Google.Enums;

    /// <summary>
    /// The base fields shared across place requests.
    /// </summary>
    /// <typeparam name="TOpeningHours">The type of the opening hours.</typeparam>
    public class Place<TOpeningHours> : BaseResponse
    {
        /// <summary>
        /// Gets or sets a string containing the human-readable address of this location.
        /// </summary>
        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; }

        /// <summary>
        /// Gets or sets the human-readable name for the returned result. For establishment results, this is usually the business name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the geometry information about the result, generally including the location (geocode) of the place and (optionally) the viewport identifying its general area of coverage.
        /// </summary>
        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        /// <summary>
        /// Gets an array of photo objects, each containing a reference to an image.
        /// A Place Search will return at most one photo object.
        /// Performing a Place Details request on the place may return up to ten photos.
        /// </summary>
        [JsonPropertyName("photos")]
        public IList<Photo> Photos { get; } = new List<Photo>();

        /// <summary>
        /// Gets or sets the place's rating, from 1.0 to 5.0, based on aggregated user reviews.
        /// </summary>
        [JsonPropertyName("rating")]
        public float Rating { get; set; }

        /// <summary>
        /// Gets or sets the URL of a recommended icon which may be displayed to the user when indicating this result.
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets an array indicates the type of the returned result.
        /// </summary>
        [JsonPropertyName("types", ItemConverterType = typeof(DefaultStringEnumConverter<AddressType>))]
        public IList<AddressType> Types { get; } = new List<AddressType>();

        /// <summary>
        /// Gets or sets a feature name of a nearby location.
        /// Often this feature refers to a street or neighbourhood within the given results.
        /// </summary>
        [JsonPropertyName("vicinity")]
        public string Vicinity { get; set; }

        /// <summary>
        /// Gets or sets an encoded location reference, derived from latitude and longitude coordinates,
        /// that represents an area: 1/8000th of a degree by 1/8000th of a degree (about 14m x 14m at the equator) or smaller.
        /// </summary>
        [JsonPropertyName("plus_code")]
        public PlusCode PlusCode { get; set; }

        /// <summary>
        /// Gets or sets the price level of the place, on a scale of 0 to 4.
        /// The exact amount indicated by a specific value will vary from region to region.
        /// </summary>
        [JsonPropertyName("price_level")]
        public PriceLevel PriceLevel { get; set; }

        /// <summary>
        /// Gets or sets the operational status of the place, if it is a business.
        /// </summary>
        [JsonPropertyName("business_status")]
        public BusinessStatus BusinessStatus { get; set; }

        /// <summary>
        /// Gets or sets the opening hours of the place.
        /// </summary>
        [JsonPropertyName("opening_hours")]
        public TOpeningHours OpeningHours { get; set; }
    }
}
