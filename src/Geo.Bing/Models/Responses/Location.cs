// <copyright file="Location.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Bing.Models.Responses
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A location with all of the necessary location information.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets or sets the type of the underlying resource.
        /// </summary>
        [JsonProperty("__type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the geographic area that contains the location.
        /// A bounding box contains SouthLatitude, WestLongitude, NorthLatitude, and EastLongitude values in degrees.
        /// </summary>
        [JsonProperty("bbox")]
        public BoundingBox BoundingBox { get; set; }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the latitude and longitude coordinates of the location.
        /// </summary>
        [JsonProperty("point")]
        public Point Point { get; set; }

        /// <summary>
        /// Gets or sets the classification of the geographic entity returned, such as Address.
        /// </summary>
        [JsonProperty("entityType")]
        public string EntityType { get; set; }

        /// <summary>
        /// Gets or sets the postal address for the location.
        /// </summary>
        [JsonProperty("address")]
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets The level of confidence that the geocoded location result is a match.
        /// Use this value with the match code to determine for more complete information about the match.
        /// The confidence of a geocoded location is based on many factors including the relative importance of the geocoded location and the user’s location, if specified.
        /// The following description provides more information about how confidence scores are assigned and how results are ranked.
        /// If the confidence is set to High, one or more strong matches were found.
        /// Multiple High confidence matches are sorted in ranked order by importance when applicable.
        /// For example, landmarks have importance but addresses do not.
        /// If a request includes a user location or a map area (see User Context Parameters), then the ranking may change appropriately.
        /// For example, a location query for "Paris" returns "Paris, France" and "Paris, TX" both with High confidence.
        /// "Paris, France" is always ranked first due to importance unless a user location indicates that the user is in or very close to Paris, TX
        /// or the map view indicates that the user is searching in that area.
        /// In some situations, the returned match may not be at the same level as the information provided in the request.
        /// For example, a request may specify address information and the geocode service may only be able to match a postal code.
        /// In this case, if the geocode service has a confidence that the postal code matches the data,
        /// the confidence is set to Medium and the match code is set to UpHierarchy to specify that it could not match all of the information and had to search up-hierarchy.
        /// If the location information in the query is ambiguous, and there is no additional information to rank the locations
        /// (such as user location or the relative importance of the location), the confidence is set to Medium.
        /// For example, a location query for "148th Ave, Bellevue" may return "148th Ave SE" and "148th Ave NE" both with Medium confidence.
        /// If the location information in the query does not provide enough information to geocode a specific location,
        /// a less precise location value may be returned and the confidence is set to Medium.For example, if an address is provided,
        /// but a match is not found for the house number, the geocode result with a Roadblock entity type may be returned.
        /// You can check the entityType field value to determine what type of entity the geocode result represents.
        /// </summary>
        [JsonProperty("confidence")]
        public string Confidence { get; set; }

        /// <summary>
        /// Gets one or more match code values that represent the geocoding level for each location in the response.
        /// For example, a geocoded location with match codes of Good and Ambiguous means that more than one geocode location was found for the location information
        /// and that the geocode service did not have search up-hierarchy to find a match.
        /// Similarly, a geocoded location with match codes of Ambiguous and UpHierarchy implies that a geocode location could not be found that matched all the provided location information,
        /// so the geocode service had to search up-hierarchy and found multiple matches at that level.
        /// An example of up an Ambiguous and UpHierarchy result is when you provide complete address information,
        /// but the geocode service cannot locate a match for the street address and instead returns information for more than one RoadBlock value.
        ///
        /// The possible values are:
        /// Good: The location has only one match or all returned matches are considered strong matches. For example, a query for New York returns several Good matches.
        ///
        /// Ambiguous: The location is one of a set of possible matches.For example, when you query for the street address 128 Main St.,
        /// the response may return two locations for 128 North Main St.and 128 South Main St.because there is not enough information to determine which option to choose.
        ///
        /// UpHierarchy: The location represents a move up the geographic hierarchy.This occurs when a match for the location request was not found, so a less precise result is returned.
        /// For example, if a match for the requested address cannot be found, then a match code of UpHierarchy with a RoadBlock entity type may be returned.
        /// </summary>
        [JsonProperty("matchCodes")]
        public List<string> MatchCodes { get; } = new List<string>();

        /// <summary>
        /// Gets a collection of geocoded points that differ in how they were calculated and their suggested use.
        /// </summary>
        [JsonProperty("geocodePoints")]
        public List<Point> GeocodePoints { get; } = new List<Point>();

        /// <summary>
        /// Gets a collection of parsed values that shows how a location query string was parsed into one or more of the following address values.
        /// AddressLine
        /// Locality
        /// AdminDistrict
        /// AdminDistrict2
        /// PostalCode
        /// CountryRegion
        /// Landmark.
        /// </summary>
        [JsonProperty("queryParseValues")]
        public List<QueryParseValue> QueryParseValues { get; } = new List<QueryParseValue>();
    }
}
