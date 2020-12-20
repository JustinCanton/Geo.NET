// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.ArcGIS.Enums;

    /// <summary>
    /// A parameters object for the geocoding ArcGIS request.
    /// </summary>
    public class GeocodingParameters
    {
        /// <summary>
        /// Gets or sets a list of address attributes.
        /// </summary>
        public List<AddressAttributeParameter> AddressAttributes { get; set; }

        /// <summary>
        /// Gets or sets a place or address type that can be used to filter suggest results.
        /// The parameter supports input of single category values or multiple comma-separated values.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets a value representing the country. When a value is passed for this parameter,
        /// all of the addresses in the input table are sent to the specified country to be geocoded.
        /// </summary>
        public string SourceCountry { get; set; }

        /// <summary>
        /// Gets or sets a set of bounding box coordinates that limit the search area for suggestions to a specific region.
        /// </summary>
        public BoundingBox SearchExtent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether StreetAddress matches should be returned even when the input house number
        /// is outside of the house number range defined for the input street. Out-of-range matches have Addr_type=StreetAddressExt.
        /// The geometry of such matches is a point corresponding to the end of the street segment where the range value is closest to the input house number.
        /// If matchOutOfRange is not specified in a request, its value is set to true by default.
        /// </summary>
        public bool MatchOutOfRange { get; set; } = true;

        /// <summary>
        /// Gets or sets the spatial reference of the x/y coordinates returned by a geocode request.
        /// For a list of valid WKID values, see Projected coordinate systems and Geographic coordinate systems
        /// (https://developers.arcgis.com/rest/services-reference/geographic-coordinate-systems.htm).
        /// </summary>
        public int OutSpatialReference { get; set; }

        /// <summary>
        /// Gets or sets the language in which reverse-geocoded addresses are returned.
        /// Addresses in many countries are available in more than one language;
        /// in these cases the LanguageCode parameter can be used to specify which language should be used for addresses returned by the reverseGeocode operation.
        /// See the table of supported countries for valid language code values in each country
        /// (https://developers.arcgis.com/rest/geocode/api-reference/geocode-coverage.htm#GUID-D61FB53E-32DF-4E0E-A1CC-473BA38A23C0).
        /// </summary>
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets a value that specifies if the output geometry of PointAddress and Subaddress matches should be the rooftop point or street entrance location.
        /// The default value is rooftop.
        /// </summary>
        public LocationType LocationType { get; set; } = LocationType.Rooftop;

        /// <summary>
        /// Gets or sets a configuration of output fields returned in a response from the
        /// ArcGIS World Geocoding Service by specifying which address component values should be included in output fields.
        /// The default value is postal city.
        /// </summary>
        public PreferredLabelValue PreferredLabelValue { get; set; } = PreferredLabelValue.PostalCity;
    }
}
