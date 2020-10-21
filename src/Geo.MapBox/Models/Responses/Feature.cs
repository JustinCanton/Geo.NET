// <copyright file="Feature.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.MapBox.Models.Responses
{
    using System.Collections.Generic;
    using Geo.MapBox.Converters;
    using Newtonsoft.Json;

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
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the GeoJSON type of the result.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets the feature types describing the feature.
        /// Options are country, region, postcode, district, place, locality, neighborhood, address, and poi.
        /// Most features have only one type, but if the feature has multiple types, all applicable types will be listed in the array.
        /// (For example, Vatican City is a country, region, and place.)
        /// </summary>
        [JsonProperty("place_type")]
        public List<string> PlaceType { get; } = new List<string>();

        /// <summary>
        /// gets or sets a value that indicates how well the returned feature matches the user's query on a scale from 0 to 1.
        /// 0 means the result does not match the query text at all, while 1 means the result fully matches the query text.
        /// You can use the relevance property to remove results that don’t fully match the query.
        /// </summary>
        [JsonProperty("relevance")]
        public float Relevance { get; set; }

        /// <summary>
        /// Gets or sets the house number for the returned address feature.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the properties to describe a feature.
        /// </summary>
        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        /// <summary>
        /// Gets the information about the place in different languages.
        /// </summary>
        public List<PlaceInformation> PlaceInformation { get; } = new List<PlaceInformation>();

        /// <summary>
        /// Gets or sets a string analogous to the text field that more closely matches the query than results in the specified language.
        /// For example, querying Köln, Germany with language set to English (en) might return a feature with the text Cologne and the matching_text Köln.
        /// </summary>
        [JsonProperty("matching_text")]
        public string MatchingText { get; set; }

        /// <summary>
        /// Gets or sets a string analogous to the place_name field that more closely matches the query than results in the specified language.
        /// For example, querying Köln, Germany with language set to English (en) might return a feature with the place_name Cologne, Germany
        /// and a matching_place_name of Köln, North Rhine-Westphalia, Germany.
        /// </summary>
        [JsonProperty("matching_place_name")]
        public string MatchingPlaceName { get; set; }

        /// <summary>
        /// Gets or sets the bounding box for the feature.
        /// </summary>
        [JsonProperty("bbox")]
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets the coordinates of the feature’s center.
        /// This may be the literal centroid of the feature’s geometry, or the center of human activity within the feature
        /// (for example, the downtown area of a city).
        /// </summary>
        [JsonProperty("center")]
        public Coordinate Center { get; set; }

        /// <summary>
        /// Gets or sets the spatial geometry of the returned feature.
        /// </summary>
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        /// <summary>
        /// Gets the hierarchy of encompassing parent features.
        /// Each parent feature may include any of the above properties.
        /// </summary>
        [JsonProperty("context")]
        public List<Context> Contexts { get; } = new List<Context>();
    }
}
