// <copyright file="LocationAttribute.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Responses
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The location attributes.
    /// </summary>
    public class LocationAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the id of the result.
        /// </summary>
        [JsonProperty("ResultID")]
        public int ResultId { get; set; }

        /// <summary>
        /// Gets or sets the match address.
        /// </summary>
        [JsonProperty("Match_addr")]
        public string MatchAddress { get; set; }

        /// <summary>
        /// Gets or sets the long label string for the address.
        /// </summary>
        [JsonProperty("LongLabel")]
        public string LongLabel { get; set; }

        /// <summary>
        /// Gets or sets the short label string for the address.
        /// </summary>
        [JsonProperty("ShortLabel")]
        public string ShortLabel { get; set; }

        /// <summary>
        /// Gets or sets the address type.
        /// </summary>
        [JsonProperty("Addr_type")]
        public string AddressType { get; set; }

        /// <summary>
        /// Gets or sets the type of address.
        /// </summary>
        [JsonProperty("Type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the place name if the address has a name.
        /// </summary>
        [JsonProperty("PlaceName")]
        public string PlaceName { get; set; }

        /// <summary>
        /// Gets or sets the block name.
        /// </summary>
        [JsonProperty("Block")]
        public string Block { get; set; }

        /// <summary>
        /// Gets or sets the sector of the address.
        /// </summary>
        [JsonProperty("Sector")]
        public string Sector { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood information for the address.
        /// </summary>
        [JsonProperty("Neighborhood")]
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the district of the address.
        /// </summary>
        [JsonProperty("District")]
        public string District { get; set; }

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        [JsonProperty("City")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the metropolitian area of the address.
        /// </summary>
        [JsonProperty("MetroArea")]
        public string MetropolitanArea { get; set; }

        /// <summary>
        /// Gets or sets the sub-region of the address.
        /// </summary>
        [JsonProperty("Subregion")]
        public string Subregion { get; set; }

        /// <summary>
        /// Gets or sets the region of the address.
        /// </summary>
        [JsonProperty("Region")]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the territory of the address.
        /// </summary>
        [JsonProperty("Territory")]
        public string Territory { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the address.
        /// </summary>
        [JsonProperty("Postal")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the postal code extension of the address.
        /// </summary>
        [JsonProperty("PostalExt")]
        public string PostalCodeExtension { get; set; }

        /// <summary>
        /// Gets or sets the country code of the address.
        /// </summary>
        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the location name.
        /// </summary>
        [JsonProperty("Loc_name")]
        public string LocationName { get; set; }

        /// <summary>
        /// Gets or sets the status of the result.
        /// </summary>
        [JsonProperty("Status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the score of the result.
        /// </summary>
        [JsonProperty("Score")]
        public double Score { get; set; }

        /// <summary>
        /// Gets or sets the place address.
        /// </summary>
        [JsonProperty("Place_addr")]
        public string PlaceAddress { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the location.
        /// </summary>
        [JsonProperty("Phone")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the url of the location.
        /// </summary>
        [JsonProperty("URL")]
        public Uri URL { get; set; }

        /// <summary>
        /// Gets or sets the rank of the location.
        /// </summary>
        [JsonProperty("Rank")]
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the additional building information for a location.
        /// </summary>
        [JsonProperty("AddBldg")]
        public string AddBuilding { get; set; }

        /// <summary>
        /// Gets or sets the additional number information for a building.
        /// </summary>
        [JsonProperty("AddNum")]
        public string AdditionalNumber { get; set; }

        /// <summary>
        /// Gets or sets the additional number from information for a location.
        /// </summary>
        [JsonProperty("AddNumFrom")]
        public string AdditionalNumberFrom { get; set; }

        /// <summary>
        /// Gets or sets the additional number to information for a location.
        /// </summary>
        [JsonProperty("AddNumTo")]
        public string AdditionalNumberTo { get; set; }

        /// <summary>
        /// Gets or sets the additional range for a location.
        /// </summary>
        [JsonProperty("AddRange")]
        public string AddRange { get; set; }

        /// <summary>
        /// Gets or sets the side information for a location.
        /// </summary>
        [JsonProperty("Side")]
        public string Side { get; set; }

        /// <summary>
        /// Gets or sets the street pre-direction for a location.
        /// </summary>
        [JsonProperty("StPreDir")]
        public string StreetPreDirection { get; set; }

        /// <summary>
        /// Gets or sets the street pre-type for a location.
        /// </summary>
        [JsonProperty("StPreType")]
        public string StreetPreType { get; set; }

        /// <summary>
        /// Gets or sets the street name a location is on.
        /// </summary>
        [JsonProperty("StName")]
        public string StreetName { get; set; }

        /// <summary>
        /// Gets or sets the street type a location is on.
        /// </summary>
        [JsonProperty("StType")]
        public string StreetType { get; set; }

        /// <summary>
        /// Gets or sets the street direction a location is on.
        /// </summary>
        [JsonProperty("StDir")]
        public string StreetDirection { get; set; }

        /// <summary>
        /// Gets or sets the building type of a location.
        /// </summary>
        [JsonProperty("BldgType")]
        public string BuildingType { get; set; }

        /// <summary>
        /// Gets or sets the building name of a location.
        /// </summary>
        [JsonProperty("BldgName")]
        public string BuildingName { get; set; }

        /// <summary>
        /// Gets or sets the level type of a location.
        /// </summary>
        [JsonProperty("LevelType")]
        public string LevelType { get; set; }

        /// <summary>
        /// Gets or sets the level name of a location.
        /// </summary>
        [JsonProperty("LevelName")]
        public string LevelName { get; set; }

        /// <summary>
        /// Gets or sets the unit type of a location.
        /// </summary>
        [JsonProperty("UnitType")]
        public string UnitType { get; set; }

        /// <summary>
        /// Gets or sets the unit name of a location.
        /// </summary>
        [JsonProperty("UnitName")]
        public string UnitName { get; set; }

        /// <summary>
        /// Gets or sets the sub-address of a location.
        /// </summary>
        [JsonProperty("SubAddr")]
        public string SubAddress { get; set; }

        /// <summary>
        /// Gets or sets the street address of a location.
        /// </summary>
        [JsonProperty("StAddr")]
        public string StreetAddress { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood of a location.
        /// </summary>
        [JsonProperty("Nbrhd")]
        public string LocationNeighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the region abbreviation of a location.
        /// </summary>
        [JsonProperty("RegionAbbr")]
        public string RegionAbbreviation { get; set; }

        /// <summary>
        /// Gets or sets the zone of a location.
        /// </summary>
        [JsonProperty("Zone")]
        public string Zone { get; set; }

        /// <summary>
        /// Gets or sets the country of a location.
        /// </summary>
        [JsonProperty("Country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the language code of a location.
        /// </summary>
        [JsonProperty("LangCode")]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the distance to a location.
        /// </summary>
        [JsonProperty("Distance")]
        public double Distance { get; set; }

        /// <summary>
        /// Gets or sets the longitude of a location.
        /// </summary>
        [JsonProperty("X")]
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the latitude of a location.
        /// </summary>
        [JsonProperty("Y")]
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the display longitude of a location.
        /// </summary>
        [JsonProperty("DisplayX")]
        public double DisplayLongitude { get; set; }

        /// <summary>
        /// Gets or sets the display latitude of a location.
        /// </summary>
        [JsonProperty("DisplayY")]
        public double DisplayLatitude { get; set; }

        /// <summary>
        /// Gets or sets the west-most longitude of a location.
        /// </summary>
        [JsonProperty("Xmin")]
        public double WestLongitude { get; set; }

        /// <summary>
        /// Gets or sets the east-most longitude of a location.
        /// </summary>
        [JsonProperty("Xmax")]
        public double EastLongitude { get; set; }

        /// <summary>
        /// Gets or sets the south-most latitude of a location.
        /// </summary>
        [JsonProperty("Ymin")]
        public double SouthLatitude { get; set; }

        /// <summary>
        /// Gets or sets the north-most latitude of a location.
        /// </summary>
        [JsonProperty("Ymax")]
        public double NorthLatitude { get; set; }

        /// <summary>
        /// Gets or sets any extra information about a location.
        /// </summary>
        [JsonProperty("ExInfo")]
        public string ExtraInformation { get; set; }
    }
}
