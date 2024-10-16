// <copyright file="LocationAttribute.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System;
    using System.Text.Json.Serialization;

    /// <summary>
    /// The location attributes.
    /// </summary>
    public class LocationAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the id of the result.
        /// </summary>
        [JsonPropertyName("ResultID")]
        public int ResultId { get; set; }

        /// <summary>
        /// Gets or sets the match address.
        /// </summary>
        [JsonPropertyName("Match_addr")]
        public string MatchAddress { get; set; }

        /// <summary>
        /// Gets or sets the long label string for the address.
        /// </summary>
        [JsonPropertyName("LongLabel")]
        public string LongLabel { get; set; }

        /// <summary>
        /// Gets or sets the short label string for the address.
        /// </summary>
        [JsonPropertyName("ShortLabel")]
        public string ShortLabel { get; set; }

        /// <summary>
        /// Gets or sets the address type.
        /// </summary>
        [JsonPropertyName("Addr_type")]
        public string AddressType { get; set; }

        /// <summary>
        /// Gets or sets the type of address.
        /// </summary>
        [JsonPropertyName("Type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the place name if the address has a name.
        /// </summary>
        [JsonPropertyName("PlaceName")]
        public string PlaceName { get; set; }

        /// <summary>
        /// Gets or sets the block name.
        /// </summary>
        [JsonPropertyName("Block")]
        public string Block { get; set; }

        /// <summary>
        /// Gets or sets the sector of the address.
        /// </summary>
        [JsonPropertyName("Sector")]
        public string Sector { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood information for the address.
        /// </summary>
        [JsonPropertyName("Neighborhood")]
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the district of the address.
        /// </summary>
        [JsonPropertyName("District")]
        public string District { get; set; }

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        [JsonPropertyName("City")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the metropolitian area of the address.
        /// </summary>
        [JsonPropertyName("MetroArea")]
        public string MetropolitanArea { get; set; }

        /// <summary>
        /// Gets or sets the sub-region of the address.
        /// </summary>
        [JsonPropertyName("Subregion")]
        public string Subregion { get; set; }

        /// <summary>
        /// Gets or sets the region of the address.
        /// </summary>
        [JsonPropertyName("Region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the territory of the address.
        /// </summary>
        [JsonPropertyName("Territory")]
        public string Territory { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the address.
        /// </summary>
        [JsonPropertyName("Postal")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the postal code extension of the address.
        /// </summary>
        [JsonPropertyName("PostalExt")]
        public string PostalCodeExtension { get; set; }

        /// <summary>
        /// Gets or sets the country code of the address.
        /// </summary>
        [JsonPropertyName("CountryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Sets the country code by an alternate name.
        /// </summary>
        [JsonPropertyName("Country")]
        public string AlternateCountryCode
        {
            set { CountryCode = value; }
        }

        /// <summary>
        /// Gets or sets the location name.
        /// </summary>
        [JsonPropertyName("Loc_name")]
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the status of the result.
        /// </summary>
        [JsonPropertyName("Status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the score of the result.
        /// </summary>
        [JsonPropertyName("Score")]
        public double Score { get; set; }

        /// <summary>
        /// Gets or sets the place address.
        /// </summary>
        [JsonPropertyName("Place_addr")]
        public string PlaceAddress { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the location.
        /// </summary>
        [JsonPropertyName("Phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the url of the location.
        /// </summary>
        [JsonPropertyName("URL")]
        public Uri URL { get; set; }

        /// <summary>
        /// Gets or sets the rank of the location.
        /// </summary>
        [JsonPropertyName("Rank")]
        public float Rank { get; set; }

        /// <summary>
        /// Gets or sets the additional building information for a location.
        /// </summary>
        [JsonPropertyName("AddBldg")]
        public string AddBuilding { get; set; }

        /// <summary>
        /// Gets or sets the additional number information for a building.
        /// </summary>
        [JsonPropertyName("AddNum")]
        public string AdditionalNumber { get; set; }

        /// <summary>
        /// Gets or sets the additional number from information for a location.
        /// </summary>
        [JsonPropertyName("AddNumFrom")]
        public string AdditionalNumberFrom { get; set; }

        /// <summary>
        /// Gets or sets the additional number to information for a location.
        /// </summary>
        [JsonPropertyName("AddNumTo")]
        public string AdditionalNumberTo { get; set; }

        /// <summary>
        /// Gets or sets the additional range for a location.
        /// </summary>
        [JsonPropertyName("AddRange")]
        public string AddRange { get; set; }

        /// <summary>
        /// Gets or sets the side information for a location.
        /// </summary>
        [JsonPropertyName("Side")]
        public string Side { get; set; }

        /// <summary>
        /// Gets or sets the street pre-direction for a location.
        /// </summary>
        [JsonPropertyName("StPreDir")]
        public string StreetPreDirection { get; set; }

        /// <summary>
        /// Gets or sets the street pre-type for a location.
        /// </summary>
        [JsonPropertyName("StPreType")]
        public string StreetPreType { get; set; }

        /// <summary>
        /// Gets or sets the street name a location is on.
        /// </summary>
        [JsonPropertyName("StName")]
        public string StreetName { get; set; }

        /// <summary>
        /// Gets or sets the street type a location is on.
        /// </summary>
        [JsonPropertyName("StType")]
        public string StreetType { get; set; }

        /// <summary>
        /// Gets or sets the street direction a location is on.
        /// </summary>
        [JsonPropertyName("StDir")]
        public string StreetDirection { get; set; }

        /// <summary>
        /// Gets or sets the building type of a location.
        /// </summary>
        [JsonPropertyName("BldgType")]
        public string BuildingType { get; set; }

        /// <summary>
        /// Gets or sets the building name of a location.
        /// </summary>
        [JsonPropertyName("BldgName")]
        public string BuildingName { get; set; }

        /// <summary>
        /// Gets or sets the level type of a location.
        /// </summary>
        [JsonPropertyName("LevelType")]
        public string LevelType { get; set; }

        /// <summary>
        /// Gets or sets the level name of a location.
        /// </summary>
        [JsonPropertyName("LevelName")]
        public string LevelName { get; set; }

        /// <summary>
        /// Gets or sets the unit type of a location.
        /// </summary>
        [JsonPropertyName("UnitType")]
        public string UnitType { get; set; }

        /// <summary>
        /// Gets or sets the unit name of a location.
        /// </summary>
        [JsonPropertyName("UnitName")]
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets the sub-address of a location.
        /// </summary>
        [JsonPropertyName("SubAddr")]
        public string SubAddress { get; set; }

        /// <summary>
        /// Gets or sets the street address of a location.
        /// </summary>
        [JsonPropertyName("StAddr")]
        public string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood of a location.
        /// </summary>
        [JsonPropertyName("Nbrhd")]
        public string LocationNeighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the region abbreviation of a location.
        /// </summary>
        [JsonPropertyName("RegionAbbr")]
        public string RegionAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the zone of a location.
        /// </summary>
        [JsonPropertyName("Zone")]
        public string Zone { get; set; }

        /// <summary>
        /// Gets or sets the country of a location.
        /// </summary>
        [JsonPropertyName("CntryName")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the language code of a location.
        /// </summary>
        [JsonPropertyName("LangCode")]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the distance to a location.
        /// </summary>
        [JsonPropertyName("Distance")]
        public double Distance { get; set; }

        /// <summary>
        /// Gets or sets the longitude of a location.
        /// </summary>
        [JsonPropertyName("X")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude of a location.
        /// </summary>
        [JsonPropertyName("Y")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the display longitude of a location.
        /// </summary>
        [JsonPropertyName("DisplayX")]
        public double DisplayLongitude { get; set; }

        /// <summary>
        /// Gets or sets the display latitude of a location.
        /// </summary>
        [JsonPropertyName("DisplayY")]
        public double DisplayLatitude { get; set; }

        /// <summary>
        /// Gets or sets the west-most longitude of a location.
        /// </summary>
        [JsonPropertyName("Xmin")]
        public double WestLongitude { get; set; }

        /// <summary>
        /// Gets or sets the east-most longitude of a location.
        /// </summary>
        [JsonPropertyName("Xmax")]
        public double EastLongitude { get; set; }

        /// <summary>
        /// Gets or sets the south-most latitude of a location.
        /// </summary>
        [JsonPropertyName("Ymin")]
        public double SouthLatitude { get; set; }

        /// <summary>
        /// Gets or sets the north-most latitude of a location.
        /// </summary>
        [JsonPropertyName("Ymax")]
        public double NorthLatitude { get; set; }

        /// <summary>
        /// Gets or sets any extra information about a location.
        /// </summary>
        [JsonPropertyName("ExInfo")]
        public string ExtraInformation { get; set; }
    }
}
