// <copyright file="BoundingBoxModule.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Bounding box coordinates for a location.
    /// </summary>
    public class BoundingBoxModule
    {
        /// <summary>
        /// Gets or sets the minimum longitude coordinate point associated with the location result.
        /// </summary>
        [JsonPropertyName("min_longitude")]
        public double? MinimumLongitude { get; set; }

        /// <summary>
        /// Gets or sets the minimum latitude coordinate point associated with the location result.
        /// </summary>
        [JsonPropertyName("min_latitude")]
        public double? MinimumLatitude { get; set; }

        /// <summary>
        /// Gets or sets the maximum longitude coordinate point associated with the location result.
        /// </summary>
        [JsonPropertyName("max_longitude")]
        public double? MaximumLongitude { get; set; }

        /// <summary>
        /// Gets or sets the maximum latitude coordinate point associated with the location result.
        /// </summary>
        [JsonPropertyName("max_latitude")]
        public double? MaximumLatitude { get; set; }
    }
}
