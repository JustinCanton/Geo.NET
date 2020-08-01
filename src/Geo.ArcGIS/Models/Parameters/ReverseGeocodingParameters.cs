// <copyright file="ReverseGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.ArcGIS.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.ArcGIS.Enums;
    using Geo.ArcGIS.Models.Responses;

    /// <summary>
    /// A parameters object for the reverse geocoding ArcGIS request.
    /// </summary>
    public class ReverseGeocodingParameters : StorageParameters
    {
        /// <summary>
        /// Gets or sets the point from which to search for the closest address.
        /// </summary>
        public Coordinate Location { get; set; }

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
        /// Gets or sets a list that limits the possible match types returned by the reverseGeocode operation.
        /// </summary>
        public List<FeatureType> FeatureTypes { get; set; }

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
