// <copyright file="GeocodeProvidedLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The location information provided to the geocode request.
    /// </summary>
    public class GeocodeProvidedLocation
    {
        /// <summary>
        /// Gets or sets the location provided.
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }
    }
}
