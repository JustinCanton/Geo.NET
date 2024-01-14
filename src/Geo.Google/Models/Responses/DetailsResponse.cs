﻿// <copyright file="DetailsResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The Google details response object.
    /// </summary>
    public class DetailsResponse
    {
        /// <summary>
        /// Gets a list of html attributions about this listing which must be displayed to the user.
        /// </summary>
        [JsonPropertyName("html_attributions")]
        public IList<string> HtmlAttributes { get; } = new List<string>();

        /// <summary>
        /// Gets or sets the results of the google details request.
        /// </summary>
        [JsonPropertyName("result")]
        public Details Result { get; set; }

        /// <summary>
        /// Gets or sets the status of the Google Geocoding API call.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
