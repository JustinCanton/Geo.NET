// <copyright file="GeocodingParameters.cs" company="Geo.NET">
// Copyright (c) Geo.NET.
// Licensed under the MIT license. See the LICENSE file in the solution root for full license information.
// </copyright>

namespace Geo.Ola.Models.Parameters
{
    /// <summary>
    /// The parameters possible to use during a geocoding request.
    /// </summary>
    public class GeocodingParameters : IKeyParameters
    {
        /// <summary>
        /// Gets or sets the address to geocode.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the language in which to return the results.
        /// </summary>
        public string Language { get; set; }

        /// <inheritdoc/>
        public string Key { get; set; }
    }
}
