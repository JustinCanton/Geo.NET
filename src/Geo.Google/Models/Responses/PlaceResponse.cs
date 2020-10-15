// <copyright file="PlaceResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The Google place response object.
    /// </summary>
    /// <typeparam name="TResponseType">The type of the response items.</typeparam>
    public class PlaceResponse<TResponseType>
    {
        /// <summary>
        /// Gets a list of html attributions about this listing which must be displayed to the user.
        /// </summary>
        [JsonProperty("html_attributions")]
        public List<string> HtmlAttributes { get; } = new List<string>();

        /// <summary>
        /// Gets a list of the results for the Google request.
        /// </summary>
        [JsonProperty("results")]
        public IEnumerable<TResponseType> Results { get; } = new List<TResponseType>();

        /// <summary>
        /// Gets or sets the status of the Google Geocoding API call.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
