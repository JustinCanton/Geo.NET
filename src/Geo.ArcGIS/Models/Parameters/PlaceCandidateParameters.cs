// <copyright file="PlaceCandidateParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    using System.Globalization;
    using Geo.ArcGIS.Enums;
    using Geo.ArcGIS.Models.Responses;

    /// <summary>
    /// A parameters object for the place candidates ArcGIS request.
    /// </summary>
    public class PlaceCandidateParameters : StorageParameters
    {
        /// <summary>
        /// Gets or sets the address of the location.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets a string that represents the second line of a street address.
        /// This can include street name/house number, building name, place-name, or subunit.
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets a string that represents the third line of a street address.
        /// This can include street name/house number, building name, place-name, or subunit.
        /// </summary>
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or sets the neighbourhood of the location.
        /// </summary>
        public string Neighbourhood { get; set; }

        /// <summary>
        /// Gets or sets the city of the location.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the subregion of the location.
        /// </summary>
        public string Subregion { get; set; }

        /// <summary>
        /// Gets or sets the region of the location.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the standard postal code for an address, typically, a three– to six-digit alphanumeric code.
        /// </summary>
        public string Postal { get; set; }

        /// <summary>
        /// Gets or sets a postal code extension, such as the United States Postal Service ZIP+4 code, provides finer resolution or higher accuracy when also passing postal.
        /// </summary>
        public string PostalExt { get; set; }

        /// <summary>
        /// Gets or sets a value representing the country. Providing this value increases geocoding speed.
        /// Acceptable values include the full country name in English or the official language of the country, the two-character country code, or the three-character country code.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// gets or sets the type of places to search for such as Restaurants, Coffee Shop, Gas Stations.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the x and y coordinate or the geometry used to conduct the search.
        /// </summary>
        public Coordinate Location { get; set; }

        /// <summary>
        /// Gets or sets the spatial reference of the x/y coordinates returned by a geocode request.
        /// For a list of valid WKID values, see Projected coordinate systems and Geographic coordinate systems
        /// (https://developers.arcgis.com/rest/services-reference/geographic-coordinate-systems.htm).
        /// </summary>
        public int OutSpatialReference { get; set; }

        /// <summary>
        /// Gets or sets a set of bounding box coordinates that limit the search area for suggestions to a specific region.
        /// </summary>
        public BoundingBox SearchExtent { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of candidates to return.
        /// The maximum allowable is 50.
        /// The minimum is 1.
        /// If any other value is specified, then all matching candidates up to the service maximum are returned.
        /// </summary>
        public uint MaximumLocations { get; set; } = 0;

        /// <summary>
        /// Gets or sets the language in which reverse-geocoded addresses are returned.
        /// Addresses in many countries are available in more than one language;
        /// in these cases the LanguageCode parameter can be used to specify which language should be used for addresses returned by the reverseGeocode operation.
        /// See the table of supported countries for valid language code values in each country
        /// (https://developers.arcgis.com/rest/geocode/api-reference/geocode-coverage.htm#GUID-D61FB53E-32DF-4E0E-A1CC-473BA38A23C0).
        /// </summary>
        public CultureInfo LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the output geometry of PointAddress and Subaddress matches should be the rooftop point or street entrance location.
        /// The default value is rooftop.
        /// </summary>
        public LocationType LocationType { get; set; } = LocationType.Rooftop;
    }
}
