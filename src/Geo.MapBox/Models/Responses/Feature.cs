// <copyright file="Feature.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.MapBox.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using Geo.MapBox.Converters;

    /// <summary>
    /// The features of the location.
    /// </summary>
    [JsonConverter(typeof(FeatureConverter))]
    public class Feature
    {
        /// <summary>
        /// Gets or sets a feature ID in the format {type}.{id} where {type} is the lowest hierarchy feature in the place_type field.
        /// The {id} suffix of the feature ID is unstable and may change within versions.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the GeoJSON type of the result.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets the feature types describing the feature.
        /// Options are country, region, postcode, district, place, locality, neighbourhood, address, and poi.
        /// Most features have only one type, but if the feature has multiple types, all applicable types will be listed in the array.
        /// (For example, Vatican City is a country, region, and place.)
        /// </summary>
        [JsonPropertyName("place_type")]
        public IList<string> PlaceType { get; } = new List<string>();

        /// <summary>
        /// gets or sets a value that indicates how well the returned feature matches the user's query on a scale from 0 to 1.
        /// 0 means the result does not match the query text at all, while 1 means the result fully matches the query text.
        /// You can use the relevance property to remove results that don’t fully match the query.
        /// </summary>
        [JsonPropertyName("relevance")]
        public float Relevance { get; set; }

        /// <summary>
        /// Gets or sets the house number for the returned address feature.
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the properties to describe a feature.
        /// </summary>
        [JsonPropertyName("properties")]
        public Properties Properties { get; set; }

        /// <summary>
        /// Gets the information about the place in different languages.
        /// </summary>
        public IList<PlaceInformation> PlaceInformation { get; } = new List<PlaceInformation>();

        /// <summary>
        /// Gets or sets a string analogous to the text field that more closely matches the query than results in the specified language.
        /// For example, querying Köln, Germany with language set to English (en) might return a feature with the text Cologne and the matching_text Köln.
        /// </summary>
        [JsonPropertyName("matching_text")]
        public string MatchingText { get; set; }

        /// <summary>
        /// Gets or sets a string analogous to the place_name field that more closely matches the query than results in the specified language.
        /// For example, querying Köln, Germany with language set to English (en) might return a feature with the place_name Cologne, Germany
        /// and a matching_place_name of Köln, North Rhine-Westphalia, Germany.
        /// </summary>
        [JsonPropertyName("matching_place_name")]
        public string MatchingPlaceName { get; set; }

        /// <summary>
        /// Gets or sets the bounding box for the feature.
        /// </summary>
        [JsonPropertyName("bbox")]
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the feature’s center.
        /// This may be the literal centroid of the feature’s geometry, or the center of human activity within the feature
        /// (for example, the downtown area of a city).
        /// </summary>
        [JsonPropertyName("center")]
        public Coordinate Center { get; set; }

        /// <summary>
        /// Gets or sets the spatial geometry of the returned feature.
        /// </summary>
        [JsonPropertyName("geometry")]
        public Geometry Geometry { get; set; }

        /// <summary>
        /// Gets the hierarchy of encompassing parent features.
        /// Each parent feature may include any of the above properties.
        /// </summary>
        [JsonPropertyName("context")]
        public IList<Context> Contexts { get; } = new List<Context>();
    }
}
