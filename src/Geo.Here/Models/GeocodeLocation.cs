// <copyright file="GeocodeLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Geocoding location matches.
    /// </summary>
    public class GeocodeLocation : Location
    {
        /// <summary>
        /// Gets or sets the score for this location.
        /// </summary>
        [JsonProperty("scoring")]
        public Score Score { get; set; }
    }
}
