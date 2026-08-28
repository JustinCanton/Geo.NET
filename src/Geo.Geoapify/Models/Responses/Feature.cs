// <copyright file="Feature.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// A single GeoJSON Feature representing a Geoapify geocoding result.
    /// </summary>
    public class Feature
    {
        /// <summary>
        /// Gets or sets the GeoJSON object type (always "Feature").
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the geometry of the feature.
        /// </summary>
        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        /// <summary>
        /// Gets or sets the properties of the feature containing the address and its metadata.
        /// </summary>
        [JsonPropertyName("properties")]
        public FeatureProperties Properties { get; set; }

        /// <summary>
        /// Gets or sets the bounding box of the feature as [lon1, lat1, lon2, lat2].
        /// </summary>
        [JsonPropertyName("bbox")]
        public IList<double> BoundingBox { get; set; }
    }
}
