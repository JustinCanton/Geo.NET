// <copyright file="GeocodeProvidedLocation.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapQuest.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The location information provided to the geocode request.
    /// </summary>
    public class GeocodeProvidedLocation
    {
        /// <summary>
        /// Gets or sets the location provided.
        /// </summary>
        [JsonPropertyName("location")]
        public string Location { get; set; }
    }
}
