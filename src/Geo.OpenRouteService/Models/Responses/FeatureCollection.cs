// <copyright file="FeatureCollection.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A GeoJSON FeatureCollection returned by all ORS geocoding endpoints.
    /// </summary>
    public class FeatureCollection
    {
        /// <summary>
        /// Gets or sets the GeoJSON object type (always "FeatureCollection").
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the list of geocoding result features.
        /// </summary>
        [JsonPropertyName("features")]
        public IList<Feature> Features { get; set; } = new List<Feature>();

        /// <summary>
        /// Gets or sets the bounding box encompassing all features as [west, south, east, north].
        /// </summary>
        [JsonPropertyName("bbox")]
        public IList<double> BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets the geocoding metadata including version, attribution, and echoed query parameters.
        /// </summary>
        [JsonPropertyName("geocoding")]
        public GeocodingMetadata Geocoding { get; set; }
    }
}
