// <copyright file="BrowseLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Browse location matches.
    /// </summary>
    public class BrowseLocation : LookupLocation
    {
        /// <summary>
        /// Gets or sets the distance from the search center to this result item in meters. For example: "172039".
        /// </summary>
        [JsonProperty("distance")]
        public long Distance { get; set; }
    }
}
