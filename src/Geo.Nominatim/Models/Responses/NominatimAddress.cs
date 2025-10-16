// <copyright file="NominatimAddress.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Nominatim.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the structured address information returned by Nominatim.
    /// </summary>
    public class NominatimAddress
    {
        /// <summary>
        /// Gets or sets the house number.
        /// </summary>
        [JsonPropertyName("house_number")]
        public string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the road/street name.
        /// </summary>
        [JsonPropertyName("road")]
        public string Road { get; set; }

        /// <summary>
        /// Gets or sets the suburb.
        /// </summary>
        [JsonPropertyName("suburb")]
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the village.
        /// </summary>
        [JsonPropertyName("village")]
        public string Village { get; set; }

        /// <summary>
        /// Gets or sets the town.
        /// </summary>
        [JsonPropertyName("town")]
        public string Town { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the city district.
        /// </summary>
        [JsonPropertyName("city_district")]
        public string CityDistrict { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        [JsonPropertyName("county")]
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the state district.
        /// </summary>
        [JsonPropertyName("state_district")]
        public string StateDistrict { get; set; }

        /// <summary>
        /// Gets or sets the postcode.
        /// </summary>
        [JsonPropertyName("postcode")]
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the country code (ISO 3166-1 alpha-2).
        /// </summary>
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the continent.
        /// </summary>
        [JsonPropertyName("continent")]
        public string Continent { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        [JsonPropertyName("region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood.
        /// </summary>
        [JsonPropertyName("neighbourhood")]
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the quarter.
        /// </summary>
        [JsonPropertyName("quarter")]
        public string Quarter { get; set; }

        /// <summary>
        /// Gets or sets the amenity.
        /// </summary>
        [JsonPropertyName("amenity")]
        public string Amenity { get; set; }

        /// <summary>
        /// Gets or sets the leisure facility.
        /// </summary>
        [JsonPropertyName("leisure")]
        public string Leisure { get; set; }

        /// <summary>
        /// Gets or sets the tourism facility.
        /// </summary>
        [JsonPropertyName("tourism")]
        public string Tourism { get; set; }

        /// <summary>
        /// Gets or sets the shop.
        /// </summary>
        [JsonPropertyName("shop")]
        public string Shop { get; set; }

        /// <summary>
        /// Gets or sets the industrial facility.
        /// </summary>
        [JsonPropertyName("industrial")]
        public string Industrial { get; set; }

        /// <summary>
        /// Gets or sets the residential area.
        /// </summary>
        [JsonPropertyName("residential")]
        public string Residential { get; set; }

        /// <summary>
        /// Gets or sets the hamlet.
        /// </summary>
        [JsonPropertyName("hamlet")]
        public string Hamlet { get; set; }

        /// <summary>
        /// Gets or sets the croft.
        /// </summary>
        [JsonPropertyName("croft")]
        public string Croft { get; set; }

        /// <summary>
        /// Gets or sets the isolated dwelling.
        /// </summary>
        [JsonPropertyName("isolated_dwelling")]
        public string IsolatedDwelling { get; set; }

        /// <summary>
        /// Gets or sets the municipality.
        /// </summary>
        [JsonPropertyName("municipality")]
        public string Municipality { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        [JsonPropertyName("province")]
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the island.
        /// </summary>
        [JsonPropertyName("island")]
        public string Island { get; set; }

        /// <summary>
        /// Gets or sets the islet.
        /// </summary>
        [JsonPropertyName("islet")]
        public string Islet { get; set; }

        /// <summary>
        /// Gets or sets the archipelago.
        /// </summary>
        [JsonPropertyName("archipelago")]
        public string Archipelago { get; set; }

        /// <summary>
        /// Gets or sets the building name.
        /// </summary>
        [JsonPropertyName("building")]
        public string Building { get; set; }

        /// <summary>
        /// Gets or sets the man-made structure.
        /// </summary>
        [JsonPropertyName("man_made")]
        public string ManMade { get; set; }

        /// <summary>
        /// Gets or sets the public building.
        /// </summary>
        [JsonPropertyName("public_building")]
        public string PublicBuilding { get; set; }

        /// <summary>
        /// Gets or sets the emergency facility.
        /// </summary>
        [JsonPropertyName("emergency")]
        public string Emergency { get; set; }

        /// <summary>
        /// Gets or sets the historic site.
        /// </summary>
        [JsonPropertyName("historic")]
        public string Historic { get; set; }

        /// <summary>
        /// Gets or sets the military facility.
        /// </summary>
        [JsonPropertyName("military")]
        public string Military { get; set; }

        /// <summary>
        /// Gets or sets the natural feature.
        /// </summary>
        [JsonPropertyName("natural")]
        public string Natural { get; set; }

        /// <summary>
        /// Gets or sets the landuse.
        /// </summary>
        [JsonPropertyName("landuse")]
        public string Landuse { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        [JsonPropertyName("place")]
        public string Place { get; set; }

        /// <summary>
        /// Gets or sets the railway.
        /// </summary>
        [JsonPropertyName("railway")]
        public string Railway { get; set; }

        /// <summary>
        /// Gets or sets the waterway.
        /// </summary>
        [JsonPropertyName("waterway")]
        public string Waterway { get; set; }
    }
}