// <copyright file="ReverseGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Radar.Models.Parameters
{
    using System.Collections.Generic;
    using Geo.Radar.Models;

    /// <summary>
    /// The parameters possible to use during a reverse geocoding request.
    /// </summary>
    public class ReverseGeocodingParameters : ILayersParameter, IKeyParameters
    {
        /// <summary>
        /// Gets or sets the coordinates to reverse geocode.
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <inheritdoc/>
        public IList<Layer> Layers { get; } = new List<Layer>();

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
