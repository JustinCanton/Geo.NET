// <copyright file="FeatureProperties.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Models.Responses
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The properties of a GeoJSON feature returned by the ORS geocoding API.
    /// </summary>
    public class FeatureProperties
    {
        /// <summary>Gets or sets the feature identifier.</summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>Gets or sets the Pelias global identifier.</summary>
        [JsonPropertyName("gid")]
        public string Gid { get; set; }

        /// <summary>Gets or sets the layer type (e.g. venue, address, locality).</summary>
        [JsonPropertyName("layer")]
        public string Layer { get; set; }

        /// <summary>Gets or sets the data source (e.g. osm, oa, gn, wof).</summary>
        [JsonPropertyName("source")]
        public string Source { get; set; }

        /// <summary>Gets or sets the source-specific identifier.</summary>
        [JsonPropertyName("source_id")]
        public string SourceId { get; set; }

        /// <summary>Gets or sets the primary name of the place.</summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>Gets or sets the house number.</summary>
        [JsonPropertyName("housenumber")]
        public string HouseNumber { get; set; }

        /// <summary>Gets or sets the street name.</summary>
        [JsonPropertyName("street")]
        public string Street { get; set; }

        /// <summary>Gets or sets the postal code.</summary>
        [JsonPropertyName("postalcode")]
        public string PostalCode { get; set; }

        /// <summary>Gets or sets the confidence score (0.0–1.0) of the match.</summary>
        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }

        /// <summary>Gets or sets the distance from the focus point or query point in kilometers.</summary>
        [JsonPropertyName("distance")]
        public double? Distance { get; set; }

        /// <summary>Gets or sets the accuracy level of the result (e.g. point, centroid).</summary>
        [JsonPropertyName("accuracy")]
        public string Accuracy { get; set; }

        /// <summary>Gets or sets the type of match made (e.g. exact, fallback).</summary>
        [JsonPropertyName("match_type")]
        public string MatchType { get; set; }

        /// <summary>Gets or sets the fully formatted address label.</summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }

        /// <summary>Gets or sets the country name.</summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>Gets or sets the Pelias GID of the country.</summary>
        [JsonPropertyName("country_gid")]
        public string CountryGid { get; set; }

        /// <summary>Gets or sets the ISO 3166-1 alpha-2 country code.</summary>
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        /// <summary>Gets or sets the macro region name.</summary>
        [JsonPropertyName("macroregion")]
        public string MacroRegion { get; set; }

        /// <summary>Gets or sets the Pelias GID of the macro region.</summary>
        [JsonPropertyName("macroregion_gid")]
        public string MacroRegionGid { get; set; }

        /// <summary>Gets or sets the region (state or province) name.</summary>
        [JsonPropertyName("region")]
        public string Region { get; set; }

        /// <summary>Gets or sets the Pelias GID of the region.</summary>
        [JsonPropertyName("region_gid")]
        public string RegionGid { get; set; }

        /// <summary>Gets or sets the macro county name.</summary>
        [JsonPropertyName("macrocounty")]
        public string MacroCounty { get; set; }

        /// <summary>Gets or sets the Pelias GID of the macro county.</summary>
        [JsonPropertyName("macrocounty_gid")]
        public string MacroCountyGid { get; set; }

        /// <summary>Gets or sets the county name.</summary>
        [JsonPropertyName("county")]
        public string County { get; set; }

        /// <summary>Gets or sets the Pelias GID of the county.</summary>
        [JsonPropertyName("county_gid")]
        public string CountyGid { get; set; }

        /// <summary>Gets or sets the local administrative area name.</summary>
        [JsonPropertyName("localadmin")]
        public string LocalAdmin { get; set; }

        /// <summary>Gets or sets the Pelias GID of the local administrative area.</summary>
        [JsonPropertyName("localadmin_gid")]
        public string LocalAdminGid { get; set; }

        /// <summary>Gets or sets the locality (city or town) name.</summary>
        [JsonPropertyName("locality")]
        public string Locality { get; set; }

        /// <summary>Gets or sets the Pelias GID of the locality.</summary>
        [JsonPropertyName("locality_gid")]
        public string LocalityGid { get; set; }

        /// <summary>Gets or sets the borough name.</summary>
        [JsonPropertyName("borough")]
        public string Borough { get; set; }

        /// <summary>Gets or sets the Pelias GID of the borough.</summary>
        [JsonPropertyName("borough_gid")]
        public string BoroughGid { get; set; }

        /// <summary>Gets or sets the neighbourhood name.</summary>
        [JsonPropertyName("neighbourhood")]
        public string Neighbourhood { get; set; }

        /// <summary>Gets or sets the Pelias GID of the neighbourhood.</summary>
        [JsonPropertyName("neighbourhood_gid")]
        public string NeighbourhoodGid { get; set; }

        /// <summary>Gets or sets provider-specific addendum data as a raw JSON element.</summary>
        [JsonPropertyName("addendum")]
        public JsonElement? Addendum { get; set; }
    }
}
