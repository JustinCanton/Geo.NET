// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Trimble.Models.Enums;

    /// <summary>
    /// The parameters possible to use during a Trimble Maps geocoding request.
    /// </summary>
    public class GeocodingParameters : IKeyParameters, IAdditionalParameters
    {
        /// <summary>
        /// Gets or sets the street address to geocode.
        /// <para>Optional.</para>
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the city to geocode.
        /// <para>Optional.</para>
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state or province to geocode.
        /// <para>Optional.</para>
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the postal/ZIP code to geocode.
        /// <para>Optional.</para>
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Gets or sets the county to geocode.
        /// <para>Optional.</para>
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the country to geocode.
        /// <para>Optional.</para>
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the geographic region.
        /// <para>Optional.</para>
        /// <para>Default: <see cref="Region.NorthAmerica"/>.</para>
        /// </summary>
        public Region Region { get; set; } = Region.NorthAmerica;

        /// <summary>
        /// Gets or sets a specific data version to query.
        /// <para>Optional.</para>
        /// </summary>
        public string Dataset { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to return.
        /// <para>Optional.</para>
        /// </summary>
        public int? MaxResults { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether results should be limited to named roads only.
        /// <para>Optional.</para>
        /// </summary>
        public bool MatchNamedRoadsOnly { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }

        /// <inheritdoc/>
        public IDictionary<string, string> AdditionalParameters { get; } = new Dictionary<string, string>();
    }
}
