// <copyright file="PlaceResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The Google place response object.
    /// </summary>
    public class PlaceResponse
    {
        /// <summary>
        /// Gets or sets a list of html attributions about this listing which must be displayed to the user.
        /// </summary>
        [JsonPropertyName("html_attributions")]
        public IList<string> HtmlAttributes { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets a list of the results for the Google request.
        /// </summary>
        [JsonPropertyName("results")]
        public IEnumerable<Place<OpeningHours>> Results { get; set; } = new List<Place<OpeningHours>>();

        /// <summary>
        /// Gets or sets the status of the Google Geocoding API call.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
