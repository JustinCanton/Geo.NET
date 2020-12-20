// <copyright file="AutocompleteResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Google.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The Google autocomplete response object.
    /// </summary>
    /// <typeparam name="TResponseType">The type of the response items.</typeparam>
    public class AutocompleteResponse<TResponseType>
    {
        /// <summary>
        /// Gets a list of the predictions for the Google request.
        /// </summary>
        [JsonProperty("predictions")]
        public IEnumerable<TResponseType> Predictions { get; } = new List<TResponseType>();

        /// <summary>
        /// Gets or sets the status of the Google Geocoding API call.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
