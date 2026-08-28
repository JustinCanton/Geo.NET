// <copyright file="BaseSearchParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Geoapify.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Geoapify.Enums;
    using Geo.Geoapify.Models.Parameters.Biases;
    using Geo.Geoapify.Models.Parameters.Filters;

    /// <summary>
    /// The shared parameters common to the Geoapify forward geocoding and address autocomplete endpoints.
    /// </summary>
    public abstract class BaseSearchParameters : IKeyParameters, IAdditionalParameters
    {
        /// <summary>
        /// Gets or sets the API key. When set, this overrides the key configured during dependency injection.
        /// <para>Optional.</para>
        /// </summary>
        public string Key { get; set; }

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
        /// <para>Default: 5. Maximum: 100.</para>
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Gets the filters restricting the area the results are taken from. Multiple filters are combined with AND logic.
        /// <para>Optional.</para>
        /// </summary>
        public IList<Filter> Filters { get; } = new List<Filter>();

        /// <summary>
        /// Gets the biases preferring the area the results are taken from. Multiple biases are combined with OR logic.
        /// <para>Optional.</para>
        /// </summary>
        public IList<Bias> Biases { get; } = new List<Bias>();

        /// <inheritdoc/>
        public IDictionary<string, string> AdditionalParameters { get; } = new Dictionary<string, string>();
    }
}
