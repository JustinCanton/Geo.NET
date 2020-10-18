// <copyright file="DetailsResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The Google details response object.
    /// </summary>
    public class DetailsResponse
    {
        /// <summary>
        /// Gets a list of html attributions about this listing which must be displayed to the user.
        /// </summary>
        [JsonProperty("html_attributions")]
        public List<string> HtmlAttributes { get; } = new List<string>();

        /// <summary>
        /// Gets or sets the results of the google details request.
        /// </summary>
        [JsonProperty("result")]
        public Details Result { get; set; }

        /// <summary>
        /// Gets or sets the status of the Google Geocoding API call.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
