// <copyright file="ReverseGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Trimble.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Trimble.Models.Enums;

    /// <summary>
    /// The parameters possible to use during a Trimble Maps reverse geocoding request.
    /// </summary>
    public class ReverseGeocodingParameters : IKeyParameters, IAdditionalParameters
    {
        /// <summary>
        /// Gets or sets the coordinates to reverse geocode.
        /// <para>Required.</para>
        /// </summary>
        public Coordinate Coordinate { get; set; }

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
        /// Gets or sets the preferred response language.
        /// <para>Optional.</para>
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether results should be limited to named roads only.
        /// <para>Optional.</para>
        /// </summary>
        public bool MatchNamedRoadsOnly { get; set; }

        /// <summary>
        /// Gets or sets the maximum distance in miles to search for the nearest road.
        /// <para>Optional.</para>
        /// </summary>
        public double? MaxCleanupMiles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include posted speed limit data in the response.
        /// <para>Optional.</para>
        /// </summary>
        public bool IncludePostedSpeedLimit { get; set; }

        /// <summary>
        /// Gets or sets the vehicle type classification.
        /// <para>Optional.</para>
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// Gets or sets the direction of travel in degrees.
        /// <para>Optional.</para>
        /// </summary>
        public double? Heading { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include road link identifiers in the response.
        /// <para>Optional.</para>
        /// </summary>
        public bool IncludeLinkInfo { get; set; }

        /// <summary>
        /// Gets or sets the format to use for country abbreviations in the response.
        /// <para>Optional.</para>
        /// </summary>
        public CountryAbbrevType? CountryAbbrevType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to include Trimble place database information.
        /// <para>Optional.</para>
        /// </summary>
        public bool IncludeTrimblePlaceIds { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }

        /// <inheritdoc/>
        public IDictionary<string, string> AdditionalParameters { get; } = new Dictionary<string, string>();
    }
}
