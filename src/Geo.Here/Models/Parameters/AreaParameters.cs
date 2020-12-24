// <copyright file="AreaParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    /// <summary>
    /// Parameters used for narrowing down the geographical area in a HERE request.
    /// </summary>
    public class AreaParameters : BaseFilterParameters
    {
        /// <summary>
        /// Gets or sets the search within a geographic area. This is a hard filter. Results will be returned if they are located within the specified area.
        /// A geographic area can be a country (or multiple countries), provided as comma-separated ISO 3166-1 alpha-3 country codes.
        /// Format: countryCode:{countryCode}[,{countryCode}]*
        /// Examples:
        /// countryCode:USA
        /// countryCode:CAN,MEX,USA.
        /// Note: This parameter must be accompanied by the 'At' parameter.
        /// </summary>
        public string InCountry { get; set; }

        /// <summary>
        /// Gets or sets a circular area, provided as latitude, longitude, and radius(in meters).
        /// Note: This parameter and the 'At' parameter are mutually exclusive. Only one of them is allowed.
        /// </summary>
        public Circle InCircle { get; set; }

        /// <summary>
        /// Gets or sets a bounding box, provided as west longitude, south latitude, east longitude, north latitude.
        /// Note: This parameter and the 'At' parameter are mutually exclusive. Only one of them is allowed.
        /// </summary>
        public BoundingBox InBoundingBox { get; set; }

        /// <summary>
        /// Gets or sets a geographic corridor. This is a hard filter. Results will be returned if they are located within the specified area.
        /// A route is defined by a Flexible Polyline Encoding, followed by an optional width, represented by a sub-parameter "w".
        /// In regular expression syntax, the values of route look like:
        /// [a-zA-Z0-9_-]+(;w=\d+)?
        /// "[a-zA-Z0-9._-]+" is the encoded flexible polyline.
        /// "w=\d+" is the optional width. The width is specified in meters from the center of the path. If no width is provided, the default is 1000 meters.
        /// Examples:
        /// BFoz5xJ67i1B1B7PzIhaxL7Y
        /// BFoz5xJ67i1B1B7PzIhaxL7Y; w=5000
        /// BlD05xgKuy2xCCx9B7vUCl0OhnRC54EqSCzpEl-HCxjD3pBCiGnyGCi2CvwFCsgD3nDC4vB6eC;w=2000
        /// These can be encoded and decoded using the python app found at https://github.com/heremaps/flexible-polyline/tree/master/python.
        /// </summary>
        public string Route { get; set; }
    }
}
