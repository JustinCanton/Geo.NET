// <copyright file="CandidateResponse.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A candidate response from ArcGIS.
    /// </summary>
    public class CandidateResponse
    {
        /// <summary>
        /// Gets or sets a spatial reference.
        /// A spatial reference helps describe where features are located in the real world.
        /// </summary>
        [JsonProperty("spatialReference")]
        public SpatialReference SpatialReference { get; set; }

        /// <summary>
        /// Gets or sets an array of possible matches for the place and location passed in.
        /// </summary>
        [JsonProperty("candidates")]
        public List<Candidate> Candidates { get; set; }
    }
}
