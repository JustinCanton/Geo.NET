// <copyright file="GeocodeAddress.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Address information for a geocode result.
    /// </summary>
    public class GeocodeAddress : Address
    {
        /// <summary>
        /// Gets or sets the confidence of the result.
        /// </summary>
        [JsonPropertyName("confidence")]
        public string Confidence { get; set; }
    }
}
