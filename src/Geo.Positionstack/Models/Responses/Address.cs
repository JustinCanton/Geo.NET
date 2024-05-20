// <copyright file="Address.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Positionstack.Models.Responses
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The address information for the location.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the latitude coordinate associated with the location result.
        /// </summary>
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude coordinate associated with the location result.
        /// </summary>
        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the formatted place name or address.
        /// </summary>
        [JsonPropertyName("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the name of the location result. This could be a place name, address, postal code, and more.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of location result.
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the distance (in meters) between the location result and the coordinates specified in the query parameter. Only applicable for reverse geocoding.
        /// </summary>
        [JsonPropertyName("distance")]
        public uint? Distance { get; set; }

        /// <summary>
        /// Gets or sets the street or house number associated with the location result.
        /// </summary>
        [JsonPropertyName("number")]
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets the street name associated with the location result.
        /// </summary>
        [JsonPropertyName("street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the postal or ZIP code associated with the location result.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets a confidence score between 0 (0% confidence) and 1 (100% confidence) associated with the location result.
        /// </summary>
        [JsonPropertyName("confidence")]
        public double? Confidence { get; set; }

        /// <summary>
        /// Gets or sets the name of the region associated with the location result.
        /// </summary>
        [JsonPropertyName("region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the region code associated with the location result.
        /// </summary>
        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        /// <summary>
        /// Gets or sets the name of the administrative area associated with the location result.
        /// </summary>
        [JsonPropertyName("administrative_area")]
        public string AdministrativeArea { get; set; }

        /// <summary>
        /// Gets or sets the name of the neighbourhood associated with the location result.
        /// </summary>
        [JsonPropertyName("neighborhood")]
        public string Neighborhood { get; set; }

        /// <summary>
        /// Gets or sets the localised country name. For example: "United States".
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the ISO 3166 Alpha 2 (two letters) code of the country associated with the location result.
        /// </summary>
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets an embeddable map associated with the location result.
        /// </summary>
        [JsonPropertyName("map_url")]
        public string MapUrl { get; set; }

        /// <summary>
        /// Gets or sets an extensive set of country information.
        /// </summary>
        [JsonPropertyName("country_module")]
        public CountryModule CountryModule { get; set; }

        /// <summary>
        /// Gets or sets astrology data for a location.
        /// </summary>
        [JsonPropertyName("sun_module")]
        public SunModule SunModule { get; set; }

        /// <summary>
        /// Gets or sets timezone information for a location.
        /// </summary>
        [JsonPropertyName("timezone_module")]
        public TimezoneModule TimezoneModule { get; set; }

        /// <summary>
        /// Gets or sets bounding box coordinates for a location.
        /// </summary>
        [JsonPropertyName("bbox_module")]
        public BoundingBoxModule BoundingBoxModule { get; set; }
    }
}
