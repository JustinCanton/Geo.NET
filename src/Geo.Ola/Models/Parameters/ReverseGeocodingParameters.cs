// <copyright file="ReverseGeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Ola.Models.Parameters
{
    /// <summary>
    /// The parameters possible to use during a reverse geocoding request.
    /// </summary>
    public class ReverseGeocodingParameters : IKeyParameters
    {
        /// <summary>
        /// Gets or sets the coordinates to reverse geocode.
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
