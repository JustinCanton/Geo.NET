// <copyright file="GeocodeParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    using System.Collections.Generic;
    using System.Globalization;

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
        /// Note: Either query or qualified query parameter is required on this endpoint. Both parameters can be provided in the same request.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Gets the search within a geographic area. This is a hard filter. Results will be returned if they are located within the specified area.
        /// A geographic area can be a country (or multiple countries).
        /// </summary>
        public IList<RegionInfo> InCountry { get; } = new List<RegionInfo>();

        /// <summary>
        /// Gets or sets a qualified query. A qualified query is similar to a free-text query, but in a structured manner.
        /// It can take multiple sub-parameters, separated by semicolon, allowing to specify different aspects of a query.
        /// Currently supported sub-parameters are country, state, county, city, district, street, houseNumber, and postalCode.
        /// Format: {sub-parameter}={string}[;{sub-parameter}={string}]*
        /// Examples:
        /// city=Berlin;country=Germany;street=Friedrichstr;houseNumber=20
        /// city=Berlin;country=Germany
        /// postalCode = 10969
        /// Note: Either query or qualified query parameter is required on this endpoint. Both parameters can be provided in the same request.
        /// </summary>
        public string QualifiedQuery { get; set; }

        /// <summary>
        /// Gets a list of the types that should be included in the response. If this parameter is not set, all types are considered for the response.
        /// Supported values are:
        /// <list type="bullet">
        /// <item>address: restricting results to result types houseNumber, street, postalCodePoint, intersection, or addressBlock.</item>
        /// <item>area: restricting results to result types locality or administrativeArea including all the sub-types.</item>
        /// <item>city: restricting results to result type locality and locality type city.</item>
        /// <item>houseNumber: restricting results to result type: houseNumber, including house number types PA (Point Address) and interpolated, including exact house number matches and house number fallbacks.</item>
        /// <item>
        /// postalCode: restricting results to postal codes: either result type postalCodePoint or result type locality with locality type postalCode.
        /// Note that in Ireland and Singapore, where each address has unique postal code, postalCodePoint results are replaced by houseNumber results.
        /// </item>
        /// <item>street: restricting results to result type street.</item>
        /// </list>
        /// </summary>
        public IList<string> Types { get; } = new List<string>();
    }
}
