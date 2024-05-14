// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Radar.Models;

    /// <summary>
    /// The parameters possible to use during a geocoding request.
    /// </summary>
    public class GeocodingParameters : ICountryParameter, ILayersParameter, IKeyParameters
    {
        /// <summary>
        /// Gets or sets the address to geocode.
        /// </summary>
        public string Query { get; set; }

        /// <inheritdoc/>
        public IList<string> Countries { get; } = new List<string>();

        /// <inheritdoc/>
        public IList<Layer> Layers { get; } = new List<Layer>();

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
