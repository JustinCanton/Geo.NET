// <copyright file="NominatimPlace.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Models.Responses
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents a place returned by Nominatim API.
    /// </summary>
    public class NominatimPlace
    {
        /// <summary>
        /// Gets or sets the unique identifier for the place.
        /// </summary>
        [JsonPropertyName("place_id")]
        public long PlaceId { get; set; }

        /// <summary>
        /// Gets or sets the licence information for the data.
        /// </summary>
        [JsonPropertyName("licence")]
        public string Licence { get; set; }

        /// <summary>
        /// Gets or sets the type of OSM object.
        /// One of node, way, relation.
        /// </summary>
        [JsonPropertyName("osm_type")]
        public string OsmType { get; set; }

        /// <summary>
        /// Gets or sets the OSM ID of the object.
        /// </summary>
        [JsonPropertyName("osm_id")]
        public long OsmId { get; set; }

        /// <summary>
        /// Gets or sets the latitude coordinate.
        /// </summary>
        [JsonPropertyName("lat")]
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate.
        /// </summary>
        [JsonPropertyName("lon")]
        public string Longitude { get; set; }

        /// <summary>
        /// Gets or sets the category of the place (e.g., amenity, highway, etc.).
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the type of the place within the category (e.g., restaurant, primary, etc.).
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the rank of the place for search purposes.
        /// </summary>
        [JsonPropertyName("place_rank")]
        public int PlaceRank { get; set; }

        /// <summary>
        /// Gets or sets the importance score of the place.
        /// </summary>
        [JsonPropertyName("importance")]
        public double? Importance { get; set; }

        /// <summary>
        /// Gets or sets the rank of the address for the place.
        /// </summary>
        [JsonPropertyName("addressrank")]
        public int AddressRank { get; set; }

        /// <summary>
        /// Gets or sets the display name of the place.
        /// </summary>
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the name of the place.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address breakdown when address details are requested.
        /// </summary>
        [JsonPropertyName("address")]
        public NominatimAddress Address { get; set; }

        /// <summary>
        /// Gets or sets the bounding box of the place.
        /// Array format: [south, north, west, east].
        /// </summary>
        [JsonPropertyName("boundingbox")]
        public string[] BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets extra information about the place when requested.
        /// </summary>
        [JsonPropertyName("extratags")]
        public Dictionary<string, string> ExtraTags { get; set; }

        /// <summary>
        /// Gets or sets alternative names for the place when requested.
        /// </summary>
        [JsonPropertyName("namedetails")]
        public Dictionary<string, string> NameDetails { get; set; }

        /// <summary>
        /// Gets or sets the geometry of the place in various formats.
        /// </summary>
        [JsonPropertyName("geojson")]
        public object GeoJson { get; set; }

        /// <summary>
        /// Gets or sets the geometry as KML when requested.
        /// </summary>
        [JsonPropertyName("geokml")]
        public string GeoKml { get; set; }

        /// <summary>
        /// Gets or sets the geometry as SVG when requested.
        /// </summary>
        [JsonPropertyName("svg")]
        public string Svg { get; set; }

        /// <summary>
        /// Gets or sets the geometry as text when requested.
        /// </summary>
        [JsonPropertyName("geotext")]
        public string GeoText { get; set; }
    }
}