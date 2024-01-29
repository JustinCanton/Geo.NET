// <copyright file="Review.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A review of a place.
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Gets or sets the name of the user who submitted the review. Anonymous reviews are attributed to "A Google user".
        /// </summary>
        [JsonPropertyName("author_name")]
        public string AuthorName { get; set; }

        /// <summary>
        /// Gets or sets the URL to the user's Google Maps Local Guides profile, if available.
        /// </summary>
        [JsonPropertyName("author_url")]
        public Uri AuthorUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL to the user's profile photo, if available.
        /// </summary>
        [JsonPropertyName("profile_photo_url")]
        public Uri ProfilePhotoUrl { get; set; }

        /// <summary>
        /// Gets or sets an IETF language code indicating the language used in the user's review.
        /// This field contains the main language tag only, and not the secondary tag indicating country or region.
        /// For example, all the English reviews are tagged as 'en', and not 'en-AU' or 'en-UK' and so on.
        /// </summary>
        [JsonPropertyName("language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the user's overall rating for this place. This is a whole number, ranging from 1 to 5.
        /// </summary>
        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets the time that the review was submitted, relative to the current time.
        /// </summary>
        [JsonPropertyName("relative_time_description")]
        public string RelativeTimeDescription { get; set; }

        /// <summary>
        /// Gets or sets the user's review.
        /// When reviewing a location with Google Places, text reviews are considered optional.
        /// Therefore, this field may by empty.
        /// Note that this field may include simple HTML markup.
        /// For example, the entity reference &amp; may represent an ampersand character.
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the time that the review was submitted, measured in the number of seconds since since midnight, January 1, 1970 UTC.
        /// </summary>
        [JsonPropertyName("time")]
        public int Time { get; set; }
    }
}
