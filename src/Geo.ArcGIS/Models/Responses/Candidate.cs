// <copyright file="Candidate.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// A potential candidate from ArcGIS.
    /// </summary>
    public class Candidate : LocationInformation
    {
        /// <summary>
        /// Gets or sets a rectangular bounding box around the location lat/lng coordinates.
        /// </summary>
        [JsonProperty("extent")]
        public BoundingBox Extent { get; set; }
    }
}
