// <copyright file="Details.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The details information for a place.
    /// </summary>
    public class Details : Place<OpeningHoursWithPeriods>
    {
        /// <summary>
        /// Gets or sets an array containing the separate components applicable to this address.
        /// </summary>
        [JsonPropertyName("address_components")]
        public IList<AddressComponent> AddressComponents { get; set; } = new List<AddressComponent>();

        /// <summary>
        /// Gets or sets a representation of the place's address in the adr microformat (http://microformats.org/wiki/adr).
        /// </summary>
        [JsonPropertyName("adr_address")]
        public string AdrAddress { get; set; }

        /// <summary>
        /// Gets or sets the place's phone number in its local format.
        /// For example, the FormattedPhoneNumber for Google's Sydney, Australia office is (02) 9374 4000.
        /// </summary>
        [JsonPropertyName("formatted_phone_number")]
        public string FormattedPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the place's phone number in international format.
        /// International format includes the country code, and is prefixed with the plus (+) sign.
        /// For example, the InternationalPhoneNumber for Google's Sydney, Australia office is +61 2 9374 4000.
        /// </summary>
        [JsonPropertyName("international_phone_number")]
        public string InternationalPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the URL of the official Google page for this place.
        /// This will be the Google-owned page that contains the best available information about the place.
        /// Applications must link to or embed this page on any screen that shows detailed results about the place to the user.
        /// </summary>
        [JsonPropertyName("url")]
        public Uri Url { get; set; }

        /// <summary>
        /// Gets or sets the number of minutes this place’s current timezone is offset from UTC.
        /// For example, for places in Sydney, Australia during daylight saving time this would be 660 (+11 hours from UTC),
        /// and for places in California outside of daylight saving time this would be -480 (-8 hours from UTC).
        /// </summary>
        [JsonPropertyName("utc_offset")]
        public int UtcOffset { get; set; }

        /// <summary>
        /// Gets or sets the authoritative website for this place, such as a business' homepage.
        /// </summary>
        [JsonPropertyName("website")]
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets an array of up to five reviews.
        /// </summary>
        [JsonPropertyName("reviews")]
        public IList<Review> Reviews { get; set; } = new List<Review>();
    }
}
