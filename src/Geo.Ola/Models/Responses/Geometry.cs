// <copyright file="Geometry.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Ola.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The geometry information for the address.
    /// </summary>
    public class Geometry
    {
        public Viewport Viewport { get; set; }

        public Coordinate Location { get; set; }

        [JsonPropertyName("location_type")]
        public string LocationType { get; set; }
    }
}
