// <copyright file="ReverseSearchParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.OpenRouteService.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.OpenRouteService.Enums;
    using Geo.OpenRouteService.Models;

    /// <summary>
    /// Parameters for the ORS reverse geocoding endpoint (<c>/geocode/reverse</c>).
    /// </summary>
    public class ReverseSearchParameters : IKeyParameters, IAdditionalParameters
    {
        /// <summary>
        /// Gets or sets the API key. When set, overrides the key configured via dependency injection.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the coordinate to reverse geocode. Required.
        /// </summary>
        public Coordinate Point { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of results to return. Defaults to 10; maximum is 40.
        /// </summary>
        public int? Size { get; set; }

        /// <summary>
        /// Gets or sets the search radius in kilometers. Defaults to 1 km; maximum is 5 km.
        /// </summary>
        public double? BoundaryCircleRadius { get; set; }

        /// <summary>
        /// Gets the list of ISO 3166-1 alpha-2 or alpha-3 country codes used to restrict results.
        /// </summary>
        public IList<string> BoundaryCountries { get; } = new List<string>();

        /// <summary>
        /// Gets or sets a Pelias geographic identifier (GID) used to restrict results to a specific administrative area.
        /// </summary>
        public string BoundaryGid { get; set; }

        /// <summary>
        /// Gets the list of data sources to include in results.
        /// </summary>
        public IList<SourceType> Sources { get; } = new List<SourceType>();

        /// <summary>
        /// Gets the list of layer types to include in results.
        /// </summary>
        public IList<LayerType> Layers { get; } = new List<LayerType>();

        /// <summary>
        /// Gets additional parameters to append to the request query string.
        /// </summary>
        public IDictionary<string, string> AdditionalParameters { get; } = new Dictionary<string, string>();
    }
}
