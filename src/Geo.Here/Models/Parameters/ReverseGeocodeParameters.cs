// <copyright file="ReverseGeocodeParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Here.Models.Parameters
{
    using System.Collections.Generic;

    /// <summary>
    /// The parameters possible to use during a reverse geocoding request.
    /// </summary>
    public class ReverseGeocodeParameters : BaseFilterParameters
    {
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

        /// <summary>
        /// Gets or sets a circular area, provided as latitude, longitude, and radius(in meters).
        /// Note: This parameter and the 'At' parameter are mutually exclusive. Only one of them is allowed.
        /// </summary>
        public Circle InCircle { get; set; }
    }
}
