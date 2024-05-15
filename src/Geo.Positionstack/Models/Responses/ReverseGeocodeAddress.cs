// <copyright file="ReverseGeocodeAddress.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Address information for a reverse geocode result.
    /// </summary>
    public class ReverseGeocodeAddress : Address
    {
        /// <summary>
        /// Gets or sets the distance of the result from the point.
        /// </summary>
        [JsonPropertyName("distance")]
        public int Distance { get; set; }
    }
}
