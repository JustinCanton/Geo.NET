// <copyright file="SpatialReference.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The spatial reference information.
    /// </summary>
    public class SpatialReference
    {
        /// <summary>
        /// Gets or sets the well-known id for the spatial reference.
        /// </summary>
        [JsonProperty("wkid")]
        public int WellKnownID { get; set; }

        /// <summary>
        /// Gets or sets the latest well-known id for the spatial reference.
        /// </summary>
        [JsonProperty("latestWkid")]
        public int LatestWellKnownID { get; set; }
    }
}
