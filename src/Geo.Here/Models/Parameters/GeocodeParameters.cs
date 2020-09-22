// <copyright file="GeocodeParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET. All rights reserved.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    /// <summary>
    /// The parameters possible to use during a geocoding request.
    /// </summary>
    public class GeocodeParameters : BaseFilterParameters
    {
        /// <summary>
        /// Gets or sets a free-text query.
        /// Examples:
        /// 125, Berliner, berlin
        /// Beacon, Boston, Hospital
        /// Schnurrbart German Pub and Restaurant, Hong Kong
        /// Note: Either q or qq-parameter is required on this endpoint. Both parameters can be provided in the same request.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets the search within a geographic area. This is a hard filter. Results will be returned if they are located within the specified area.
        /// A geographic area can be a country (or multiple countries), provided as comma-separated ISO 3166-1 alpha-3 country codes.
        /// Format: countryCode:{countryCode}[,{countryCode}]*
        /// Examples:
        /// countryCode:USA
        /// countryCode:CAN,MEX,USA.
        /// </summary>
        public string InCountry { get; set; }

        /// <summary>
        /// Gets or sets a qualified query. A qualified query is similar to a free-text query, but in a structured manner.
        /// It can take multiple sub-parameters, separated by semicolon, allowing to specify different aspects of a query.
        /// Currently supported sub-parameters are country, state, county, city, district, street, houseNumber, and postalCode.
        /// Format: {sub-parameter}={string}[;{sub-parameter}={string}]*
        /// Examples:
        /// city=Berlin;country=Germany;street=Friedrichstr;houseNumber=20
        /// city=Berlin;country=Germany
        /// postalCode = 10969
        /// Note: Either q or qq-parameter is required on this endpoint. Both parameters can be provided in the same request.
        /// </summary>
        public string QualifiedQuery { get; set; }
    }
}
