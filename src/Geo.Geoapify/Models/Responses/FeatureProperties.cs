// <copyright file="FeatureProperties.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The properties of a Geoapify geocoding result.
    /// <para>
    /// Depending on the type of the returned location, some of the fields may be missing.
    /// </para>
    /// </summary>
    public class FeatureProperties
    {
        /// <summary>
        /// Gets or sets the information about the data source of the result.
        /// </summary>
        [JsonPropertyName("datasource")]
        public DataSource DataSource { get; set; }

        /// <summary>
        /// Gets or sets the name of the place or amenity.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the country name.
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the ISO 3166-1 alpha-2 country code.
        /// </summary>
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the state, province, or equivalent first level administrative area.
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the state code.
        /// </summary>
        [JsonPropertyName("state_code")]
        public string StateCode { get; set; }

        /// <summary>
        /// Gets or sets the ISO 3166-2 code of the administrative area.
        /// </summary>
        [JsonPropertyName("iso3166_2")]
        public string Iso31662 { get; set; }

        /// <summary>
        /// Gets or sets the county.
        /// </summary>
        [JsonPropertyName("county")]
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the county code.
        /// </summary>
        [JsonPropertyName("county_code")]
        public string CountyCode { get; set; }

        /// <summary>
        /// Gets or sets the city, town, or village.
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        [JsonPropertyName("postcode")]
        public string PostCode { get; set; }

        /// <summary>
        /// Gets or sets the district the address belongs to.
        /// </summary>
        [JsonPropertyName("district")]
        public string District { get; set; }

        /// <summary>
        /// Gets or sets the suburb the address belongs to.
        /// </summary>
        [JsonPropertyName("suburb")]
        public string Suburb { get; set; }

        /// <summary>
        /// Gets or sets the quarter the address belongs to.
        /// </summary>
        [JsonPropertyName("quarter")]
        public string Quarter { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood the address belongs to.
        /// </summary>
        [JsonPropertyName("neighbourhood")]
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        [JsonPropertyName("street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the house number.
        /// </summary>
        [JsonPropertyName("housenumber")]
        public string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the result in decimal degrees.
        /// </summary>
        [JsonPropertyName("lon")]
        public double? Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the result in decimal degrees.
        /// </summary>
        [JsonPropertyName("lat")]
        public double? Latitude { get; set; }

        /// <summary>
        /// Gets or sets the full address of the result formatted for display.
        /// </summary>
        [JsonPropertyName("formatted")]
        public string Formatted { get; set; }

        /// <summary>
        /// Gets or sets the first line of the formatted address.
        /// </summary>
        [JsonPropertyName("address_line1")]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the second line of the formatted address.
        /// </summary>
        [JsonPropertyName("address_line2")]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the category of the result, for example "commercial.supermarket".
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the type of the returned location, for example "building", "street", or "city".
        /// </summary>
        [JsonPropertyName("result_type")]
        public string ResultType { get; set; }

        /// <summary>
        /// Gets or sets the distance in metres from the requested location. Only returned for reverse geocoding
        /// and for requests biased by proximity.
        /// </summary>
        [JsonPropertyName("distance")]
        public double? Distance { get; set; }

        /// <summary>
        /// Gets or sets the ranking information describing how well the result matches the request.
        /// </summary>
        [JsonPropertyName("rank")]
        public Rank Rank { get; set; }

        /// <summary>
        /// Gets or sets the time zone information of the result.
        /// </summary>
        [JsonPropertyName("timezone")]
        public TimeZone TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the Open Location Code (plus code) of the result.
        /// </summary>
        [JsonPropertyName("plus_code")]
        public string PlusCode { get; set; }

        /// <summary>
        /// Gets or sets the shortened Open Location Code (plus code) of the result.
        /// </summary>
        [JsonPropertyName("plus_code_short")]
        public string PlusCodeShort { get; set; }

        /// <summary>
        /// Gets or sets the Geoapify identifier of the result, which can be used as a place filter.
        /// </summary>
        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; }

        /// <summary>
        /// Gets or sets the reference passed in with the request when performing a batch request.
        /// </summary>
        [JsonPropertyName("ref")]
        public string Reference { get; set; }
    }
}
