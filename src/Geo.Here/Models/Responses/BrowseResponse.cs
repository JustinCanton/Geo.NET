// <copyright file="BrowseResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
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
        /// Gets the list of locations that match the browse request.
        /// </summary>
        [JsonProperty("items")]
        public IList<BrowseLocation> Items { get; } = new List<BrowseLocation>();
    }
}
