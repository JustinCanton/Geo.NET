// <copyright file="GeocodeCoords.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The coordinate portion of a Trimble Maps geocoding response.
    /// </summary>
    public class GeocodeCoords
    {
        /// <summary>
        /// Gets or sets the latitude as a string.
        /// </summary>
        [JsonPropertyName("Lat")]
        public string Lat { get; set; }

        /// <summary>
        /// Gets or sets the longitude as a string.
        /// </summary>
        [JsonPropertyName("Lon")]
        public string Lon { get; set; }
    }
}
