// <copyright file="ReverseGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Geoapify.Enums;
    using Geo.Geoapify.Models;

    /// <summary>
    /// The parameters possible to use during a Geoapify reverse geocoding request (<c>/v1/geocode/reverse</c>).
    /// </summary>
    public class ReverseGeocodingParameters : IKeyParameters, IAdditionalParameters
    {
        /// <summary>
        /// Gets or sets the coordinate to reverse geocode.
        /// <para>Required.</para>
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Gets or sets the type of location the results should be restricted to.
        /// <para>Optional.</para>
        /// </summary>
        public LocationType? Type { get; set; }

        /// <summary>
        /// Gets or sets the 2 character ISO 639-1 language code the results should be returned in.
        /// <para>Optional.</para>
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to return.
        /// <para>Optional.</para>
        /// <para>Default: 1.</para>
        /// </summary>
        public int? Limit { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }

        /// <inheritdoc/>
        public IDictionary<string, string> AdditionalParameters { get; } = new Dictionary<string, string>();
    }
}
