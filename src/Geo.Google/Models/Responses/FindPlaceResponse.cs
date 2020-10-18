﻿// <copyright file="FindPlaceResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The Google find place response object.
    /// </summary>
    public class FindPlaceResponse
    {
        /// <summary>
        /// Gets a list of html attributions about this listing which must be displayed to the user.
        /// </summary>
        [JsonProperty("html_attributions")]
        public List<string> HtmlAttributes { get; } = new List<string>();

        /// <summary>
        /// Gets a list of the results for the Google request.
        /// </summary>
        [JsonProperty("candidates")]
        public IEnumerable<Place<OpeningHours>> Candidates { get; } = new List<Place<OpeningHours>>();

        /// <summary>
        /// Gets or sets the status of the Google Geocoding API call.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}