// <copyright file="BrowseResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The response from a browse request.
    /// </summary>
    public class BrowseResponse
    {
        /// <summary>
        /// Gets or sets the list of locations that match the discover request.
        /// </summary>
        [JsonProperty("items")]
        public List<BrowseLocation> Items { get; set; }
    }
}
